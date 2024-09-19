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
    public void AltaCuentaLol(CuentaLol cuentaLol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@idCuenta", cuentaLol.IdCuenta); // ID de la cuenta existente
        parametros.Add("@Nombre", cuentaLol.Nombre);
        parametros.Add("@Nivel", cuentaLol.Nivel);
        parametros.Add("@EsenciaAzul", cuentaLol.EsenciaAzul);
        parametros.Add("@PuntosRiot", cuentaLol.PuntosRiot);
        parametros.Add("@PuntosLiga", cuentaLol.PuntosLiga);

        // Ejecutamos el procedimiento almacenado para insertar la cuenta de LoL
        _conexion.Execute("InserCuentaLol", parametros, commandType: CommandType.StoredProcedure);
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


    

    //seguir con los insert de dapper


    public IEnumerable<Servidor> ObtenerServidores()
    {
        var query = "SELECT * FROM Servidor";
        var servidores = _conexion.Query<Servidor>(query);
        return servidores;
    }
    
}