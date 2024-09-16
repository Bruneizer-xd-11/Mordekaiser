namespace Mordekaiser.Core;

public class RangoLol
{
    public RangoLol()
    {
    }

    public required byte IdRango { get; set; }
    public required string Nombre { get; set; }
    public byte? Numero { get; set; }
    public required int PuntosLigaNecesarios { get; set; } 
}