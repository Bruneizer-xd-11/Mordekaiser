using System.Data;
using System.Runtime.CompilerServices;
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
        parametros.Add("@IdServidor", direction:ParameterDirection.Output);
        parametros.Add("@Nombre", servidor.Nombre);
        parametros.Add("@Abreviado", servidor.Abreviado);

        _conexion.Execute("insertServidor", parametros, commandType: CommandType.StoredProcedure);
        servidor.IdServidor = parametros.Get<byte>("@idServidor");
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
    
    public void AltaRangoLol(RangoLol rangoLol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdRango", direction: ParameterDirection.Output); 
        parametros.Add("@Nombre", rangoLol.Nombre);
        parametros.Add("@Numero", rangoLol.Numero);
        parametros.Add("@PuntosLigaNecesarios", rangoLol.PuntosLigaNecesarios);

        _conexion.Execute("insertRangoLol", parametros, commandType: CommandType.StoredProcedure);

        rangoLol.IdRango = parametros.Get<byte>("@IdRango");
    }
    public void AltaRangoValorant(RangoValorant rangoValorant)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdRango", direction: ParameterDirection.Output); 
        parametros.Add("@Nombre", rangoValorant.Nombre);
        parametros.Add("@Numero", rangoValorant.Numero);
        parametros.Add("@PuntosCompetitivo", rangoValorant.PuntosCompetitivo);

        _conexion.Execute("insertRangoValorant", parametros, commandType: CommandType.StoredProcedure);

        rangoValorant.idRango = parametros.Get<ushort>("@idRango");
    }

    //seguir con los insert de dapper


    public IEnumerable<Servidor> ObtenerServidores()
    {
        var query = "SELECT * FROM Servidor";
        var servidores = _conexion.Query<Servidor>(query);
        return servidores;
    }
    
}