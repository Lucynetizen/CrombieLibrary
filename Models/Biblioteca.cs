namespace Library.Models
{
    public class Biblioteca
    {
        
        public int Id { get; set; }  
        public string Nombre { get; set; }  
        public string Direccion { get; set; }  
        public string Telefono { get; set; }
        public ICollection<Libro> Libros { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

        public Biblioteca()
        {
            Libros = new List<Libro>();
            Usuarios = new List<Usuario>();
        }
    }
}
