using Library.Entity;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LibraryContext _context;

        public UsuarioService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CrearEstudiante(string nombre, int id)
        {
            var estudiante = new Estudiante(nombre, id);
            _context.Usuarios.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<Usuario> CrearProfesor(string nombre, int id)
        {
            var profesor = new Profesor(nombre, id);
            _context.Usuarios.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<List<Usuario>> ObtenerTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario?? throw new Exception("Usuario no encontrado");
        }

        public async Task<List<Libro>> ObtenerLibrosPrestados(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.LibrosPrestados)
                .FirstOrDefaultAsync(u => u.id_usuario == usuarioId);
            return usuario?.LibrosPrestados.ToList() ?? new List<Libro>();
        }

        public async Task<bool> PrestarLibro(int usuarioId, int libroId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.LibrosPrestados)
                .FirstOrDefaultAsync(u => u.id_usuario == usuarioId);
            var libro = await _context.Libros.FindAsync(libroId);

            if (usuario == null || libro == null) return false;
            if (usuario.LibrosPrestados.Count >= usuario.LimiteLibros) return false;

            usuario.LibrosPrestados.Add(libro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DevolverLibro(int usuarioId, int libroId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.LibrosPrestados)
                .FirstOrDefaultAsync(u => u.id_usuario == usuarioId);
            var libro = await _context.Libros.FindAsync(libroId);

            if (usuario == null || libro == null) return false;

            var resultado = usuario.LibrosPrestados.Remove(libro);
            if (resultado) await _context.SaveChangesAsync();
            return resultado;
        }
    }
}
