namespace Mordekaiser.AdoDapper.Test;

public class DaoServidorTest : TestBase
{
    [Fact]
    public void ObtenerServidoresTestOK()
    {
        var servidores = dao.ObtenerServidores();

        Assert.NotEmpty(servidores);
        Assert.Contains(servidores, s=> s.Nombre == "");
    }
}


/*Qué wachin Luis. Cómo vas a dejar tu cuenta abierta*/