namespace Mordekaiser.Core;
using System.Linq;


    public class AuthService
    {
        private readonly Cuenta _usuarioActual;

        public AuthService(Cuenta usuarioActual)
        {
            _usuarioActual = usuarioActual;
        }

        public void RequiereRol(params Rol[] rolesPermitidos)
        {
            if (!rolesPermitidos.Contains(_usuarioActual.Rol))
                throw new UnauthorizedAccessException("No tienes permisos para realizar esta acción.");
        }
    }

