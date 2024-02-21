using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta 
    {
        public class Ejecuta : IRequest<CarritoDTO>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibroService _libroService;

            public Manejador(CarritoContexto contexto, ILibroService libroService)
            {
                _contexto = contexto;
                _libroService = libroService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.carritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaCarritoDTO = new List<CarritoDetalleDTO>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSelecionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalle = new CarritoDetalleDTO
                        {
                            TituloLibro=objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,    
                            LibroId = objetoLibro.LibroMaterialId
                        };
                        listaCarritoDTO.Add(carritoDetalle);
                    }
                }
                var carritoSesionDTO = new CarritoDTO
                {
                    CarritoDTOId = carritoSesion.carritoSesionId,
                    FechaCreacionSesio = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDTO
                };
                return carritoSesionDTO;
            }
        }
    }
}
