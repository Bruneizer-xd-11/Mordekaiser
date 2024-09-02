namespace Mordekaiser.Core;

public class Inventario
{
    // Constructor
    public Inventario(int idCuenta)
        {
            IdCuenta = idCuenta;
            Objetos = new List<Objeto>();
        }

        
        public int IdCuenta { get; set; } // Identificador de la cuenta
        public List<Objeto> Objetos { get; set; } 
        public int Cantidad { get; set; } 
    }
// corregir idCuenta a int