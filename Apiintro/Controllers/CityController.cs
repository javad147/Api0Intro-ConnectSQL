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
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Cities.Include(c => c.Country).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] City city)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), city);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _context.Cities.Include(c => c.Country).FirstOrDefaultAsync(c => c.Id == id);
            if (city == null) return NotFound();

            return Ok(city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] City city)
        {
            if (id != city.Id) return BadRequest();

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
