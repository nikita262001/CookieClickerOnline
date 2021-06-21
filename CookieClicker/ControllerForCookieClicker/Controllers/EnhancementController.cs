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
    public class EnhancementController : ControllerBase
    {
        DataBaseCookieContext db;
        public EnhancementController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enhancement>>> Get()
        {
            Log.Information("Get IEnumerable<Enhancement> Success.");
            return await db.Enhancement.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Enhancement enhancement)
        {
            if (enhancement == null)
            {
                Log.Information($"Post Enhancement BadRequest. Равен null.");
                return BadRequest();
            }

            db.Enhancement.Add(enhancement);
            await db.SaveChangesAsync();
            Log.Information($"Post Enhancement Success.");
            return Ok("Success");
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put(Enhancement enhancement)
        {
            var answer = await db.Enhancement.FirstOrDefaultAsync(obj => obj.IdEnhancement == enhancement.IdEnhancement);
            if (answer == null)
            {
                Log.Information($"Put Enhancement BadRequest. Равен null.");
                return BadRequest();
            }
            else if ( (enhancement.IdTypeEnhancement == 3 && enhancement.CookiePerSecond != 0) || (enhancement.IdTypeEnhancement == 1 && enhancement.CookiePerSecond == 0))
            {
                Log.Information($"Put Enhancement BadRequest. Не корректно введены данные.");
                return BadRequest();
            }
            
            answer.Name = enhancement.Name;
            answer.FirstCost = enhancement.FirstCost;
            answer.CookiePerSecond = enhancement.CookiePerSecond;
            answer.BonusFormat = enhancement.BonusFormat;
            answer.IdTypeEnhancement = enhancement.IdTypeEnhancement;
            answer.IdImageIB = enhancement.IdImageIB;

            await db.SaveChangesAsync();
            Log.Information($"Put Enhancement Success.");
            return Ok("Success");
        }

        [HttpDelete]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var enhancement = await db.Enhancement.FirstOrDefaultAsync(obj => obj.IdEnhancement == id);
            if (enhancement == null)
            {
                Log.Information($"Delete Enhancement NotFound.");
                return NotFound();
            }
            db.Enhancement.Remove(enhancement);
            await db.SaveChangesAsync();
            Log.Information($"Delete Enhancement Success.");
            return Ok("Success");
        }
    }
}
