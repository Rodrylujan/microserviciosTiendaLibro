namespace TiendaServicios.Api.Autor.Modelo
{
    public class AutorDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
