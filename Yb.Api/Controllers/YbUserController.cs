using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yb.Api.Data;
using Yb.Api.Models;

namespace Yb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YbUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public YbUserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<YbUser>>> Get()
        {
            return await _context.YbUsers.ToListAsync();
        }
    }
}