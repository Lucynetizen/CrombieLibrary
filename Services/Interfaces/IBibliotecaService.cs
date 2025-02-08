using Library.Models;

namespace Library.Services.Interfaces
{
    public interface IBibliotecaService
    {
        Task<Biblioteca> CrearBiblioteca(Biblioteca biblioteca);
        Task<List<Biblioteca>> ObtenerTodasBibliotecas();
        Task<Biblioteca> ObtenerBibliotecaPorId(int id);
        Task<bool> AgregarLibro(int bibliotecaId, int libroId);
        Task<bool> AgregarUsuario(int bibliotecaId, int usuarioId);
        Task<List<Libro>> ObtenerLibrosDeBiblioteca(int bibliotecaId);
        Task<List<Usuario>> ObtenerUsuariosDeBiblioteca(int bibliotecaId);
    }
}
