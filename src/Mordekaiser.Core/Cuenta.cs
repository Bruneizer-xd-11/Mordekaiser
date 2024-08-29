namespace Mordekaiser.Core;

public class Cuenta
{
    public required Servidor Servidor { get; set; }
    
    //composiciones

    public int idCuenta { get; set; }
    public required string Nombre { get; set; }
    public required string Contrasena { get; set; }
    public required string eMail { get; set; }
}