using System.Data;
using Microsoft.Extensions.Configuration;
using Mordekaiser.Core;
using MySqlConnector;

namespace Mordekaiser.AdoDapper.Test;

public abstract class TestBase
{
    protected readonly IDbConnection Conexion;
    protected readonly IDao Dao;
    public TestBase()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .Build();
        string cadena = config.GetConnectionString("MySQL")!;
        
        Conexion = new MySqlConnection(cadena);

        Dao = new DaoDapper(Conexion);
    }
}

/*Qué wachin Luis. Cómo vas a dejar tu cuenta abierta*/