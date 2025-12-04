namespace Mordekaiser.Core;
using Microsoft.AspNetCore.Mvc;


    public class AuthService
    {
        private readonly Cuenta _usuarioActual;

        public AuthService(Cuenta usuarioActual)
        {
            _usuarioActual = usuarioActual;
        }

        public IActionResult? RequiereRol(Rol rolRequerido)
{
    if (_usuarioActual.Rol != rolRequerido)
    {
        return new RedirectToActionResult("SinPermiso", "Home", null);
    }

    return null;
}
    }

