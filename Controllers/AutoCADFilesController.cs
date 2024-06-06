using AutoCADApi.DbContext;
using AutoCADApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoCADApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoCADFilesController : ControllerBase
    {
        private readonly AutoCadContext _context;

        public AutoCADFilesController(AutoCadContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutoCADFile>>> GetAutoCADFiles()
        {
            return await _context.AutoCADFiles.Include(f => f.Pins).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutoCADFile>> GetAutoCADFile(int id)
        {
            var file = await _context.AutoCADFiles.Include(f => f.Pins)
                                                 .ThenInclude(p => p.ModalContent)
                                                 .FirstOrDefaultAsync(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }

        [HttpPost]
        public async Task<ActionResult<AutoCADFile>> PostAutoCADFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var autoCADFile = new AutoCADFile
            {
                FileName = file.FileName,
                FileData = memoryStream.ToArray()
            };

            _context.AutoCADFiles.Add(autoCADFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutoCADFile), new { id = autoCADFile.Id }, autoCADFile);
        }
    }
}
