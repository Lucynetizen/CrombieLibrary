using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Entity
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Biblioteca> Bibliotecas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Estudiante>("Estudiante")
                .HasValue<Profesor>("Profesor");

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.id_usuario);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            // Configuración de LibrosPrestados para Usuario
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.LibrosPrestados)
                .WithOne()
                .HasForeignKey("UsuarioId")
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Libro
            modelBuilder.Entity<Libro>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Libro>()
                .Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Libro>()
                .Property(l => l.Autor)
                .IsRequired()
                .HasMaxLength(100);

            // Configuración de Biblioteca
            modelBuilder.Entity<Biblioteca>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Biblioteca>()
                .Property(b => b.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            // Relación muchos a muchos entre Biblioteca y Usuario
            modelBuilder.Entity<Biblioteca>()
                .HasMany(b => b.Usuarios)
                .WithMany()
                .UsingEntity(
                    "BibliotecaUsuario",
                    l => l.HasOne(typeof(Usuario)).WithMany().HasForeignKey("UsuarioId"),
                    r => r.HasOne(typeof(Biblioteca)).WithMany().HasForeignKey("BibliotecaId")
                );

            // Relación muchos a muchos entre Biblioteca y Libro
            modelBuilder.Entity<Biblioteca>()
                .HasMany(b => b.Libros)
                .WithMany()
                .UsingEntity(
                    "BibliotecaLibro",
                    l => l.HasOne(typeof(Libro)).WithMany().HasForeignKey("LibroId"),
                    r => r.HasOne(typeof(Biblioteca)).WithMany().HasForeignKey("BibliotecaId")
                );

            // Configuraciones específicas para Estudiante y Profesor
            modelBuilder.Entity<Estudiante>()
                .HasBaseType<Usuario>();

            modelBuilder.Entity<Profesor>()
                .HasBaseType<Usuario>();
        }
    }
}