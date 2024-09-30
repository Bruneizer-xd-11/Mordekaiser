namespace Mordekaiser.AdoDapper.Test;

public class UnitTest : TestBase
{

    public UnitTest() : base()
    {
        
    }

    [Fact]
    public void TestInsertAndDeleteServidor()
    {
        // Arrange
        var nuevoServidor = new Servidor
        {
            idServidor = 100,
            Nombre = "Servidor_prueba",
            Abreviado = "Sp"
        };

        dao.AltaServidor(nuevoServidor);

        var servidores = dao.ObtenerServidores().ToList();
        Assert.Contains(servidores, servidor => servidor.idServidor == 100);
    }
    [Fact]
    public void AltaCuenta()
    {
        var unservidor = dao.ObtenerServidores().First();
        var nuevaCuentaLol = new Cuenta
        {
            Servidor = unservidor,
            IdCuenta = 1, 
            Nombre = "CuentaLol Test",
            Contrasena = "hola",
            Email = "miguel@gmail.com"
        };


        dao.AltaCuenta(nuevaCuentaLol);
        
        var cuentas = dao.ObtenerCuenta();

        Assert.Contains(cuentas, cuenta => cuenta.IdCuenta == 1);
    }

    [Fact]
    public void BajaServidor()
    {
        byte BajaServidor = 1;

        dao.BajaServidor(BajaServidor);

        var listaServidores = dao.ObtenerServidores();

        Assert.DoesNotContain(listaServidores, servidor => servidor.idServidor == 1);
    }
    [Fact]
    public void ObtenerCuenta()
    {

        var rangos = dao.ObtenerRangosLol().ToList();

        Assert.NotEmpty(rangos); 

        Assert.Contains(rangos, r => r.Nombre == "Oro"); 
    }
        [Fact]
    public void ObtenerRangosValorant()
    {

        var rangos = dao.ObtenerRangosValorant().ToList();

        Assert.NotEmpty(rangos); 

        Assert.Contains(rangos, r => r.Nombre == "Hierro"); 
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    
    public void ObtenerServidoresPorId(byte parametro)
    {
        var servidorId = dao.ObtenerServidor(parametro);
    
        var listaServidores = dao.ObtenerServidores();

        Assert.Contains(listaServidores, servidor => servidor.idServidor == servidorId.idServidor);
    }
}



