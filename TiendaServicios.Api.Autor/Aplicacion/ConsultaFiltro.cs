using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico: IRequest<AutorDTO> {
            public required string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador( ContextoAutor contextoAutor, IMapper mapper)
            {
                _contexto = contextoAutor;
                _mapper = mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.autorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync(cancellationToken);
                if (autor == null)
                {
                    throw new Exception("No se encontro el Autor");
                }
                var autorDTO = _mapper.Map<AutorLibro, AutorDTO>(autor);
                return autorDTO;
            }
        }
    }
}
