namespace Mordekaiser.Core;


public class Objeto
{
    public Objeto(TipoObjeto tipoObjeto)
    {
        TipoObjeto = tipoObjeto;
    }

    public required TipoObjeto TipoObjeto { get; set; }
    public required ushort idObjeto { get; set; }
    public required string Nombre { get; set; }
    public uint? PrecioEA { get; set; }
    public uint? PrecioRP { get; set; }
    public uint? Venta { get; set; }
    public byte idTipoObjeto { get; set; } 
}