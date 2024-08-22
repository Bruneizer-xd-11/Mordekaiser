namespace Mordekaiser.Core;
public class CuentaLol
{

    public Cuenta Cuenta { get; set; }
    public Inventario Inventario { get; set; }

    //        public CuentaLol(int id, string nombreUsuario)
    //{
      //  idCuenta = id;                // Assigning parameter id to property IdCuenta
        //Nombre = nombreUsuario;       // Assigning parameter nombreUsuario to property Nombre
    //    inventario = new Inventario(this); // Initializing Inventario with this instance
    //}
    
    public int IdCuenta { get; set; }
    public required string Nombre { get; set; }
    public int Nivel { get; set; }
    public int EsenciaAzul { get; set; }
    public int PuntosRiot { get; set; }
    public int PuntosLiga { get; set; }
    public sbyte IdRango { get; set; }

}