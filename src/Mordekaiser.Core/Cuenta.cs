    namespace Mordekaiser.Core;

    public class Cuenta
    {

        public required Servidor Servidor { get; set; }
        //composiciones
        public uint IdCuenta { get; set; }
        public required string Nombre { get; set; }
        public required string Contrasena { get; set; }
        public required string? Email { get; set; }
    
    }