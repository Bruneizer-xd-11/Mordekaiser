namespace Mordekaiser.Core
{
    public interface IDao
    {
        Task AltaCuentaAsync(Cuenta cuenta);
        Task<Cuenta?> BuscarCuentaPorEmailAsync(string email);
        Task<Cuenta?> BuscarCuentaPorNombreAsync(string nombre);


        Task<Cuenta?> ObtenerCuentaPorIdAsync(int idCuenta);
        Task<int> BajaCuentaAsync(int idCuenta);
        Task<IEnumerable<CuentaLol>> ObtenerCuentasLolAsync();
        Task<CuentaLol?> ObtenerCuentaLolPorIdAsync(uint idCuentaLol);

        Task AltaCuentaLolAsync(CuentaLol cuentaLol);
        Task BajaCuentaLolAsync(uint idCuentaLol);

        Task AltaRangoLolAsync(RangoLol rangoLol);
        Task<RangoLol?> ObtenerRangoLolAsync(byte idRangoLol);
        Task<IEnumerable<RangoLol>> ObtenerRangosLolAsync();
        Task<IEnumerable<CuentaValorant>> ObtenerCuentasValorantAsync();
        Task<CuentaValorant?> ObtenerCuentaValorantPorIdAsync(int id);

        Task AltaCuentaValorantAsync(CuentaValorant cuentaValorant);
        Task BajaCuentaValorantAsync(int idCuenta);

        Task AltaRangoValorantAsync(RangoValorant rangoValorant);
        Task<IEnumerable<RangoValorant>> ObtenerRangosValorantAsync();

        Task<IEnumerable<Servidor>> ObtenerServidoresAsync();
        Task<Servidor?> ObtenerServidorAsync(byte id);
        Task AltaServidorAsync(Servidor servidor);
        Task<int> DeleteServidorAsync(byte idServidor);
        Task BorrarTodosServidoresAsync();
        Task<IEnumerable<Objeto>> ObtenerObjetosAsync();
        Task<IEnumerable<TipoObjeto>> ObtenerTiposObjetosAsync();

        Task AltaTipoObjetoAsync(TipoObjeto tipoObjeto);
        Task AltaObjetoAsync(Objeto objeto);
        Task AltaInventarioAsync(Inventario inventario);
        Task BajaObjetoAsync(ushort idObjeto);

        Task<Cuenta?> LoginAsync(string nombreUsuario, string contrasena);

        Task<IEnumerable<(int IdCuenta, int NivelLol)>> ObtenerNivelesLolAsync();
        Task<IEnumerable<(int IdCuenta, int NivelValorant)>> ObtenerNivelesValorantAsync();

        Task<IEnumerable<Cuenta>> ObtenerCuentaAsync();
    }
}
