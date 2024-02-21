using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ILibroService,LibroService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Nuevo.Manejador).Assembly));

var baseAdres = builder.Configuration["Services:Libros"];

if(baseAdres != null)
{
    builder.Services.AddHttpClient("Libros", cfg =>
    {
        cfg.BaseAddress = new Uri(baseAdres);
    });
}


var connectionString = builder.Configuration.GetConnectionString("MiConexion");

if (connectionString != null)
{
    builder.Services.AddDbContext<CarritoContexto>(opt =>
    {
        opt.UseMySQL(connectionString);
    });
}


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
