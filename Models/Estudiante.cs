namespace Library.Models
{
    public class Estudiante : Usuario
    {
        public Estudiante() { }
        public Estudiante(string nombre, int id)
        {
            Nombre = nombre;
            id_usuario = id;
            LibrosPrestados = new List<Libro>();
            LimiteLibros = 3;
        }
    }
}
