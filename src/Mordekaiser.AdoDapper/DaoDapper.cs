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
        parametros.Add("@IdServidor", servidor.IdServidor);
        parametros.Add("@Nombre", servidor.Nombre);
        parametros.Add("@Abreviado", servidor.Abreviado);

        _conexion.Execute("insertServidor", parametros, commandType: CommandType.StoredProcedure);
    }
        public void AltaCuenta(Cuenta cuenta)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdCuenta", cuenta.IdCuenta);
        parametros.Add("@Nombre", cuenta.Nombre);
        parametros.Add("@Contrasena", cuenta.Contrasena);
        parametros.Add("@Email", cuenta.Email);

        _conexion.Execute("insertCuenta", parametros, commandType: CommandType.StoredProcedure);
    }
    //seguir con los insert de dapper


    public IEnumerable<Servidor> ObtenerServidores()
    {
        var query = "SELECT * FROM Servidor";
        var servidores = _conexion.Query<Servidor>(query);
        return servidores;
    }
    
}