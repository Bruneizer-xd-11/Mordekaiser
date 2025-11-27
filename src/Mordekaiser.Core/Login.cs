namespace Mordekaiser.Core;

using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "Ingrese su usuario o email.")]
    public string UserOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese su contraseña.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}