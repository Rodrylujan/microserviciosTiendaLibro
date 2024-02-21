using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Nuevo.Manejador).Assembly));

builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta));

builder.Services.AddDbContext<ContextoLibro>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MiConexion"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
