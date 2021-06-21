using ControllerForCookieClicker.Different;
using CookieClicker.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControllerForCookieClicker.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        DataBaseCookieContext db;
        public AccountController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            Log.Information("GetAll IEnumerable<Account> Success.");
            return await db.Account.ToListAsync();
        }

        [HttpGet]
        [Route("take={takeCount}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetTakeCountTop(int takeCount)
        {
            Log.Information("GetTakeCountTop IEnumerable<Account> Success.");
            return (await db.Account.ToListAsync()).OrderByDescending(obj => obj.Cookies).Take(takeCount).ToList();
        }

        [HttpGet]
        [Route("login={login}&password={password}")]
        public async Task<ActionResult<Account>> GetTarget(string login, string password)
        {
            var userFind = await db.Account.FirstOrDefaultAsync(obj => obj.Login == login && obj.Password == password);
            if (userFind == null)
            {
                Log.Information($"Get Account NotFound. Логин={login}, Пароль={password}.");
                return NotFound();
            }
            Log.Information("Get Account Success.");
            return Ok(userFind);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Account user)
        {
            var userFind = await db.Account.FirstOrDefaultAsync(obj => obj.Login == user.Login);
            if (user == null)
            {
                Log.Information($"Post Account BadRequest. Равен null.");
                return BadRequest();
            }
            else if (userFind != null)
            {
                Log.Information($"Post Account BadRequest. Пользователь с таким логином существует.");
                return BadRequest();
            }

            user.ChocolateTime = 0;
            user.ClickGold = 0;
            user.Cookies = 0;
            user.DateRegistration = DateTime.Now;
            user.LastEntrance = DateTime.Now;

            db.Account.Add(user);
            await db.SaveChangesAsync();
            Log.Information($"Post Account Success. Логин={user.Login}");
            return Ok("Success");
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put(Account user)
        {
            var answer = await db.Account.FirstOrDefaultAsync(obj => obj.IdAccount == user.IdAccount);
            if (user == null)
            {
                Log.Information($"Put Account BadRequest. Равен null.");
                return BadRequest();
            }
            else if (answer == null)
            {
                Log.Information($"Put Account NotFound. Логин={user.Login}, Пароль={user.Password}.");
                return NotFound();
            }
            answer.Password = user.Password;
            answer.Cookies = user.Cookies;
            answer.ClickGold = user.ClickGold;
            answer.ChocolateTime = user.ChocolateTime;
            answer.LastEntrance = user.LastEntrance;

            await db.SaveChangesAsync();
            Log.Information($"Put Account Success.");
            return Ok("Success");
        }

        [HttpDelete]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var user = await db.Account.FirstOrDefaultAsync(obj => obj.IdAccount == id);
            if (user == null)
            {
                Log.Information($"Delete Account NotFound.");
                return NotFound();
            }
            db.Account.Remove(user);
            await db.SaveChangesAsync();
            Log.Information($"Delete Account Success.");
            return Ok("Success");
        }

        [HttpPut]
        [Route("account={idAccount}&countCookies={cookie}")]
        public void UpdateCookie(int idAccount, decimal cookie)
        {
            var account = db.Account.FirstOrDefault(obj => obj.IdAccount == idAccount);
            account.Cookies += cookie;
            account.LastEntrance = DateTime.Now;
            db.SaveChanges();
        }

        [HttpPut]
        [Route("account={idAccount}&cookies={cookie}&clickGold={clickGold}&chocolateTime={chocolateTime}")]
        public void UpdateAccountForTime(int idAccount, decimal cookie, decimal clickGold, decimal chocolateTime)
        {
            var account = db.Account.FirstOrDefault(obj => obj.IdAccount == idAccount);
            account.Cookies += cookie;
            account.ClickGold = clickGold;
            account.ChocolateTime = chocolateTime;
            account.LastEntrance = DateTime.Now;
            db.SaveChanges();
            DistributionOfCookiesToFriends(idAccount, cookie);
        }

        private async void DistributionOfCookiesToFriends(int idAccount, decimal cookie)
        {
            List<Account> friends = new List<Account>();
            var listAccounts = await db.Account.ToListAsync();
            var listFriends = await db.Friend.ToListAsync();

            var userFind = listAccounts.FirstOrDefault(obj => obj.IdAccount == idAccount);
            if (userFind == null)
            {
                return;
            }
            else
            {
                foreach (var item in listFriends.Where(obj => obj.Invited == idAccount && obj.BeFriends == true))
                {
                    friends.Add(listAccounts.FirstOrDefault(obj => obj.IdAccount == item.Inviting));
                }
                foreach (var item in listFriends.Where(obj => obj.Inviting == idAccount && obj.BeFriends == true))
                {
                    friends.Add(listAccounts.FirstOrDefault(obj => obj.IdAccount == item.Invited));
                }
            }

            foreach (var item in friends)
                item.Cookies += cookie * Convert.ToDecimal(0.2);
            db.SaveChanges();

        }
    }
}
