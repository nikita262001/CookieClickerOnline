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
    public class TypeEnhancementController : ControllerBase
    {
        DataBaseCookieContext db;
        public TypeEnhancementController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEnhancement>>> Get()
        {
            Log.Information("Get IEnumerable<TypeEnhancement> Success.");
            return await db.TypeEnhancement.ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put(TypeEnhancement typeEnhancement)
        {
            var answer = await db.TypeEnhancement.FirstOrDefaultAsync(obj => obj.IdTypeEnhancement == typeEnhancement.IdTypeEnhancement);
            if (answer == null)
            {
                Log.Information($"Put ImageIB BadRequest. Равен null.");
                return BadRequest();
            }
            answer.Name = typeEnhancement.Name;
            answer.СoefNextBuy = typeEnhancement.СoefNextBuy;

            await db.SaveChangesAsync();
            Log.Information($"Put ImageIB Success.");
            return Ok("Success");
        }
    }
}
