﻿namespace TiendaServicio.Api.Libro.Modelo
{
    public class LibroMaterial
    {
        public Guid LibroMaterialId { get; set; }
        public string Titulo { get; set; }  
        public DateTime FechaPublicacion { get; set; }

        public Guid AutorLibro { get; set; }
    }
}
