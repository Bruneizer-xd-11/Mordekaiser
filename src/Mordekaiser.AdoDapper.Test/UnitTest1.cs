namespace Mordekaiser.AdoDapper.Test;

public class DaoServidorTest : TestBase
{
    [Fact]
    public void ObtenerServidoresTestOK()
    {
        var servidores = Dao.ObtenerServidores();

        Assert.NotEmpty(servidores);
        Assert.Contains(servidores, s=> s.Nombre == "");
    }
}