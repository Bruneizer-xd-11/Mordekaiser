using System.Data;
using Dapper;
using Mordekaiser.Core;

namespace Mordekaiser.AdoDapper;

public class DaoDapper : IDao
{
    private readonly IDbConnection _conexion;

    public DaoDapper(IDbConnection conexion) => _conexion = conexion;

    public void AltaServidor(Servidor servidor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnidServidor", servidor.idServidor);
        parametros.Add("@UnNombre", servidor.Nombre);
        parametros.Add("@UnAbreviado", servidor.Abreviado);

        _conexion.Execute("InsertServidor", parametros, commandType: CommandType.StoredProcedure);
        // servidor.IdServidor = parametros.Get<byte>("@IdServidor");
    }

    public void AltaCuenta(Cuenta cuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnidCuenta", cuenta.IdCuenta);
        parametros.Add("@UnidServidor", cuenta.Servidor.idServidor);
        parametros.Add("@UnNombre", cuenta.Nombre);
        parametros.Add("@Uncontrasena", cuenta.Contrasena);
        parametros.Add("@UneMail", cuenta.Email);
        _conexion.Execute("InsertCuenta", parametros, commandType: CommandType.StoredProcedure);
    }

    public void AltaRangoLol(RangoLol rangoLol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdRango", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", rangoLol.Nombre);
        parametros.Add("@Numero", rangoLol.Numero);
        parametros.Add("@PuntosLigaNecesarios", rangoLol.PuntosLigaNecesarios);

