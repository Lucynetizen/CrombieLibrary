using System.Globalization;

namespace Library.Models
{
    public class Usuario
    {
        public Usuario() { }
        public string Nombre { get; set; }
        public int id_usuario { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int LimiteLibros { get; set; }
        public virtual ICollection<Libro> LibrosPrestados { get; set; }
    }
}
