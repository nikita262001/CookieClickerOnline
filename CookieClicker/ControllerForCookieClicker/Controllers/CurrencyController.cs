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
    public class CurrencyController : ControllerBase
    {
        DataBaseCookieContext db;
        public CurrencyController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> Get()
        {
            Log.Information("Get IEnumerable<Currency> Success.");
            return await db.Currency.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Currency currency)
        {
            if (currency == null)
            {
                Log.Information($"Post Currency BadRequest. Равен null.");
                return BadRequest();
            }

            db.Currency.Add(currency);
            await db.SaveChangesAsync();
            Log.Information($"Post Currency Success.");
            return Ok("Success");
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put(Currency currency)
        {
            var answer = await db.Currency.FirstOrDefaultAsync(obj => obj.IdCurrency == currency.IdCurrency);
            if (currency == null)
            {
                Log.Information($"Put Currency BadRequest. Равен null.");
                return BadRequest();
            }
            else if (answer == null)
            {
                Log.Information($"Put Currency NotFound.");
                return NotFound();
            }
            answer.Name = currency.Name;
            answer.RublesToOneCurrency = currency.RublesToOneCurrency;

            await db.SaveChangesAsync();
            Log.Information($"Put Currency Success.");
            return Ok("Success");
        }

        [HttpDelete]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var currency = await db.Currency.FirstOrDefaultAsync(obj => obj.IdCurrency == id);
            if (currency == null)
            {
                Log.Information($"Delete Currency NotFound.");
                return NotFound();
            }
            db.Currency.Remove(currency);
            await db.SaveChangesAsync();
            Log.Information($"Delete Currency Success.");
            return Ok("Success");
        }
    }
}
