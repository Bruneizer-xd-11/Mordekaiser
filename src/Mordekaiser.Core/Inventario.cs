namespace Mordekaiser.Core;

public class Inventario
{
    // Constructor
    public Inventario(uint idCuenta)
        {
            IdCuenta = idCuenta;
            Objetos = new List<Objeto>();
        }

        public required int idInventario { get; set; }
        public required uint IdCuenta { get; set; } // Identificador de la cuenta
        public required List<Objeto> Objetos { get; set; } 
        public int? Cantidad { get; set; }  
    }
// corregir idCuenta a int