namespace Mordekaiser.Core;

public class CuentaValorant
{
    public CuentaValorant()
    {
    }

    public required Cuenta Cuenta { get; set; }
    public required RangoValorant RangoValorant { get; set; }

    //composiciones

    public required uint idCuenta { get; set; }
    public string? Nombre { get; set; }
    public required uint Nivel { get; set; }
    public required uint Experiencia { get; set; }
    public required int PuntosCompetitivo { get; set; }
    public ushort? idRango { get; set; }

}