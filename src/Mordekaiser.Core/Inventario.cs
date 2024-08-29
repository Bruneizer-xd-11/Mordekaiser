namespace Mordekaiser.Core;

public class Inventario
{
    // Constructor
    public Inventario(CuentaLol cuentaLol)
    {
        CuentaLol = cuentaLol;
    }

    // Properties
    public CuentaLol CuentaLol { get; private set; }
    public List<Objeto> Objetos { get; set; } = new List<Objeto>();
    public int IdCuenta { get; set; }
    public short IdObjeto { get; set; }
    public int Cantidad { get; set; }
}

// Assuming these are placeholders for the actual classes