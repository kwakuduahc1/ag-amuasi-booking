using AgAmuasiBooking.Context;
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
    //[Authorize(Policy = "Manager")]
    //[Authorize(Policy = "Administrator")]
    public class ServicesController(DbContextOptions<ApplicationDbContext> dbContextOptions, CancellationToken token) : ControllerBase
    {
        private readonly ApplicationDbContext db = new(dbContextOptions);

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable> List() => await db.Services
            .OrderByDescending(x => x.ServiceName)
            .Select(x => new
            {
                x.ServicesID,
                x.ServiceName,
                ServiceCosts = x.ServiceCosts
                .OrderByDescending(c => c.ServiceCostsID)
                .Select(c => new
                {
                    c.Service,
                    c.ServiceCostsID,
                    c.Cost,
                    c.DateSet
                })
                .FirstOrDefault() ?? null
            })
            .AsNoTracking()
            .ToListAsync(token);

    }
}