using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mordekaiser.Core
{
    public class Cuenta
    {
        public required Servidor servidor { get; set; }
        
        //composiciones

        public int idCuenta { get; set; }
        public sbyte idServidor { get; set; }
        public required string Nombre { get; set; }
        public required string Contrasena { get; set; }
        public required string eMail { get; set; }
    }
}