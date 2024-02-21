using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Modelo;

namespace TiendaServicio.Api.Libro.Persistencia
{
    public class ContextoLibro: DbContext
    {
        public ContextoLibro(DbContextOptions<ContextoLibro> options): base(options) { }

        public DbSet<LibroMaterial> LibroMaterial { get; set; }
    }
}
