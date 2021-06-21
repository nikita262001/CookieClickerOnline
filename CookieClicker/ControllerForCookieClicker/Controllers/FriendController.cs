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
    public class FriendController : ControllerBase
    {
        DataBaseCookieContext db;
        public FriendController()
        {
            db = new DataBaseCookieContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetAll()
        {
            Log.Information("GetAll IEnumerable<Account> Success.");
            return await db.Friend.ToListAsync();
        }

        [HttpGet]
        [Route("idFriend={idFriend}/BeFriends={beFriend}")]
        public async Task<ActionResult<List<Account>>> GetAllFriendTarget(int idFriend,bool beFriend)
        {
            List<Account> friends = new List<Account>();
            var listAccounts = await db.Account.ToListAsync();
            var userFind = listAccounts.FirstOrDefault(obj => obj.IdAccount == idFriend);
            if (userFind == null)
            {
                Log.Information($"GetAllFriendTarget Friend NotFound.");
                return NotFound();
            }
            else
            {
                var listFriends = await db.Friend.ToListAsync();
                foreach (var item in listFriends.Where(obj => obj.Invited == idFriend && obj.BeFriends == beFriend))
                {
                    friends.Add(listAccounts.FirstOrDefault(obj => obj.IdAccount == item.Inviting));
                }
                foreach (var item in listFriends.Where(obj => obj.Inviting == idFriend && obj.BeFriends == beFriend))
                {
                    friends.Add(listAccounts.FirstOrDefault(obj => obj.IdAccount == item.Invited));
                }
            }
            Log.Information("GetAllFriendTarget Friend Success.");
            return Ok(friends);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(Friend friend)
        {
            var friendFind = await db.Friend.FirstOrDefaultAsync(obj => obj.Inviting == friend.IdFriend || obj.Invited == friend.IdFriend);
            if (friend == null)
            {
                Log.Information($"Post Friend BadRequest.");
                return BadRequest();
            }
            else if (friendFind != null)
            {
                Log.Information($"Post Friend BadRequest.");
                return BadRequest();
            }

            friend.FriendshipDate = DateTime.Now;
            friend.BeFriends = false;

            db.Friend.Add(friend);
            await db.SaveChangesAsync();
            Log.Information($"Post Friend Success.");
            return Ok("Success");
        }

        [HttpDelete]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var friend = await db.Friend.FirstOrDefaultAsync(obj => obj.IdFriend == id);
            if (friend == null)
            {
                Log.Information($"Delete Friend NotFound.");
                return NotFound();
            }
            db.Friend.Remove(friend);
            await db.SaveChangesAsync();
            Log.Information($"Delete Friend Success.");
            return Ok("Success");
        }

        [HttpPut]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Put(int id)
        {
            var answer = await db.Friend.FirstOrDefaultAsync(obj => obj.IdFriend == id);

            if (answer == null)
            {
                Log.Information($"Put Friend BadRequest. Равен null.");
                return BadRequest();
            }

            List<Account> accounts = new List<Account>
            {
                await db.Account.FirstOrDefaultAsync(obj => obj.IdAccount == answer.Invited),
                await db.Account.FirstOrDefaultAsync(obj => obj.IdAccount == answer.Inviting)
            };

            var listAccounts = await db.Account.ToListAsync();
            foreach (var item in accounts)
            {
                List<Account> friends = new List<Account>();
                var listFriends = await db.Friend.ToListAsync();
                foreach (var itemFriend in listFriends.Where(obj => obj.Invited == item.IdAccount && obj.BeFriends == true))
                {
                    friends.Add(listAccounts.FirstOrDefault(obj => obj.IdAccount == itemFriend.Inviting));
                }
                foreach (var itemFriend in listFriends.Where(obj => obj.Inviting == item.IdAccount && obj.BeFriends == true))
                {
                    friends.Add(listAccounts.FirstOrDefault(obj => obj.IdAccount == itemFriend.Invited));
                }
                if (friends.Count >= 5)
                {
                    Log.Information($"Put Friend BadRequest. Друзей >= 5.");
                    return BadRequest();
                }
            }

            answer.BeFriends = true;

            await db.SaveChangesAsync();
            Log.Information($"Put Friend Success.");
            return Ok("Success");
        }
    }
}
