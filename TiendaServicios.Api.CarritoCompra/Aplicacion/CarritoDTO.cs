namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDTO
    {
        public int CarritoDTOId { get; set; }
        public DateTime FechaCreacionSesio { get; set; }
        public List<CarritoDetalleDTO> ListaProductos { get; set; }
    }
}
