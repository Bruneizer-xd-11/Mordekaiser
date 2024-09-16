namespace Mordekaiser.Core;
public class CuentaLol
{
    public CuentaLol()
    {
    }

    
    public required Cuenta Cuenta { get; set; }
    public RangoLol? RangoLol { get; set; }
    public Inventario? Inventario { get; set; }
    public required uint IdCuenta { get; set; }
    public required string Nombre { get; set; }
    public uint Nivel { get; set; }=0;
    public uint EsenciaAzul { get; set; }=0;
    public uint PuntosRiot { get; set; }=0;
    public int PuntosLiga { get; set; }=0;
    public byte IdRango { get; set; }=0;
}