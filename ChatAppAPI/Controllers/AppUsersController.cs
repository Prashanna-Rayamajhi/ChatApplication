using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAppAPI.Data;
using ChatAppAPI.Models;
using ChatAppAPI.ViewModel;

namespace ChatAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public AppUsersController(ChatAppContext context)
        {
            _context = context;
        }

  
        // GET: api/AppUsers/5
        [HttpGet("{Email}")]
        public async Task<ActionResult<AppUserDTO>> GetAppUserByEmail(string Email)
        {
            var appUser = await _context.AppUsers.Select(s => new AppUserDTO
            {
                ID = s.ID,
                UserName = s.UserName,
                Email = s.Email
            }).FirstOrDefaultAsync(s => s.Email == Email);
             

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }


        // POST: api/AppUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> PostAppUser(AppUserDTO appUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = new AppUser { ID = appUserDTO.ID, Email = appUserDTO.Email, UserName = appUserDTO.UserName };

            try
            {
                _context.AppUsers.Add(appUser);
                await _context.SaveChangesAsync();

                appUserDTO.ID = appUser.ID;
                return CreatedAtAction("GetAppUserByEmail", new { Email = appUserDTO.Email }, appUserDTO);
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Email should be unique" });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }


        }



        private bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(e => e.ID == id);
        }
    }
}
