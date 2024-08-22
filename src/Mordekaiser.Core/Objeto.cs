using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mordekaiser.Core
{
    public class Objeto
    {
        public short idObjeto { get; set; }
        public required string Nombre { get; set; }
        public int PrecioEA { get; set; }
        public int PrecioRP { get; set; }
        public int Venta { get; set; }
        public sbyte idTipoObjeto { get; set; }

        
    }
    
}
