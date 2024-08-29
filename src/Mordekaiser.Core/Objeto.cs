namespace Mordekaiser.Core
{
    public class Objeto
    {
        public required TipoObjeto TipoObjeto { get; set; }
        public short idObjeto { get; set; }
        public required string Nombre { get; set; }
        public int PrecioEA { get; set; }
        public int PrecioRP { get; set; }
        public int Venta { get; set; }

    
    }

}
