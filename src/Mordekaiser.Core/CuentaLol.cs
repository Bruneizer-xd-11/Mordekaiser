namespace Mordekaiser.Core;
using System.ComponentModel.DataAnnotations;
public class CuentaLol
{
    public CuentaLol()
    {
    }

    public Cuenta? Cuenta { get; set; }
    public RangoLol? RangoLol { get; set; }
    public Inventario? Inventario { get; set; }
[Required(ErrorMessage = "Debe elegir una cuenta base.")]
    public required uint IdCuenta { get; set; }
    
    [Required(ErrorMessage = "Debe poner un nombre.")]
    public required string Nombre { get; set; }  = string.Empty;
    [Required(ErrorMessage = "Debe poner un valor.")]
 [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int Nivel { get; set; } = 0;
[Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int EsenciaAzul { get; set; } = 0;
[Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int PuntosRiot { get; set; } = 0;
    [Required(ErrorMessage = "Debe poner un valor.")]
    [Range(0, int.MaxValue, ErrorMessage = "No se permiten valores negativos.")]
    public int PuntosLiga { get; set; }=0;
    public byte IdRango { get; set; }
}