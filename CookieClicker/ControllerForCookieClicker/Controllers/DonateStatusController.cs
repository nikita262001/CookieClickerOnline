using ControllerForCookieClicker.Different;
using CookieClicker.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControllerForCookieClicker.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class DonateStatusController : ControllerBase
    {
        DataBaseCookieContext db;
        public DonateStatusController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonateStatus>>> Get()
        {
            Log.Information("Get IEnumerable<DonateStatus> Success.");
            return await db.DonateStatus.ToListAsync();
        }
    }
}
