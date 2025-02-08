using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Entity;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BibliotecaController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Biblioteca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biblioteca>>> GetBibliotecas()
        {
            return await _context.Bibliotecas.ToListAsync();
        }

        // GET: api/Biblioteca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Biblioteca>> GetBiblioteca(int id)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync(id);
            if (biblioteca == null) return NotFound();
            return biblioteca;
        }

        // POST: api/Biblioteca
        [HttpPost]
        public async Task<ActionResult<Biblioteca>> PostBiblioteca(Biblioteca biblioteca)
        {
            _context.Bibliotecas.Add(biblioteca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBiblioteca), new { id = biblioteca.Id }, biblioteca);
        }

        // PUT: api/Biblioteca/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBiblioteca(int id, Biblioteca biblioteca)
        {
            if (id != biblioteca.Id) return BadRequest();

            _context.Entry(biblioteca).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Biblioteca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBiblioteca(int id)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync(id);
            if (biblioteca == null) return NotFound();

            _context.Bibliotecas.Remove(biblioteca);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
