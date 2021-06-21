using ControllerForCookieClicker.Different;
using CookieClicker.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControllerForCookieClicker.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class EnhancementAccountController : ControllerBase
    {
        DataBaseCookieContext db;
        public EnhancementAccountController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnhancementAccount>>> Get()
        {
            Log.Information("Get IEnumerable<EnhancementAccount> Success.");
            return await db.EnhancementAccount.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(EnhancementAccount enhancementAccount)
        {
            var answer = await db.EnhancementAccount.FirstOrDefaultAsync(obj => obj.IdEnhancement == enhancementAccount.IdEnhancement && obj.IdAccount == enhancementAccount.IdEnhancement);
            if (enhancementAccount == null)
            {
                Log.Information($"Post EnhancementAccount BadRequest. Равен null.");
                return BadRequest();
            }
            else if(answer != null)
            {
                Log.Information($"Post EnhancementAccount BadRequest. Такое улучшение на аккаунте есть.");
                return BadRequest();
            }

            db.EnhancementAccount.Add(enhancementAccount);
            await db.SaveChangesAsync();
            Log.Information($"Post EnhancementAccount Success.");
            return Ok("Success");
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put(EnhancementAccount enhancementAccount)
        {
            var answer = await db.EnhancementAccount.ToListAsync();
            var answerFindId = answer.FirstOrDefault(obj => obj.IdEnhancementAccount == enhancementAccount.IdEnhancementAccount);
            var answerFindCopy = answer.FirstOrDefault(obj => obj.IdEnhancement == enhancementAccount.IdEnhancement && obj.IdAccount == enhancementAccount.IdEnhancement);
            if (answerFindId == null)
            {
                Log.Information($"Put EnhancementAccount BadRequest. Равен null.");
                return BadRequest();
            }
            else if (answerFindCopy != null)
            {
                Log.Information($"Put EnhancementAccount BadRequest. Такое улучшение на аккаунте есть.");
                return BadRequest();
            }

            answerFindId.IdAccount = enhancementAccount.IdAccount;
            answerFindId.IdEnhancement = enhancementAccount.IdEnhancement;
            answerFindId.Quantity = enhancementAccount.Quantity;

            await db.SaveChangesAsync();
            Log.Information($"Put EnhancementAccount Success.");
            return Ok("Success");
        }

        [HttpDelete]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            EnhancementAccount enhancementAccount = await db.EnhancementAccount.FirstOrDefaultAsync(obj => obj.IdEnhancementAccount == id);
            if (enhancementAccount == null)
            {
                Log.Information($"Delete EnhancementAccount NotFound.");
                return NotFound();
            }
            db.EnhancementAccount.Remove(enhancementAccount);
            await db.SaveChangesAsync();
            Log.Information($"Delete EnhancementAccount Success.");
            return Ok("Success");
        }
    }
}
