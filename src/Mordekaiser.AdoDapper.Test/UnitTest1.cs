using System.Linq;
using Xunit;
using Mordekaiser.Core;

namespace Mordekaiser.AdoDapper.Test
{
    public class UnitTest : TestBase
    {
        public UnitTest() : base()
        {
            PrepararBaseDeDatos(); // Limpia o prepara la base de datos antes de cada prueba
        }

        [Fact]
        public void TestInsertAndDeleteServidor()
        {
            // Arrange
            var nuevoServidor = new Servidor
            {
                Nombre = "Servidor Test",
                Abreviado = "ST"
            };

            // Act - Insertar el nuevo servidor
            dao.AltaServidor(nuevoServidor);

            // Assert - Verificar que el servidor se insertó correctamente
            var servidores = dao.ObtenerServidores().ToList();
            Assert.Single(servidores); // Debería haber solo un servidor
            Assert.Equal("Servidor Test", servidores[0].Nombre);
            
            // Act - Bajar el servidor
            // Implementar el método de eliminación en IDao y DaoDapper
            // dao.BajaServidor(nuevoServidor.IdServidor); // Descomentar cuando exista el método
            
            // Assert - Verificar que el servidor fue eliminado
            servidores = dao.ObtenerServidores().ToList();
            Assert.Empty(servidores); // La lista de servidores debería estar vacía
        }

        [Fact]
        public void TestInsertAndDeleteCuenta()
        {
            // Arrange
            var nuevaCuenta = new Cuenta
            {
                Nombre = "Cuenta Test",
                Contrasena = "Contrasena123",
                Email = "test@example.com"
            };

  
            dao.AltaCuenta(nuevaCuenta);
        }



        // Método para limpiar la base de datos después de cada prueba
        protected override void PrepararBaseDeDatos()
        {
            // Aquí puedes limpiar las tablas específicas para asegurarte de que las pruebas sean independientes
            Conexion.Execute("DELETE FROM Servidor");
            Conexion.Execute("DELETE FROM Cuenta"); // Asumiendo que hay una tabla Cuenta
        }
    }
}
