using AgAmuasiBooking.Context;
using AgAmuasiBooking.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AccountingUltimate.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("bStudioApps")]
    [Authorize(Policy = "Administration")]
    public class ServicesController(DbContextOptions<ApplicationDbContext> dbContextOptions, CancellationToken token) : ControllerBase
    {
        private readonly ApplicationDbContext db = new(dbContextOptions);

        [HttpGet]
        public async Task<IEnumerable> List()
        {
            const string qry = """
                SELECT s.servicesid, s.servicename, c.cost, c.servicecostsid, s.perperson
                FROM services s
                LEFT JOIN LATERAL(
                   	SELECT c.servicesid, c."cost", c.servicecostsid, ROW_NUMBER() OVER(PARTITION BY  servicesid ORDER BY c.servicecostsid DESC) rn
                   	FROM servicecosts c
                   	WHERE c.servicesid = s.servicesid
                )  c ON c.servicesid = s.servicesid
                WHERE c.rn < 6
                ORDER BY s.servicename
                """;
            using var con = db.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync(token);
            var res = await con.QueryAsync<ServicesDto>(qry);
            return res.GroupBy(x => new
            {
                x.ServicesID,
                x.ServiceName,
                x.PerPerson
            }, (k, v) => new
            {
                k.ServicesID,
                k.ServiceName,
                k.PerPerson,
                Cost = v.First()?.Cost ?? 0,
                Costs = v.Select(x => new
                {
                    x.ServiceCostsID,
                    x.Cost
                })
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] AddServiceDto serv)
        {
            Services? _serv;
            if (serv.ServicesID > 0)
            {
                _serv = await db.Services.FindAsync(serv.ServicesID, token);
                if (_serv == null)
                    return BadRequest(new { Message = "The service was not found" });
                _serv.ServiceName = serv.ServiceName;
                db.Entry(_serv).State = EntityState.Modified;
                await db.SaveChangesAsync(token);
                return Ok(new { id = _serv.ServicesID, sid = 0 });
            }
            else
            {
                if (await db.Services.AnyAsync(x => x.ServiceName == serv.ServiceName))
                    return BadRequest(new { Message = $"Service {serv.ServiceName} already exists" });
                _serv = new Services
                {
                    ServiceName = serv.ServiceName,
                    PerPerson = serv.PerPerson,
                    ServiceCosts = [
                        new ServiceCosts { Cost = serv.Cost, DateSet = DateTime.UtcNow }
                    ]
                };
                db.Services.Add(_serv);
                await db.SaveChangesAsync(token);
                return Ok(new
                {
                    id = _serv.ServicesID,
                    sid = _serv.ServiceCosts.First().ServiceCostsID
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var serv = await db.Services.FindAsync(id, token);
            if (serv == null)
                return NotFound(new { Message = "The service was not found" });
            db.Services.Remove(serv);
            await db.SaveChangesAsync(token);
            return Ok(new { Message = "The service has been deleted successfully" });
        }
    }

    public record ServicesDto(int ServicesID, string ServiceName, decimal Cost, int ServiceCostsID, bool PerPerson);

    public record AddServiceDto(int ServicesID, [StringLength(50, MinimumLength = 3)] string ServiceName, [Range(1, double.MaxValue)] decimal Cost, bool PerPerson);

    public record ServiceCostDto(int ID, [Range(1, double.MaxValue)] decimal Cost);

    public record ServiceListResponseDto(int ServicesID, string ServiceName, decimal Cost, IEnumerable<ServiceCostResponseDto> Costs);

    public record ServiceCostResponseDto(int ServiceCostsID, decimal Cost);
}