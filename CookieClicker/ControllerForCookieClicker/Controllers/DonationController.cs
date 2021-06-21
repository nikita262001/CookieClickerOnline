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
    public class DonationController : ControllerBase
    {
        DataBaseCookieContext db;
        public DonationController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> Get()
        {
            Log.Information("Get IEnumerable<Donation> Success.");
            return await db.Donation.ToListAsync();
        }
        [HttpPut]
        public async Task<ActionResult<string>> Put(Donation donation)
        {
            var answer = await db.Donation.FirstOrDefaultAsync(obj => obj.IdDonat == donation.IdDonat);
            if (answer == null)
            {
                Log.Information($"Put Donation BadRequest. Равен null.");
                return BadRequest();
            }
            else if (answer == null)
            {
                Log.Information($"Put Donation NotFound.");
                return NotFound();
            }
            answer.Comment = donation.Comment;
            answer.IdDonateStatus = donation.IdDonateStatus;

            await db.SaveChangesAsync();
            Log.Information($"Put Donation Success.");
            return Ok("Success");
        }
    }
}
