using MediatR;
using Microsoft.Extensions.Options;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest<Unit>
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly CarritoContexto _contexto;

            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);

                var valor = await _contexto.SaveChangesAsync(cancellationToken);

                if (valor<=0)
                {
                    throw new Exception("No se pudo registrar la sesion del carrito de compras");
                }
                int NewIdSesion = carritoSesion.carritoSesionId;

                foreach(var ob in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = NewIdSesion,
                        ProductoSelecionado = ob
                    };
                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }
                var valorDetalle = await _contexto.SaveChangesAsync();

                if(valorDetalle>0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo ingresar el detalle carrito de compra");
            }
        }
    }
}
