using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mordekaiser.Core;
using System.Security.Cryptography;
using System.Text;
namespace Mordekaiser.AdoDapper;

public class DaoDapper : IDao
{
    private readonly IDbConnection _conexion;
    public DaoDapper(IDbConnection conexion) => _conexion = conexion;
public async Task<Cuenta?> LoginAsync(string userOrEmail, string password)
{
    using var sha = SHA256.Create();
    var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
    var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

    string query = @"
        SELECT * FROM Cuenta
        WHERE (Nombre = @user OR eMail = @user)
        AND Contrasena = @pass;
    ";

    return await _conexion.QueryFirstOrDefaultAsync<Cuenta>(query,
                new { user = userOrEmail, pass = hash });
}
public async Task<Cuenta?> BuscarCuentaPorNombreAsync(string nombre)
{
    string sql = "SELECT * FROM Cuenta WHERE Nombre = @Nombre";
    return await _conexion.QueryFirstOrDefaultAsync<Cuenta>(sql, new { Nombre = nombre });
}
public async Task<Cuenta?> BuscarCuentaPorEmailAsync(string email)
{
    string sql = "SELECT * FROM Cuenta WHERE Email = @Email";
    return await _conexion.QueryFirstOrDefaultAsync<Cuenta>(sql, new { Email = email });
}

    public async Task AltaServidorAsync(Servidor servidor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnidServidor", servidor.idServidor);
        parametros.Add("@UnNombre", servidor.Nombre);
        parametros.Add("@UnAbreviado", servidor.Abreviado);
        await _conexion.ExecuteAsync("InsertServidor", parametros, commandType: CommandType.StoredProcedure);
    }
public async Task AltaCuentaAsync(Cuenta cuenta)
{
    var parametros = new DynamicParameters();
    parametros.Add("@UnidCuenta", cuenta.IdCuenta);
    parametros.Add("@UnidServidor", cuenta.IdServidor); 
    parametros.Add("@UnNombre", cuenta.Nombre);
    parametros.Add("@Uncontrasena", cuenta.Contrasena);
    parametros.Add("@UneMail", cuenta.Email);
    parametros.Add("@UnRol", cuenta.Rol);

    await _conexion.ExecuteAsync("InsertCuenta", parametros, commandType: CommandType.StoredProcedure);
}

    public async Task AltaRangoLolAsync(RangoLol rangoLol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdRango", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", rangoLol.Nombre);
        parametros.Add("@Numero", rangoLol.Numero);
        parametros.Add("@PuntosLigaNecesarios", rangoLol.PuntosLigaNecesarios);
        await _conexion.ExecuteAsync("InsertRangoLol", parametros, commandType: CommandType.StoredProcedure);
        rangoLol.IdRango = parametros.Get<byte>("@IdRango");
    }

    public async Task AltaRangoValorantAsync(RangoValorant rangoValorant)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdRango", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", rangoValorant.Nombre);
        parametros.Add("@Numero", rangoValorant.Numero);
        parametros.Add("@PuntosCompetitivo", rangoValorant.PuntosCompetitivo);
        await _conexion.ExecuteAsync("InsertRangoValorant", parametros, commandType: CommandType.StoredProcedure);
        rangoValorant.idRango = parametros.Get<ushort>("@IdRango");
    }

