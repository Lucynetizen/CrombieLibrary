using Microsoft.AspNetCore.Builder;
using Library.Entity;
using Microsoft.EntityFrameworkCore;
using Library.Services.Implementations;
using Library.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IBibliotecaService, BibliotecaService>();

// **Construir la aplicación después de registrar servicios**
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var LibraryContext= scope.ServiceProvider.GetRequiredService<LibraryContext>();
    if (LibraryContext.Database.CanConnect())
    {
        Console.WriteLine("Conexión exitosa a la base de datos");
        LibraryContext.Database.EnsureCreated();
    }
    else
    {
        Console.WriteLine("No se pudo conectar a la base de datos");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
