using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoCADApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCADApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageFilesController : ControllerBase
    {
        private readonly AutoCadContext _context;

        public ImageFilesController(AutoCadContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageFile>>> GetImageFiles()
        {
            return await _context.ImageFiles.Include(f => f.Pins).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageFile>> GetImageFile(int id)
        {
            var file = await _context.ImageFiles.Include(f => f.Pins)
                                                 .ThenInclude(p => p.ModalContent)
                                                 .FirstOrDefaultAsync(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }

        [HttpPost]
        public async Task<ActionResult<ImageFile>> PostImageFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var imageFile = new ImageFile
            {
                FileName = file.FileName,
                FileData = memoryStream.ToArray(),
            };

            _context.ImageFiles.Add(imageFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImageFile), new { id = imageFile.Id }, imageFile);
        }
    }
}
