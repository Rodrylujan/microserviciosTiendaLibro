using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest<Unit>
        { 
            public required string Nombre { get; set; }
            public required string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set;}
        }

        public class Manejador : IRequestHandler<Ejecuta,Unit>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            async Task<Unit> IRequestHandler<Ejecuta, Unit>.Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombres = request.Nombre,
                    Apellidos = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString(),
                };
                _contexto.autorLibro.Add(autorLibro);

                var valor = await _contexto.SaveChangesAsync();


                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo registrar");
            }
        }
    }
}
