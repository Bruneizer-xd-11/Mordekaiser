namespace Mordekaiser.Core;

public interface IDao
{
    void AltaServidor(Servidor servidor);
    IEnumerable<Servidor> ObtenerServidores();
}
