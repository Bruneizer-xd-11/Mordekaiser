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
        throw new NotImplementedException();
    }

    public IEnumerable<Servidor> ObtenerServidores()
    {
        var query = "SELECT * FROM Servidor";
        var servidores = _conexion.Query<Servidor>(query);
        return servidores;
    }
}
