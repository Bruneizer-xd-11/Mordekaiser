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
            idServidor = 100,
            Nombre = "Servidor_prueba",
            Abreviado = "Sp"
        };

        // Act
        dao.AltaServidor(nuevoServidor);

        // Assert
        var listaServidores = dao.ObtenerServidores(); 
           Assert.Contains(listaServidores, servidor => servidor.idServidor == nuevoServidor.idServidor);
        // Asegúrate de que el servidor no exista antes de la prueba
        dao.BajaServidor(nuevoServidor.idServidor); // Elimina el servidor si ya existe
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
    public void TestAltaObjeto()
    {
        var tipoObjeto = new TipoObjeto { 
            Nombre = "Skins_Test_" + DateTime.Now.Ticks 
        };
        dao.AltaTipoObjeto(tipoObjeto);
    
        var objeto = new Objeto(tipoObjeto)
        {
            TipoObjeto = tipoObjeto,
            Nombre = "SkinTest",
            PrecioEA = 1000,
            PrecioRP = 500,
            Venta = 1500,
            idTipoObjeto = tipoObjeto.idTipoObjeto
        };
    
        // Act
        dao.AltaObjeto(objeto);
        var objetos = dao.ObtenerObjetos();
        
        // Assert
        Assert.Contains(objetos, o => o.Nombre == objeto.Nombre);
    }
    [Fact]
    public void TestBajaObjeto()
    {
        // Asegúrate de que el TipoObjeto se inserte correctamente
        var tipoObjeto = new TipoObjeto { 
            idTipoObjeto = 100, // Asegúrate de que este ID no esté en uso
            Nombre = "Centinelas_Test_" + DateTime.Now.Ticks
        };
        dao.AltaTipoObjeto(tipoObjeto);

        // Ahora crea el objeto usando el tipoObjeto recién insertado
        var objeto = new Objeto(tipoObjeto)
        {
            TipoObjeto = tipoObjeto,
            idObjeto = 101,
            Nombre = "SkinTest_" + DateTime.Now.Ticks,
            PrecioEA = 1000,
            PrecioRP = 500,
            Venta = 1500,
            idTipoObjeto = tipoObjeto.idTipoObjeto
        };

        // Act
        dao.AltaObjeto(objeto);
        dao.BajaObjeto(objeto.idObjeto);
        var objetos = dao.ObtenerObjetos();

        // Assert
        Assert.DoesNotContain(objetos, o => o.idObjeto == objeto.idObjeto);
    }
}





