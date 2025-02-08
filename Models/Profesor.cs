namespace Library.Models
{
    public class Profesor : Usuario
    {
        public Profesor() { }
        public Profesor(string nombre, int id)
        {
            Nombre = nombre;
            id_usuario = id;
            LibrosPrestados = new List<Libro>();
            LimiteLibros = 5;
        }
    }
}
