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
    
        // Aquí llamamos al procedimiento almacenado 'insertServidor'
        _conexion.Execute("insertServidor", parametros, commandType: CommandType.StoredProcedure);
    }



    public IEnumerable<Servidor> ObtenerServidores()
    {
        var query = "SELECT * FROM Servidor";
        var servidores = _conexion.Query<Servidor>(query);
        return servidores;
    }
    
}