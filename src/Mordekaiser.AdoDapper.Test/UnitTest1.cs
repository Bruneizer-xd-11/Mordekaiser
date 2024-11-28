namespace Mordekaiser.AdoDapper.Test;

public class UnitTest : TestBase
{

    public UnitTest() : base()
    {
        
    }

    [Fact]
    public void TestAltaservidor()
    {
        // Arrange
        var nuevoServidor = new Servidor
        {
            Nombre = "Servidor_prueba_" + DateTime.Now.Ticks,
            Abreviado = "S" + new Random().Next(10,99)
        };

        // Act
        dao.AltaServidor(nuevoServidor);

        // Assert
        var listaServidores = dao.ObtenerServidores(); 
           Assert.Contains(listaServidores, servidor => servidor.Nombre == nuevoServidor.Nombre);
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
        Assert.Contains(listaRango, rangosLol => rangosLol.IdRango == rangoId?.IdRango);
    }

    [Fact]
    public void TestAltaCuenta()
    {
        // Arrange
        var servidor = dao.ObtenerServidor(2);
        Assert.NotNull(servidor);
        
        var cuenta = new Cuenta
        {
            Servidor = servidor,
            Nombre = "TestUser_" + DateTime.Now.Ticks,
            Contrasena = "TestPass123!",
            Email = $"test_{DateTime.Now.Ticks}@test.com"
        };

        // Act
        dao.AltaCuenta(cuenta);

        // Assert
        var cuentas = dao.ObtenerCuenta();
        Assert.Contains(cuentas, c => c.IdCuenta == cuenta.IdCuenta);
    }

    [Fact]
    public void TestAltaCuentaLol()
    {
        // Arrange
        var servidor = dao.ObtenerServidor(1);
        Assert.NotNull(servidor);
        
        var cuenta = new Cuenta
        {
            Servidor = servidor,
            Nombre = "TestLolAccount_" + DateTime.Now.Ticks,
            Contrasena = "TestPass123!",
            Email = $"test_{DateTime.Now.Ticks}@test.com"
        };
        dao.AltaCuenta(cuenta);
        
        var rangoLol = dao.ObtenerRangoLol(1);
        Assert.NotNull(rangoLol);
        
        var cuentaLol = new CuentaLol
        {
            Cuenta = cuenta,
            RangoLol = rangoLol,
            IdCuenta = cuenta.IdCuenta,
            Nombre = cuenta.Nombre,
            Nivel = 30,
            EsenciaAzul = 1000,
            PuntosRiot = 500,
            PuntosLiga = 100,
            IdRango = rangoLol.IdRango
        };
        
        // Act
        dao.AltaCuentaLol(cuentaLol);

        // Assert
        var cuentasLol = dao.ObtenerCuentasLol();
        Assert.Contains(cuentasLol, c => c.IdCuenta == cuentaLol.IdCuenta);
    }

    [Fact]
    public void TestAltaCuentaValorant()
    {
        // Arrange
        var servidor = dao.ObtenerServidor(1);
        Assert.NotNull(servidor);
        
        var cuenta = new Cuenta
        {
            Servidor = servidor,
            Nombre = "TestValAccount_" + DateTime.Now.Ticks,
            Contrasena = "TestPass123!",
            Email = $"test_{DateTime.Now.Ticks}@test.com"
        };
        dao.AltaCuenta(cuenta);
        
        var rangoValorant = dao.ObtenerRangosValorant().FirstOrDefault();
        Assert.NotNull(rangoValorant);
        
        var cuentaValorant = new CuentaValorant
        {
            Cuenta = cuenta,
            RangoValorant = rangoValorant,
            idCuenta = cuenta.IdCuenta,
            Nombre = cuenta.Nombre,
            Nivel = 20,
            Experiencia = 5000,
            PuntosCompetitivo = 800,
            idRango = rangoValorant.idRango
        };
        
        // Act
        dao.AltaCuentaValorant(cuentaValorant);

        // Assert
        var cuentasValorant = dao.ObtenerCuentasValorant();
        Assert.Contains(cuentasValorant, c => c.idCuenta == cuentaValorant.idCuenta);
    }

    [Fact]
    public void TestLogin()
    {
        // Arrange
        var servidor = dao.ObtenerServidor(1);
        Assert.NotNull(servidor);
        
        var cuenta = new Cuenta
        {
            Servidor = servidor,
            Nombre = "TestLogin_" + DateTime.Now.Ticks,
            Contrasena = "TestPass123!",
            Email = $"test_{DateTime.Now.Ticks}@test.com"
        };
        dao.AltaCuenta(cuenta);
        
        // Act
        var loginResult = dao.Login(cuenta.Nombre, "TestPass123!");
        
        // Assert
        Assert.NotNull(loginResult);
        Assert.Equal(cuenta.Nombre, loginResult.Nombre);
    }

    [Fact]
    public void TestBajaCuentaLol()
    {
        // Arrange
        uint idCuenta = 1;

        // Act
        dao.BajaCuentaLol(idCuenta);

        // Assert
        var cuentasLol = dao.ObtenerCuentasLol();
        Assert.DoesNotContain(cuentasLol, c => c.IdCuenta == idCuenta);
    }

    [Fact]
    public void TestBajaCuentaValorant()
    {
        // Arrange
        int idCuenta = 1;

        // Act
        dao.BajaCuentaValorant(idCuenta);

        // Assert
        var cuentasValorant = dao.ObtenerCuentasValorant();
        Assert.DoesNotContain(cuentasValorant, c => c.idCuenta == idCuenta);
    }

    [Fact]
    public void TestObtenerNivelesLol()
    {
        // Act
        var niveles = dao.ObtenerNivelesLol().ToList();

        // Assert
        Assert.NotEmpty(niveles);
    }

    [Fact]
    public void TestObtenerNivelesValorant()
    {
        // Act
        var niveles = dao.ObtenerNivelesValorant().ToList();

        // Assert
        Assert.NotEmpty(niveles);
    }
}




