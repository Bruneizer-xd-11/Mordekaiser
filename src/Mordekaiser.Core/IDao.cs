namespace Mordekaiser.Core;

public interface IDao
{
    void AltaServidor(Servidor servidor);
    IEnumerable<Servidor> ObtenerServidores();

    void AltaCuenta(Cuenta cuenta);
    IEnumerable<Cuenta> obtenercuenta();
}

/*Qué wachin Luis. Cómo vas a dejar tu cuenta abierta*/