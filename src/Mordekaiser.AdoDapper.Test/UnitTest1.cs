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
            Contrasena = "holaputa",
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
        

}

