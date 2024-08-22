using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mordekaiser.Core
{
    public class RangoLol
    {
        public sbyte IdRango { get; set; }
        public required string Nombre { get; set; }
        public sbyte Numero { get; set; }
        public int PuntosLigaNecesarios { get; set; } 
    }
}