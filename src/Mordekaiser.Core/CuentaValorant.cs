namespace Mordekaiser.Core;
using System.ComponentModel.DataAnnotations;

public class CuentaValorant
{
    public CuentaValorant()
    {
    }

    public Cuenta? Cuenta { get; set; }
    public RangoValorant? RangoValorant { get; set; }

    [Required(ErrorMessage = "Debe elegir una cuenta base.")]
    public int idCuenta { get; set; }

    [Required(ErrorMessage = "Debe poner un nombre.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int Nivel { get; set; }

    [Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int Experiencia { get; set; }

    [Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int PuntosCompetitivo { get; set; }

    public ushort? idRango { get; set; }
}
