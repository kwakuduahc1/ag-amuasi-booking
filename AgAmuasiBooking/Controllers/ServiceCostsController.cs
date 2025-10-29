using AgAmuasiBooking.Context;
using AgAmuasiBooking.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace AccountingUltimate.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("bStudioApps")]
    [Authorize(Policy = "Administration")]
    public class ServiceCostsController(DbContextOptions<ApplicationDbContext> dbContextOptions, CancellationToken token) : ControllerBase
    {
        private readonly ApplicationDbContext db = new(dbContextOptions);

        [HttpGet]
        public async Task<IEnumerable> List()
        {
            const string sql = """
                select s.servicesid, servicename, perperson, c.cost
                from services s
                inner join (
                    SELECT servicesid, cost, row_number() OVER (PARTITION BY servicesid ORDER BY servicesid DESC) AS rn
                    from servicecosts 
                ) as c on c.servicesid = s.servicesid AND c.rn = 1
                ORDER BY servicename;
                """;
            using var con = db.Database.GetDbConnection();
            return await con.QueryAsync<PrimeServiceCost>(sql);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCostDto serv)
        {
            if (!await db.Services.AnyAsync(x => x.ServicesID == serv.ID))
                return BadRequest(new { Message = "The service was not found" });
            var s = new ServiceCosts { Cost = serv.Cost, DateSet = DateTime.UtcNow, ServicesID = serv.ID };
            db.ServiceCosts.Add(s);
            await db.SaveChangesAsync(token);
            return Ok(s.ServiceCostsID);
        }
    }
    public record PrimeServiceCost(int ServicesID, string ServiceName, bool PerPerson, decimal Cost);
}