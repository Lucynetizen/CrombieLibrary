using Library.Models;

namespace Library.Services.Interfaces
{
    public interface ILibroService
    {
        Task<Libro> CrearLibro(Libro libro);
        Task<List<Libro>> ObtenerTodosLibros();
        Task<Libro> ObtenerLibroPorId(int id);
        Task<bool> ActualizarLibro(Libro libro);
        Task<bool> EliminarLibro(int id);
    }
}
