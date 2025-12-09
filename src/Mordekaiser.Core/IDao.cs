namespace Mordekaiser.Core;

public interface IDao
{

    Task AltaServidorAsync(Servidor servidor);
    Task BorrarTodosServidoresAsync();
    Task AltaCuentaAsync(Cuenta cuenta);
Task<Cuenta?> BuscarCuentaPorEmailAsync(string email);
Task<Cuenta?> BuscarCuentaPorNombreAsync(string nombre)   ;
 Task AltaRangoLolAsync(RangoLol rangoLol);

Task<CuentaValorant?> ObtenerCuentaValorantPorIdAsync(int id);
    Task AltaRangoValorantAsync(RangoValorant rangoValorant);
 Task<CuentaLol?> ObtenerCuentaLolPorIdAsync(uint idCuentaLol);

    Task AltaCuentaLolAsync(CuentaLol cuentaLol);

    Task AltaCuentaValorantAsync(CuentaValorant cuentaValorant);

    Task AltaTipoObjetoAsync(TipoObjeto tipoObjeto);


    Task AltaObjetoAsync(Objeto objeto);


    Task AltaInventarioAsync(Inventario inventario);

    Task<IEnumerable<Servidor>> ObtenerServidoresAsync();


    Task<Servidor?> ObtenerServidorAsync(byte a);

    Task<RangoLol?> ObtenerRangoLolAsync(byte b);


    Task<IEnumerable<RangoLol>> ObtenerRangosLolAsync();


    Task<IEnumerable<RangoValorant>> ObtenerRangosValorantAsync();
    Task<IEnumerable<(int IdCuenta, int NivelLol)>> ObtenerNivelesLolAsync();
    Task<IEnumerable<(int IdCuenta, int NivelValorant)>> ObtenerNivelesValorantAsync();

    Task<IEnumerable<Cuenta>> ObtenerCuentaAsync();

    Task<IEnumerable<CuentaLol>> ObtenerCuentasLolAsync();

    Task<IEnumerable<CuentaValorant>> ObtenerCuentasValorantAsync();

    Task BajaCuentaLolAsync(uint idCuenta);
    Task <int >DeleteCuentaAsync(uint Cuenta);
    Task BajaCuentaValorantAsync(int idCuenta);

    Task<int> DeleteServidorAsync(byte idServidor);

    Task BajaObjetoAsync(ushort idObjeto);

    Task<Cuenta?> LoginAsync(string nombreUsuario, string contrasena);

    Task<IEnumerable<Objeto>> ObtenerObjetosAsync();
    Task<IEnumerable<TipoObjeto>> ObtenerTiposObjetosAsync();
}
