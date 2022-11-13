using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetsChatApplication.Model;
using LetsChatApplication.Hub;
using Microsoft.AspNetCore.SignalR;

namespace LetsChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInfoController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public UsersInfoController(ChatContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/UsersInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersInfo>>> GetUsersInfo()
        {
            return await _context.UsersInfo.ToListAsync();
        }

        // GET: api/UsersInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersInfo>> GetUsersInfo(int id)
        {
            var usersInfo = await _context.UsersInfo.FindAsync(id);

            if (usersInfo == null)
            {
                return NotFound();
            }

            return usersInfo;
        }

        // PUT: api/UsersInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersInfo(int id, UsersInfo usersInfo)
        {
            if (id != usersInfo.UsersId)
            {
                return BadRequest();
            }

            _context.Entry(usersInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersInfo>> PostUsersInfo(UsersInfo usersInfo)
        {
            _context.UsersInfo.Add(usersInfo);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.BroadcastMessage();


            return CreatedAtAction("GetUsersInfo", new { id = usersInfo.UsersId }, usersInfo);
        }

        // DELETE: api/UsersInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersInfo(int id)
        {
            var usersInfo = await _context.UsersInfo.FindAsync(id);
            if (usersInfo == null)
            {
                return NotFound();
            }

            _context.UsersInfo.Remove(usersInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersInfoExists(int id)
        {
            return _context.UsersInfo.Any(e => e.UsersId == id);
        }
    }
}
