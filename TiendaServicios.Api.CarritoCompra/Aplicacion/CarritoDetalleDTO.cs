﻿namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDetalleDTO
    {
        public Guid LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get;}
        public DateTime? FechaPublicacion { get; set; }
    }
}
