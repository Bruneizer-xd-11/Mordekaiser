using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mordekaiser.AdoDapper.Test
{
    public class UnitTest : TestBase
    {
        public UnitTest() : base()
        {
        }

        [Fact]
        public async Task TestAltaServidorAsync()
        {
            // Arrange
            var nuevoServidor = new Servidor
            {
                Nombre = "Servidor_prueba_" + DateTime.Now.Ticks,
                Abreviado = "S" + new Random().Next(10, 99)
            };

            // Act
            await dao.AltaServidorAsync(nuevoServidor);

            // Assert
            var listaServidores = await dao.ObtenerServidoresAsync();
            Assert.Contains(listaServidores, servidor => servidor.Nombre == nuevoServidor.Nombre);
        }

        [Fact]
        public async Task DeleteServidorAsync()
        {
            byte idBajaServidor = 1;

            await dao.DeleteServidorAsync(idBajaServidor);

            var listaServidores = await dao.ObtenerServidoresAsync();
            Assert.DoesNotContain(listaServidores, servidor => servidor.idServidor == idBajaServidor);
        }

        [Fact]

        public async Task ObtenerRangosValorantPorNombreAsync()
        {
            var rangos = (await dao.ObtenerRangosValorantAsync()).ToList();

            Assert.NotEmpty(rangos);
            Assert.Contains(rangos, r => r.Nombre == "Hierro");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ObtenerServidoresPorIdAsync(byte parametro)
        {
            var servidorId = await dao.ObtenerServidorAsync(parametro);
            var listaServidores = await dao.ObtenerServidoresAsync();

            Assert.Contains(listaServidores, servidor => servidor.idServidor == servidorId?.idServidor);
        }

        [Fact]
        public async Task ObtenerRangosValorantPorIDAsync()
        {
            var rangos = (await dao.ObtenerRangosValorantAsync()).ToList();

            Assert.NotEmpty(rangos);
            Assert.Contains(rangos, r => r.idRango == 2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task ObtenerRangoLolPorIDAsync(byte parametro)
        {
            var rangoId = await dao.ObtenerRangoLolAsync(parametro);
            var listaRango = await dao.ObtenerRangosLolAsync();

            Assert.Contains(listaRango, rangosLol => rangosLol.IdRango == rangoId?.IdRango);
        }

        [Fact]
        public async Task TestBajaCuentaLolAsync()
        {
            uint idCuenta = 1;

            await dao.BajaCuentaLolAsync(idCuenta);

            var cuentasLol = await dao.ObtenerCuentasLolAsync();
            Assert.DoesNotContain(cuentasLol, c => c.IdCuenta == idCuenta);
        }

        [Fact]
        public async Task TestBajaCuentaValorantAsync()
        {
            int idCuenta = 1;

            await dao.BajaCuentaValorantAsync(idCuenta);

            var cuentasValorant = await dao.ObtenerCuentasValorantAsync();
            Assert.DoesNotContain(cuentasValorant, c => c.idCuenta == idCuenta);
        }
    }
}
