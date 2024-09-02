namespace Mordekaiser.Core;

public class Cuenta
{
    public Cuenta()
    {
    }

    public required Servidor Servidor { get; set; }
    
    //composiciones

    public required int IdCuenta { get; set; }
    public required string Nombre { get; set; }
    public required string Contrasena { get; set; }
    public required string Email { get; set; }
}