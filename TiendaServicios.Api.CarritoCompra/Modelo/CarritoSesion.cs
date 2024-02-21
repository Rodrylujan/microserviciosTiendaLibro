namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public int carritoSesionId { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<CarritoSesionDetalle> ListaDetalles { get; set; }
    }
}
