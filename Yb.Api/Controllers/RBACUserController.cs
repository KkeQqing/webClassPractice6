using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Api.Models;
using Yb.Api.Data;
using Yb.Api.Models;

namespace Yb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RBACUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RBACUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RBACUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RBACUser>>> GetRBACUsers()
        {
            return await _context.RBACUsers.ToListAsync();
        }

        // GET: api/RBACUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RBACUser>> GetRBACUser(string id)
        {
            var user = await _context.RBACUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST: api/RBACUser
        [HttpPost]
        public async Task<ActionResult<RBACUser>> PostRBACUser(RBACUser user)
        {
            _context.RBACUsers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRBACUser), new { id = user.Id }, user);
        }

        // PUT: api/RBACUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRBACUser(string id, RBACUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RBACUserExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/RBACUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRBACUser(string id)
        {
            var user = await _context.RBACUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.RBACUsers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RBACUserExists(string id)
        {
            return _context.RBACUsers.Any(e => e.Id == id);
        }
    }
}