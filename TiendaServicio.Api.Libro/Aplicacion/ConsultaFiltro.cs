using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico: IRequest<LibroMaterialDTO>
        {
            public Guid LibroMaterialId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {
            private readonly ContextoLibro _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibro contexto,IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibroMaterial.Where(x => x.LibroMaterialId == request.LibroMaterialId).FirstOrDefaultAsync(cancellationToken);
                if (libro == null)
                {
                    throw new Exception($"No se pudo recuperar el libro con el id {request.LibroMaterialId}");
                }
                var libroDTO = _mapper.Map<LibroMaterial,LibroMaterialDTO>(libro);
                return libroDTO;
            }
        }
    }
}
