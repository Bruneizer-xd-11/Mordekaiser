using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mordekaiser.Core;

public interface IDao
{
    // Métodos de alta
    // void AltaServidor(Servidor servidor);
    Task AltaServidorAsync(Servidor servidor);

    // void AltaCuenta(Cuenta cuenta);
    Task AltaCuentaAsync(Cuenta cuenta);

    // void AltaRangoLol(RangoLol rangoLol);
    Task AltaRangoLolAsync(RangoLol rangoLol);

    // void AltaRangoValorant(RangoValorant rangoValorant);
    Task AltaRangoValorantAsync(RangoValorant rangoValorant);

    // void AltaCuentaLol(CuentaLol cuentaLol);
    Task AltaCuentaLolAsync(CuentaLol cuentaLol);

    // void AltaCuentaValorant(CuentaValorant cuentaValorant);
    Task AltaCuentaValorantAsync(CuentaValorant cuentaValorant);

    // void AltaTipoObjeto(TipoObjeto tipoObjeto);
    Task AltaTipoObjetoAsync(TipoObjeto tipoObjeto);

    // void AltaObjeto(Objeto objeto);
    Task AltaObjetoAsync(Objeto objeto);

    // void AltaInventario(Inventario inventario);
    Task AltaInventarioAsync(Inventario inventario);

    // Métodos de obtención
    // IEnumerable<Servidor> ObtenerServidores();
    Task<IEnumerable<Servidor>> ObtenerServidoresAsync();

    // Servidor? ObtenerServidor(byte a);
    Task<Servidor?> ObtenerServidorAsync(byte a);

    // RangoLol? ObtenerRangoLol(byte b);
    Task<RangoLol?> ObtenerRangoLolAsync(byte b);

    // IEnumerable<RangoLol> ObtenerRangosLol();
    Task<IEnumerable<RangoLol>> ObtenerRangosLolAsync();

    // IEnumerable<RangoValorant> ObtenerRangosValorant();
    Task<IEnumerable<RangoValorant>> ObtenerRangosValorantAsync();

    // IEnumerable<(int IdCuenta, int NivelLol)> ObtenerNivelesLol();
    Task<IEnumerable<(int IdCuenta, int NivelLol)>> ObtenerNivelesLolAsync();

    // IEnumerable<(int IdCuenta, int NivelValorant)> ObtenerNivelesValorant();
    Task<IEnumerable<(int IdCuenta, int NivelValorant)>> ObtenerNivelesValorantAsync();

    // IEnumerable<Cuenta> ObtenerCuenta();
    Task<IEnumerable<Cuenta>> ObtenerCuentaAsync();

    // IEnumerable<CuentaLol> ObtenerCuentasLol();
    Task<IEnumerable<CuentaLol>> ObtenerCuentasLolAsync();

    // IEnumerable<CuentaValorant> ObtenerCuentasValorant();
    Task<IEnumerable<CuentaValorant>> ObtenerCuentasValorantAsync();

    // Métodos de baja
    // void BajaCuentaLol(uint idCuenta);
    Task BajaCuentaLolAsync(uint idCuenta);

    // void BajaCuenta(uint Cuenta);
    Task BajaCuentaAsync(uint Cuenta);

    // void BajaCuentaValorant(int idCuenta);
    Task BajaCuentaValorantAsync(int idCuenta);

    // void BajaServidor(byte idServidor);
    Task BajaServidorAsync(byte idServidor);

    // void BajaObjeto(ushort idObjeto);
    Task BajaObjetoAsync(ushort idObjeto);

    // Método de login
    // Cuenta? Login(string nombreUsuario, string contrasena);
    Task<Cuenta?> LoginAsync(string nombreUsuario, string contrasena);

    // Método de obtención de objetos
    // IEnumerable<Objeto> ObtenerObjetos();
    Task<IEnumerable<Objeto>> ObtenerObjetosAsync();

    // IEnumerable<TipoObjeto> ObtenerTiposObjetos();
    Task<IEnumerable<TipoObjeto>> ObtenerTiposObjetosAsync();
}
