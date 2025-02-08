using Library.Models;

namespace Library.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CrearEstudiante(string nombre, int id);
        Task<Usuario> CrearProfesor(string nombre, int id);
        Task<List<Usuario>> ObtenerTodosUsuarios();
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task<List<Libro>> ObtenerLibrosPrestados(int usuarioId);
        Task<bool> PrestarLibro(int usuarioId, int libroId);
        Task<bool> DevolverLibro(int usuarioId, int libroId);
    }
}
