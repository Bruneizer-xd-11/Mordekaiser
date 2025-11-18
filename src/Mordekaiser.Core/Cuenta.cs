namespace Mordekaiser.Core;
using System.ComponentModel.DataAnnotations;

public class Cuenta
{
    public Cuenta() { }

    public uint IdCuenta { get; set; }

    [Required(ErrorMessage = "Debe poner un nombre.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe poner una contraseña.")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe poner un email.")]
    [EmailAddress(ErrorMessage = "Debe ingresar un email válido.")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "Debe elegir un servidor.")]
    public uint IdServidor { get; set; }

    public Servidor? Servidor { get; set; }

    public CuentaLol? CuentaLol { get; set; }
    public CuentaValorant? CuentaValorant { get; set; }
}
