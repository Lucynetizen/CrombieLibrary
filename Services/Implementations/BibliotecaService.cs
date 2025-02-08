using Library.Entity;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Implementations
{
    public class BibliotecaService : IBibliotecaService
    {
        private readonly LibraryContext _context;

        public BibliotecaService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Biblioteca> CrearBiblioteca(Biblioteca biblioteca)
        {
            _context.Bibliotecas.Add(biblioteca);
            await _context.SaveChangesAsync();
            return biblioteca;
        }

        public async Task<List<Biblioteca>> ObtenerTodasBibliotecas()
        {
            return await _context.Bibliotecas
                .Include(b => b.Libros)
                .Include(b => b.Usuarios)
                .ToListAsync();
        }

        public async Task<Biblioteca> ObtenerBibliotecaPorId(int id)
        {
            return await _context.Bibliotecas
                .Include(b => b.Libros)
                .Include(b => b.Usuarios)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> AgregarLibro(int bibliotecaId, int libroId)
        {
            var biblioteca = await _context.Bibliotecas
                .Include(b => b.Libros)
                .FirstOrDefaultAsync(b => b.Id == bibliotecaId);
            var libro = await _context.Libros.FindAsync(libroId);

            if (biblioteca == null || libro == null) return false;

            biblioteca.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AgregarUsuario(int bibliotecaId, int usuarioId)
        {
            var biblioteca = await _context.Bibliotecas
                .Include(b => b.Usuarios)
                .FirstOrDefaultAsync(b => b.Id == bibliotecaId);
            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (biblioteca == null || usuario == null) return false;

            biblioteca.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Libro>> ObtenerLibrosDeBiblioteca(int bibliotecaId)
        {
            var biblioteca = await _context.Bibliotecas
                .Include(b => b.Libros)
                .FirstOrDefaultAsync(b => b.Id == bibliotecaId);
            return biblioteca?.Libros.ToList() ?? new List<Libro>();
        }

        public async Task<List<Usuario>> ObtenerUsuariosDeBiblioteca(int bibliotecaId)
        {
            var biblioteca = await _context.Bibliotecas
                .Include(b => b.Usuarios)
                .FirstOrDefaultAsync(b => b.Id == bibliotecaId);
            return biblioteca?.Usuarios.ToList() ?? new List<Usuario>();
        }
    }
}
