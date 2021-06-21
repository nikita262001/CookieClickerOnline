using ControllerForCookieClicker.Different;
using CookieClicker.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControllerForCookieClicker.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageIBController : ControllerBase
    {
        DataBaseCookieContext db;
        public ImageIBController()
        {
            db = new DataBaseCookieContext();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageIB>>> Get()
        {
            Log.Information("Get IEnumerable<ImageIB> Success.");
            var images = await db.ImageIB.ToListAsync();
            return images;
        }

        [HttpGet]
        [Route("id={id}")]
        public async Task<ActionResult<ImageIB>> GetTarget(int id)
        {
            var userFind = await db.ImageIB.FirstOrDefaultAsync(obj => obj.IdImageIB == id);
            if (userFind == null)
            {
                Log.Information($"Get ImageIB NotFound.");
                return NotFound();
            }
            Log.Information("Get ImageIB Success.");
            return Ok(userFind);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(ImageIB imageIB)
        {
            if (imageIB == null)
            {
                Log.Information($"Post ImageIB BadRequest. Равен null.");
                return BadRequest();
            }
            imageIB.Version = 1;
            db.ImageIB.Add(imageIB);
            await db.SaveChangesAsync();
            Log.Information($"Post ImageIB Success.");
            return Ok("Success");
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put(ImageIB imageIB)
        {
            var answer = await db.ImageIB.FirstOrDefaultAsync(obj => obj.IdImageIB == imageIB.IdImageIB);
            if (imageIB == null)
            {
                Log.Information($"Put ImageIB BadRequest. Равен null.");
                return BadRequest();
            }
            else if (answer == null)
            {
                Log.Information($"Put ImageIB NotFound.");
                return NotFound();
            }

            answer.Version++;
            answer.ImageInByte = imageIB.ImageInByte;

            await db.SaveChangesAsync();
            Log.Information($"Put ImageIB Success.");
            return Ok("Success");
        }

        [HttpDelete]
        [Route("id={id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var imageIB = await db.ImageIB.FirstOrDefaultAsync(obj => obj.IdImageIB == id);
            if (imageIB == null)
            {
                Log.Information($"Delete ImageIB NotFound.");
                return NotFound();
            }
            db.ImageIB.Remove(imageIB);
            await db.SaveChangesAsync();
            Log.Information($"Delete ImageIB Success.");
            return Ok("Success");
        }
    }
}
