using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mordekaiser.Core
{
    public class CuentaValorant
    {
        public required Cuenta cuenta { get; set; }

        //composiciones

        public int idCuenta { get; set; }
        public required string Nombre { get; set; }
        public int Nivel { get; set; }
        public int Experiencia { get; set; }
        public int PuntosCompetitivo { get; set; }
        public short idRango { get; set; }

    }
}