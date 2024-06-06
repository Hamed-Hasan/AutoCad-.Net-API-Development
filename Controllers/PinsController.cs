using AutoCADApi.DbContext;
using AutoCADApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoCADApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PinsController : ControllerBase
    {
        private readonly AutoCadContext _context;

        public PinsController(AutoCadContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pin>> GetPin(int id)
        {
            var pin = await _context.Pins.Include(p => p.ModalContent).FirstOrDefaultAsync(p => p.Id == id);

            if (pin == null)
            {
                return NotFound();
            }

            return pin;
        }

        [HttpPost]
        public async Task<ActionResult<Pin>> PostPin(Pin pin)
        {
            _context.Pins.Add(pin);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPin), new { id = pin.Id }, pin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPin(int id, Pin pin)
        {
            if (id != pin.Id)
            {
                return BadRequest();
            }

            _context.Entry(pin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PinExists(id))
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
        public async Task<IActionResult> DeletePin(int id)
        {
            var pin = await _context.Pins.FindAsync(id);
            if (pin == null)
            {
                return NotFound();
            }

            _context.Pins.Remove(pin);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PinExists(int id)
        {
            return _context.Pins.Any(e => e.Id == id);
        }
    }
}