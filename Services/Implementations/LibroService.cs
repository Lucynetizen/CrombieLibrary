using Library.Entity;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Implementations
{
    public class LibroService : ILibroService
    {
        private readonly LibraryContext _context;

        public LibroService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Libro> CrearLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return libro;
        }

        public async Task<List<Libro>> ObtenerTodosLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        public async Task<Libro> ObtenerLibroPorId(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            return libro ?? throw new Exception($"No se encontró el libro con Id {id}");
        }


        public async Task<bool> ActualizarLibro(Libro libro)
        {
            var libroExistente = await _context.Libros.FindAsync(libro.Id);
            if (libroExistente == null) return false;

            _context.Entry(libroExistente).CurrentValues.SetValues(libro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return false;

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
