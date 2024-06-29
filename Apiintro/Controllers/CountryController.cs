using Microsoft.AspNetCore.Mvc;
using Apiintro.Data;
using Apiintro.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Apiintro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Countries.Include(c => c.Cities).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country country)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), country);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var country = await _context.Countries.Include(c => c.Cities).FirstOrDefaultAsync(c => c.Id == id);
            if (country == null) return NotFound();

            return Ok(country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Country country)
        {
            if (id != country.Id) return BadRequest();

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return NotFound();

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
