namespace Mordekaiser.Core;
using System.ComponentModel.DataAnnotations;
public class CuentaValorant
{

    public  Cuenta? Cuenta   { get; set; }
    public  RangoValorant? RangoValorant { get; set; }


    //composiciones

[Required(ErrorMessage = "Debe elegir una cuenta base.")]
    public required uint idCuenta { get; set; }
        [Required(ErrorMessage = "Debe poner un nombre.")]
    public string? Nombre { get; set; }
        [Required(ErrorMessage = "Debe poner un valor.")]
 [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public required uint Nivel { get; set; }
    [Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public required uint Experiencia { get; set; }
       [Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public required int PuntosCompetitivo { get; set; }
    public ushort? idRango { get; set; }
}