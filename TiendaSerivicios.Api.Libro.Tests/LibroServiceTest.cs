using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;
using Xunit;
using GenFu;
using Microsoft.EntityFrameworkCore;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibroServiceTest
    {
        private IEnumerable<LibroMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibroMaterial>().Fill(x => x.Titulo).AsArticleTitle().Fill(x => x.LibroMaterialId, () => { return Guid.NewGuid(); });

            var lista =A.ListOf<LibroMaterial>(30);

            lista[0].LibroMaterialId = Guid.Empty;

            return lista;   
        }

        private Mock<ContextoLibro> CreaContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbset = new Mock<DbSet<LibroMaterial>>();

            dbset.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbset.As<IQueryable<LibroMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbset.As<IQueryable<LibroMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbset.As<IQueryable<LibroMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());
        }

        [Fact]
        public void GetLibros() 
        {
            var mockContexto = new Mock<ContextoLibro>();
            var mockMapping = new Mock<IMapper>();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object,mockMapping.Object);

        }
    }
}
