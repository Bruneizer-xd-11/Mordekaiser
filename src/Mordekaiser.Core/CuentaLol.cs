namespace Mordekaiser.Core;
public class CuentaLol
{

    public required Cuenta Cuenta { get; set; }
    public RangoLol? RangoLol { get; set; }
    public Inventario? Inventario { get; set; }

    public int IdCuenta { get; set; }
    public required string Nombre { get; set; }
    public int Nivel { get; set; }
    public int EsenciaAzul { get; set; }
    public int PuntosRiot { get; set; }
    public int PuntosLiga { get; set; }
    public sbyte IdRango { get; set; }

}