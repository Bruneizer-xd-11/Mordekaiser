namespace Mordekaiser.AdoDapper.Test;

public class UnitTest : TestBase
{
    public UnitTest() : base()
    {
        PrepararBaseDeDatos(); 
    }

    [Fact]
    public void TestInsertAndDeleteServidor()
    {
        // Arrange
        var nuevoServidor = new Servidor
        {
            Nombre = "Servidor_prueba",
            Abreviado = "Sp"
        };

        dao.AltaServidor(nuevoServidor);

        var servidores = dao.ObtenerServidores().ToList();
        Assert.Single(servidores);
        Assert.Equal("Servidor_prueba", servidores[0].Nombre); 

        servidores = dao.ObtenerServidores().ToList();
        Assert.Empty(servidores); 
    }
           [Fact]
    public void TestBajaCuentaLol()
    {
        
        var nuevaCuentaLol = new CuentaLol
        {
            IdCuenta = 1, 
            Nombre = "CuentaLol Test",
            Nivel = 10,
            EsenciaAzul = 1000,
            PuntosRiot = 200,
            PuntosLiga = 50
        };


        dao.AltaCuentaLol(nuevaCuentaLol);

        var cuentasLol = dao.ObtenerCuentasLol().ToList();
        Assert.Single(cuentasLol);
        Assert.Equal("CuentaLol Test", cuentasLol[0].Nombre);

        dao.BajaCuentaLol(nuevaCuentaLol.IdCuenta);

        cuentasLol = dao.ObtenerCuentasLol().ToList();
        Assert.Empty(cuentasLol);
    }

    [Fact]
            protected override void PrepararBaseDeDatos()
    {
        Conexion.Execute("DELETE FROM Servidor");
        Conexion.Execute("DELETE FROM Cuenta");
        Conexion.Execute("DELETE FROM CuentaLol");
        Conexion.Execute("DELETE FROM CuentaValorant");
    }
}