public async Task AltaCuentaLolAsync(CuentaLol cuentaLol)
{
    var parametros = new DynamicParameters();
    parametros.Add("@UnidCuenta", cuentaLol.IdCuenta);
    parametros.Add("@UnNombre", cuentaLol.Nombre);
    parametros.Add("@UnNivel", cuentaLol.Nivel);
    parametros.Add("@UnEsenciaAzul", cuentaLol.EsenciaAzul);
    parametros.Add("@UnPuntosRiot", cuentaLol.PuntosRiot);
    parametros.Add("@UnPuntosLiga", cuentaLol.PuntosLiga);
    parametros.Add("@UnIdRango", cuentaLol.IdRango);  

    await _conexion.ExecuteAsync("InsertCuentaLol", parametros, commandType: CommandType.StoredProcedure);
}

    public async Task AltaCuentaValorantAsync(CuentaValorant cuentaValorant)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnidCuenta", cuentaValorant.idCuenta);
        parametros.Add("@UnNombre", cuentaValorant.Nombre);
        parametros.Add("@UnNivel", cuentaValorant.Nivel);
        parametros.Add("@UnExperiencia", cuentaValorant.Experiencia);
        parametros.Add("@UnPuntosCompetitivo", cuentaValorant.PuntosCompetitivo);
        parametros.Add("@UnidRango", cuentaValorant.idRango);

        await _conexion.ExecuteAsync("InsertCuentaValorant", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task AltaTipoObjetoAsync(TipoObjeto tipoObjeto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnidTipoObjeto", tipoObjeto.idTipoObjeto, DbType.Byte);
        parametros.Add("@UnNombre", tipoObjeto.Nombre, DbType.String);
        await _conexion.ExecuteAsync("InsertTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task AltaObjetoAsync(Objeto objeto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdObjeto", objeto.idObjeto, DbType.UInt16);
        parametros.Add("@Nombre", objeto.Nombre, DbType.String);
        parametros.Add("@PrecioEA", objeto.PrecioEA, DbType.UInt32);
        parametros.Add("@PrecioRP", objeto.PrecioRP, DbType.UInt32);
        parametros.Add("@Venta", objeto.Venta, DbType.UInt32);
        parametros.Add("@IdTipoObjeto", objeto.idTipoObjeto, DbType.Byte);
        await _conexion.ExecuteAsync("InsertObjeto", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task AltaInventarioAsync(Inventario inventario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdInventario", direction: ParameterDirection.Output);
        parametros.Add("@IdCuenta", inventario.IdCuenta);
        await _conexion.ExecuteAsync("InsertInventario", parametros, commandType: CommandType.StoredProcedure);
        inventario.idInventario = parametros.Get<ushort>("@idInventario");
    }
    public async Task<IEnumerable<Servidor>> ObtenerServidoresAsync()
    {
        var query = "SELECT * FROM Servidor";
        return await _conexion.QueryAsync<Servidor>(query);
    }
    public async Task<Servidor?> ObtenerServidorAsync(byte idServidor)
    {
        var query = @"SELECT * FROM Servidor WHERE idServidor = @idServidor";
        return await _conexion.QuerySingleOrDefaultAsync<Servidor>(query, new { idServidor });
    }
    public async Task<IEnumerable<RangoLol>> ObtenerRangosLolAsync()
    {
        var query = "SELECT * FROM RangoLol";
        return await _conexion.QueryAsync<RangoLol>(query);
    }

    public async Task<RangoLol?> ObtenerRangoLolAsync(byte idRango)
    {
        var query = @"SELECT * FROM RangoLol WHERE idRango = @idRango";
        return await _conexion.QuerySingleOrDefaultAsync<RangoLol>(query, new { idRango });
    }

    public async Task<IEnumerable<RangoValorant>> ObtenerRangosValorantAsync()
    {
        var query = "SELECT * FROM RangoValorant";
        return await _conexion.QueryAsync<RangoValorant>(query);
    }

    public async Task<IEnumerable<(int IdCuenta, int NivelLol)>> ObtenerNivelesLolAsync()
    {
        var query = "SELECT IdCuenta, Nivel AS NivelLol FROM CuentaLol";
        return await _conexion.QueryAsync<(int, int)>(query);
    }

    public async Task<IEnumerable<(int IdCuenta, int NivelValorant)>> ObtenerNivelesValorantAsync()
    {
        var query = "SELECT IdCuenta, Nivel AS NivelValorant FROM CuentaValorant";
        return await _conexion.QueryAsync<(int, int)>(query);
    }
    public async Task<IEnumerable<Cuenta>> ObtenerCuentaAsync()
    {
        var query = @"
        SELECT 
            c.IdCuenta, 
            c.Nombre, 
            c.Contrasena, 
            c.Email,
            c.idServidor,
            s.idServidor,
            s.Nombre,
            s.Abreviado
        FROM Cuenta c
        JOIN Servidor s ON c.idServidor = s.idServidor"; ;
        var cuentas = await _conexion.QueryAsync<Cuenta, Servidor, Cuenta>(
        query,
        (cuenta, servidor) =>
        {
            cuenta.Servidor = servidor;
            return cuenta;
        },
        splitOn: "IdServidor"
    );

        return cuentas;
    }    
public async Task<IEnumerable<CuentaLol>> ObtenerCuentasLolAsync()
{
    var sql = @"
        SELECT c.*, r.idRango, r.Nombre, r.PuntosLigaNecesario, r.Numero
        FROM CuentaLol c
        LEFT JOIN RangoLol r ON c.idRango = r.idRango";

    return await _conexion.QueryAsync<CuentaLol, RangoLol, CuentaLol>(
        sql,
        (cuenta, rango) =>
        {
            cuenta.RangoLol = rango;
            return cuenta;
        },
        splitOn: "idRango"
    );
}

public async Task<IEnumerable<CuentaValorant>> ObtenerCuentasValorantAsync()
{
    var sql = @"
        SELECT 
            cv.idCuenta,
            cv.Nombre,
            cv.Nivel,
            cv.Experiencia,
            cv.PuntosCompetitivo,
            cv.idRango,
            rv.idRango AS rv_idRango,   -- split marker para el segundo objeto
            rv.Nombre,                  -- mantener nombres que coincidan con la clase
            rv.Numero,
            rv.PuntosNecesarios
        FROM CuentaValorant cv
        LEFT JOIN RangoValorant rv ON cv.idRango = rv.idRango;";

    return await _conexion.QueryAsync<CuentaValorant, RangoValorant, CuentaValorant>(
        sql,
        (cv, rv) =>
        {
            cv.RangoValorant = rv; 
            return cv;
        },
        splitOn: "rv_idRango"
    );
}

    public async Task BajaCuentaLolAsync(uint idCuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdCuenta", idCuenta);
        await _conexion.ExecuteAsync("DeleteCuentaLol", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task<int> DeleteCuentaAsync(uint unidCuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unidCuenta", unidCuenta);
        return await _conexion.ExecuteAsync("DeleteCuenta", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task BajaCuentaValorantAsync(int idCuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdCuenta", idCuenta);
        await _conexion.ExecuteAsync("DeleteCuentaValorant", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task BorrarTodosServidoresAsync()
{
    const string sql = "DELETE FROM Servidor;";
    await _conexion.ExecuteAsync(sql);
}
    public async Task<int> DeleteServidorAsync(byte idServidor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("p_unidServidor", idServidor);
        return await _conexion.ExecuteAsync("DeleteServidor", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task BajaObjetoAsync(ushort idObjeto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@idObjeto", idObjeto, DbType.UInt16);
        await _conexion.ExecuteAsync("DeleteObjeto", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task<IEnumerable<Objeto>> ObtenerObjetosAsync()
    {
        var consulta = "SELECT * FROM Objeto";
        return await _conexion.QueryAsync<Objeto>(consulta);
    }

    public async Task<IEnumerable<TipoObjeto>> ObtenerTiposObjetosAsync()
    {
        var obtener = "SELECT * FROM TipoObjeto";
        return await _conexion.QueryAsync<TipoObjeto>(obtener);
    }
}