        _conexion.Execute("InsertRangoLol", parametros, commandType: CommandType.StoredProcedure);
        rangoLol.IdRango = parametros.Get<byte>("@IdRango"); // Asegúrate de que sea el tipo correcto
    }

    public void AltaRangoValorant(RangoValorant rangoValorant)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdRango", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", rangoValorant.Nombre);
        parametros.Add("@Numero", rangoValorant.Numero);
        parametros.Add("@PuntosCompetitivo", rangoValorant.PuntosCompetitivo);

        _conexion.Execute("InsertRangoValorant", parametros, commandType: CommandType.StoredProcedure);
        rangoValorant.idRango = parametros.Get<ushort>("@IdRango");
    }

    public void AltaCuentaLol(CuentaLol cuentaLol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@idCuenta", cuentaLol.IdCuenta);
        parametros.Add("@Nombre", cuentaLol.Nombre);
        parametros.Add("@Nivel", cuentaLol.Nivel);
        parametros.Add("@EsenciaAzul", cuentaLol.EsenciaAzul);
        parametros.Add("@PuntosRiot", cuentaLol.PuntosRiot);
        parametros.Add("@PuntosLiga", cuentaLol.PuntosLiga);

        _conexion.Execute("InsertCuentaLol", parametros, commandType: CommandType.StoredProcedure);
    }

    public void AltaCuentaValorant(CuentaValorant cuentaValorant)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@idCuenta", cuentaValorant.idCuenta);
        parametros.Add("@Nombre", cuentaValorant.Nombre);
        parametros.Add("@Nivel", cuentaValorant.Nivel);
        parametros.Add("@Experiencia", cuentaValorant.Experiencia);
        parametros.Add("@PuntosCompetitivo", cuentaValorant.PuntosCompetitivo);
        parametros.Add("@idRango", cuentaValorant.idRango);

        _conexion.Execute("InsertCuentaValorant", parametros, commandType: CommandType.StoredProcedure);
    }

    public void AltaTipoObjeto(TipoObjeto tipoObjeto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@idTipoObjeto", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", tipoObjeto.Nombre);

        _conexion.Execute("InsertTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        tipoObjeto.idTipoObjeto = parametros.Get<byte>("@idTipoObjeto");
    }

    public void AltaObjeto(Objeto objeto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdObjeto", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", objeto.Nombre);
        parametros.Add("@PrecioEA", objeto.PrecioEA);
        parametros.Add("@PrecioRP", objeto.PrecioRP);
        parametros.Add("@Venta", objeto.Venta);
        parametros.Add("@IdTipoObjeto", objeto.idTipoObjeto); // Asegúrate de que este ID provenga de TipoObjeto

        _conexion.Execute("InsertObjeto", parametros, commandType: CommandType.StoredProcedure);
        objeto.idObjeto = parametros.Get<byte>("@idObjeto");
    }

    public void AltaInventario(Inventario inventario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdInventario", direction: ParameterDirection.Output);
        parametros.Add("@IdCuenta", inventario.IdCuenta); // Usa la propiedad IdCuenta

        _conexion.Execute("InsertInventario", parametros, commandType: CommandType.StoredProcedure);
        inventario.idInventario = parametros.Get<ushort>("@idInventario");
    }

    public IEnumerable<Servidor> ObtenerServidores()
    {
        var query = "SELECT * FROM Servidor";
        return _conexion.Query<Servidor>(query);
    }
        public Servidor? ObtenerServidor(byte idServidor)
    {
        var query = @"SELECT * FROM Servidor
                    where idServidor = @idServidor";
        
        return _conexion.QuerySingleOrDefault<Servidor>(query , new {idServidor});
    }

    public IEnumerable<RangoLol> ObtenerRangosLol()
    {
        var query = "SELECT * FROM RangoLol"; // Suponiendo que tienes una tabla RangoLol
        return _conexion.Query<RangoLol>(query);
    }
    public RangoLol? ObtenerRangoLol(byte idRango)
    {
        var query = @"SELECT * FROM RangoLol
                    where idRango = @idRango";
        
        return _conexion.QuerySingleOrDefault<RangoLol>(query , new {idRango});
    }

    public IEnumerable<RangoValorant> ObtenerRangosValorant()
    {
        var query = "SELECT * FROM RangoValorant"; // Suponiendo que tienes una tabla RangoValorant
        return _conexion.Query<RangoValorant>(query);
    }

    public IEnumerable<(int IdCuenta, int NivelLol)> ObtenerNivelesLol()
    {
        var query = "SELECT IdCuenta, Nivel FROM CuentaLol"; // Suponiendo que tienes una tabla CuentaLol
        return _conexion.Query<(int IdCuenta, int NivelLol)>(query);
    }

    public IEnumerable<(int IdCuenta, int NivelValorant)> ObtenerNivelesValorant()
    {
        var query = "SELECT IdCuenta, Nivel FROM CuentaValorant"; // Suponiendo que tienes una tabla CuentaValorant
        return _conexion.Query<(int IdCuenta, int NivelValorant)>(query);
    }

    public void BajaCuentaLol(uint idCuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdCuenta", idCuenta);
        _conexion.Execute("DeleteCuentaLol", parametros, commandType: CommandType.StoredProcedure);
    }

    public void BajaCuentaValorant(int idCuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdCuenta", idCuenta);
        _conexion.Execute("DeleteCuentaValorant", parametros, commandType: CommandType.StoredProcedure);
    }
    public void BajaCuenta(uint idCuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdCuenta", idCuenta);
        _conexion.Execute("DeleteCuenta", parametros, commandType: CommandType.StoredProcedure);
    }



    public IEnumerable<CuentaLol> ObtenerCuentasLol()
    {
        var query = "SELECT * FROM CuentaLol"; 
        return _conexion.Query<CuentaLol>(query);
    }

    public IEnumerable<CuentaValorant> ObtenerCuentasValorant()
    {
        var query = "SELECT * FROM CuentaValorant"; 
        return _conexion.Query<CuentaValorant>(query);
    }

    public IEnumerable<Cuenta> ObtenerCuenta()
    {
        var query = "SELECT * FROM Cuenta";
        return _conexion.Query<Cuenta>(query);
    }
    public void BajaServidor(byte idServidor)
    {
        var idServidorParametro = idServidor;
        _conexion.Execute("BajaServidor", new { p_unidServidor = idServidorParametro } , commandType: CommandType.StoredProcedure);
    }
    
}
