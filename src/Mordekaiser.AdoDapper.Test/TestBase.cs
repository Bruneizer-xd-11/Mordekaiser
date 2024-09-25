namespace Mordekaiser.AdoDapper.Test
{
    public abstract class TestBase
    {
        protected readonly IDbConnection Conexion;
        protected readonly IDao dao;

        public TestBase()
        {
            // Cargar configuración desde el archivo appsettings.json
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Obtener la cadena de conexión desde el archivo de configuración
            string cadena = config.GetConnectionString("MySQL")!;
            Conexion = new MySqlConnection(cadena);
            dao = new DaoDapper(Conexion);
        }

        // Método para limpiar o preparar la base de datos antes de cada prueba
    }
}
