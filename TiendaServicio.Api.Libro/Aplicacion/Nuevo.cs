using AutoMapper;
using MediatR;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest<Unit>
        {
            public string Titulo { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public Guid AutorLibro { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly ContextoLibro _contexto;

            public Manejador(ContextoLibro contextoLibro)
            {
                _contexto = contextoLibro;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var nuevoLibro = new LibroMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro,
                };

                _contexto.LibroMaterial.Add(nuevoLibro);

                var valor = await _contexto.SaveChangesAsync(cancellationToken);

                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo registrar el libro");
                
            }
        }
    }
}
