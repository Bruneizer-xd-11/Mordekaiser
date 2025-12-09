using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Mordekaiser.Core;

namespace MVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IDao _dao;
        public CuentaController(IDao dao) => _dao = dao;

        public async Task<IActionResult> Listado()
        {
            var usuarioRol = HttpContext.Session.GetInt32("UsuarioRol") ?? 0;
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            List<Cuenta> cuentas;
            if (usuarioRol == (int)Rol.Admin) 
            {
                cuentas = (await _dao.ObtenerCuentaAsync()).ToList();
            }
            else
            {
                cuentas = (await _dao.ObtenerCuentaAsync())
                            .Where(c => c.IdCuenta == usuarioId)
                            .ToList();
            }

            return View(cuentas);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var cuenta = await _dao.ObtenerCuentaPorIdAsync(id);
            if (cuenta == null)
                return NotFound();

            var usuarioRol = HttpContext.Session.GetInt32("UsuarioRol") ?? 0;
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioRol != (int)Rol.Admin && usuarioId != cuenta.IdCuenta)
                return View("~/Views/Home/SinPermiso.cshtml");

            return View(cuenta);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var rol = HttpContext.Session.GetInt32("UsuarioRol") ?? 0;
            if (rol != (int)Rol.Admin)
                return View("~/Views/Home/SinPermiso.cshtml");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Cuenta model)
        {
            var rol = HttpContext.Session.GetInt32("UsuarioRol") ?? 0;
            if (rol != (int)Rol.Admin)
                return View("~/Views/Home/SinPermiso.cshtml");

            if (!ModelState.IsValid)
                return View(model);

            var existenteEmail = await _dao.BuscarCuentaPorEmailAsync(model.Email);
            if (existenteEmail != null)
            {
                ModelState.AddModelError("", "El correo ya está registrado.");
                return View(model);
            }

            var existenteNombre = await _dao.BuscarCuentaPorNombreAsync(model.Nombre);
            if (existenteNombre != null)
            {
                ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                return View(model);
            }

            await _dao.AltaCuentaAsync(model);
            TempData["Success"] = "Cuenta creada correctamente.";
            return RedirectToAction("Listado");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarPorId(int id)
        {
            var cuenta = await _dao.ObtenerCuentaPorIdAsync(id);
            if (cuenta == null)
            {
                TempData["Error"] = "La cuenta no existe.";
                return RedirectToAction("Listado");
            }

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioRol = HttpContext.Session.GetInt32("UsuarioRol") ?? 0;

            bool esAdmin = usuarioRol == (int)Rol.Admin;
            bool esPropietario = usuarioId == cuenta.IdCuenta;

            if (!esAdmin && !esPropietario)
                return View("~/Views/Home/SinPermiso.cshtml");

            await _dao.BajaCuentaAsync(id);

            if (esPropietario)
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Login");
            }

            TempData["Success"] = "Cuenta eliminada correctamente.";
            return RedirectToAction("Listado");
        }
    }
}
