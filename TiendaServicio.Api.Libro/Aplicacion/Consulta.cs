using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<List<LibroMaterialDTO>> { }


        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibro _contexto;
            private readonly IMapper _mapper;

            public Manejador( ContextoLibro contextoLibro, IMapper mapper)
            {
                _contexto = contextoLibro;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibroMaterial.ToListAsync();
                var librosDTO = _mapper.Map<List<LibroMaterial>,List<LibroMaterialDTO>> (libros);
                return librosDTO;
            }
        }
    }

}
