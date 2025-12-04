using Microsoft.AspNetCore.Http;
using Mordekaiser.Core;

public class UsuarioActualService
{
    private readonly IHttpContextAccessor _accessor;

    public UsuarioActualService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Cuenta ObtenerUsuario()
    {
        var http = _accessor.HttpContext;

        string rol = http.Session.GetString("UsuarioRol");
        string nombre = http.Session.GetString("UsuarioNombre");
        int? id = http.Session.GetInt32("UsuarioId");

        if (rol == null || id == null)
            return null;

        return new Cuenta
        {
            IdCuenta = id.Value,
            Nombre = nombre,
            Rol = Enum.Parse<Rol>(rol)
        };
    }
}
