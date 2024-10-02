namespace Mordekaiser.AdoDapper.Test;

public class UnitTest : TestBase
{

    public UnitTest() : base()
    {
        
    }

    [Fact]
    public void TestAltaservidor()
    {
        var nuevoServidor = new Servidor
        {
            idServidor = 100,
            Nombre = "Servidor_prueba",
            Abreviado = "Sp"
        };

        dao.AltaServidor(nuevoServidor);

        // Assert
        var listaServidores = dao.ObtenerServidores(); 
        Assert.Contains(listaServidores, servidor => servidor.idServidor == nuevoServidor.idServidor);
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
    public void ObtenerRangosValorantPorNombre()
    {

        var rangos = dao.ObtenerRangosValorant().ToList();

        Assert.NotEmpty(rangos); 

        Assert.Contains(rangos, r => r.Nombre == "Hierro"); 
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]

    
    public void ObtenerServidoresPorId(byte parametro)
    {
        var servidorId = dao.ObtenerServidor(parametro);
    
        var listaServidores = dao.ObtenerServidores();

        Assert.Contains(listaServidores, servidor => servidor.idServidor == servidorId.idServidor);
    }
    [Fact]
    public void ObtenerRangosValorantPorID()
    {

        var rangos = dao.ObtenerRangosValorant().ToList();

        Assert.NotEmpty(rangos); 

        Assert.Contains(rangos, r => r.idRango == 2); 
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    
    public void ObtenerRangoLolPorID(byte parametro)
    {
        var rangoId = dao.ObtenerRangoLol(parametro);
    
        var listaRango = dao.ObtenerRangosLol();

        Assert.Contains(listaRango, rangosLol => rangosLol.IdRango == rangoId.  IdRango);
    }
    
}





