using autoCadApiDevelopment.Data;
using autoCadApiDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace autoCadApiDevelopment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrawingsController : ControllerBase
    {
        private readonly AutoCadContext _context;

        public DrawingsController(AutoCadContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drawing>>> GetDrawings()
        {
            return await _context.Drawings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Drawing>> GetDrawing(int id)
        {
            var drawing = await _context.Drawings.FindAsync(id);

            if (drawing == null)
            {
                return NotFound();
            }

            return drawing;
        }

        [HttpPost]
        public async Task<ActionResult<Drawing>> PostDrawing(Drawing drawing)
        {
            _context.Drawings.Add(drawing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrawing), new { id = drawing.Id }, drawing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrawing(int id, Drawing drawing)
        {
            if (id != drawing.Id)
            {
                return BadRequest();
            }

            _context.Entry(drawing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrawingExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrawing(int id)
        {
            var drawing = await _context.Drawings.FindAsync(id);
            if (drawing == null)
            {
                return NotFound();
            }

            _context.Drawings.Remove(drawing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrawingExists(int id)
        {
            return _context.Drawings.Any(e => e.Id == id);
        }
    }
}
