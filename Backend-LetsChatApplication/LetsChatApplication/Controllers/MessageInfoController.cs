using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsChatApplication.Hub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetsChatApplication.Model;
using Microsoft.AspNetCore.SignalR;

namespace LetsChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageInfoController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext; 

        public MessageInfoController(ChatContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            
            _context = context;
            _hubContext = hubContext;
            
        }

        // GET: api/MessageInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageInfo>>> GetMessageInfo()
        {
            
            return await _context.MessageInfo.ToListAsync();
           
        }

        // GET: api/MessageInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageInfo>> GetMessageInfo(int id)
        {
            var messageInfo = await _context.MessageInfo.FindAsync(id);

            if (messageInfo == null)
            {
                return NotFound();
            }

            return messageInfo;
        }

        // PUT: api/MessageInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageInfo(int id, MessageInfo messageInfo)
        {
            if (id != messageInfo.MsgId)
            {
                return BadRequest();
            }

            _context.Entry(messageInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageInfoExists(id))
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

        // POST: api/MessageInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageInfo>> PostMessageInfo(MessageInfo messageInfo)
        {
                _context.MessageInfo.Add(messageInfo);

            
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.BroadcastMessage(); 
            
            

            return CreatedAtAction("GetMessageInfo", new { id = messageInfo.MsgId }, messageInfo);
        } 

        // DELETE: api/MessageInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageInfo(int id)
        {
            var messageInfo = await _context.MessageInfo.FindAsync(id);
            if (messageInfo == null)
            {
                return NotFound();
            }

            _context.MessageInfo.Remove(messageInfo);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.BroadcastMessage(); 

            return NoContent();
        }

        private bool MessageInfoExists(int id)
        {
            return _context.MessageInfo.Any(e => e.MsgId == id);
        }
    }
}
