using Apiintro.Data;
using Apiintro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Apiintro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exisData = await _context.Categories.FindAsync(id);
            if (exisData == null) return NotFound();

            return Ok(exisData);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            var exisData = await _context.Categories.FindAsync(id);
            if (exisData == null) return NotFound();
            _context.Categories.Remove(exisData);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
