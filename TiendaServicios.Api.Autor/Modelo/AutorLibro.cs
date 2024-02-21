namespace TiendaServicios.Api.Autor.Modelo
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento {  get; set; }

        public ICollection<GradoAcademico> ListaGradoAcademicos { get; set;}

        public string AutorLibroGuid { get; set; }
    }
}
