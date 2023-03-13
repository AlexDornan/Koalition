using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoalitionServer.Data;
using KoalitionServer.Models;

namespace KoalitionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateChatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrivateChatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrivateChats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrivateChat>>> GetPrivateChats()
        {
            return await _context.PrivateChats.ToListAsync();
        }

        // GET: api/PrivateChats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateChat>> GetPrivateChat(int id)
        {
            var privateChat = await _context.PrivateChats.FindAsync(id);

            if (privateChat == null)
            {
                return NotFound();
            }

            return privateChat;
        }

        // PUT: api/PrivateChats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrivateChat(int id, PrivateChat privateChat)
        {
            if (id != privateChat.PrivateChatId)
            {
                return BadRequest();
            }

            _context.Entry(privateChat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivateChatExists(id))
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

        // POST: api/PrivateChats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrivateChat>> PostPrivateChat(PrivateChat privateChat)
        {
            _context.PrivateChats.Add(privateChat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrivateChat", new { id = privateChat.PrivateChatId }, privateChat);
        }

        // DELETE: api/PrivateChats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrivateChat(int id)
        {
            var privateChat = await _context.PrivateChats.FindAsync(id);
            if (privateChat == null)
            {
                return NotFound();
            }

            _context.PrivateChats.Remove(privateChat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrivateChatExists(int id)
        {
            return _context.PrivateChats.Any(e => e.PrivateChatId == id);
        }
    }
}
