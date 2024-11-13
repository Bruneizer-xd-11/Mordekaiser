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
}
 /*   [Fact]
    public void TestAltaObjeto()
    {
        // Arrange
        var tipoObjeto = new TipoObjeto { idTipoObjeto = 1, Nombre = "Skins" };
        dao.AltaTipoObjeto(tipoObjeto);

        var objeto = new Objeto(tipoObjeto)
        {
            TipoObjeto = tipoObjeto, // Asegúrate de incluir esta línea
            idObjeto = 1,
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
        Assert.Contains(objetos, o => o.idObjeto == objeto.idObjeto && o.Nombre == "SkinTest");
    }

    [Fact]
    public void TestBajaObjeto()
    {
        // Arrange
        var tipoObjeto = new TipoObjeto { idTipoObjeto = 2, Nombre = "Centinelas" };
        dao.AltaTipoObjeto(tipoObjeto);

        var objeto = new Objeto(tipoObjeto)
        {
            TipoObjeto = tipoObjeto,
            idObjeto = 1,
            Nombre = "SkinTest",
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
*/




