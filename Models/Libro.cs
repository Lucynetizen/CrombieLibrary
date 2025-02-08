namespace Library.Models
{
    public class Libro
    {
        // Constructor vacío requerido por Entity Framework
        public Libro() { }

        // Constructor con parámetros opcional
        public Libro(string titulo, string autor, int isbn)
        {
            Autor = autor;
            Titulo = titulo;
            ISBN = isbn;
            Disponible = true;
        }

        public int Id { get; set; }
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public int ISBN { get; set; }
        public bool Disponible { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
