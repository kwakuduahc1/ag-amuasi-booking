using AgAmuasiBooking.Context;
using AgAmuasiBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingUltimate.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("bStudioApps")]
    //[Authorize(Policy = "Manager")]
    [Authorize(Policy = "Administration")]
    public class ServiceCostsController(DbContextOptions<ApplicationDbContext> dbContextOptions, CancellationToken token) : ControllerBase
    {
        private readonly ApplicationDbContext db = new(dbContextOptions);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCostDto serv)
        { 
            if (!await db.Services.AnyAsync(x=>x.ServicesID == serv.ID))
                return BadRequest(new { Message = "The service was not found" });
            var s = new ServiceCosts { Cost = serv.Cost, DateSet = DateTime.UtcNow, ServicesID = serv.ID };
            db.ServiceCosts.Add(s);
            await db.SaveChangesAsync(token);
            return Ok(s.ServiceCostsID);
        }
    }
}