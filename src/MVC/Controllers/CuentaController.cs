using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Mordekaiser.Core;
using MySqlConnector;
namespace MVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IDao _dao;
        public CuentaController(IDao dao) => _dao = dao;

        public async Task<IActionResult> Listado()
        {

            var cuentas = (await _dao.ObtenerCuentaAsync()).ToList();
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
            var cuentaBase = await _dao.ObtenerCuentaPorIdAsync(id);

            if (cuentaBase == null)
            {
                return NotFound();
            }
            if (User.FindFirst("IdCuenta") == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(User.FindFirst("IdCuenta")!.Value);
            if (cuentaBase.IdCuenta != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            if (cuentaBase.Rol.ToString() == "Admin" && cuentaBase.IdCuenta == userId)
            {
                TempData["ErrorMessage"] = "No puedes eliminar tu propia cuenta de Administrador.";
                return RedirectToAction("ListadoCuentas");
            }
            try
            {
                await _dao.BajaCuentaAsync(id);
                TempData["SuccessMessage"] = "Cuenta base eliminada correctamente.";

                if (cuentaBase.IdCuenta == userId)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (MySqlException ex)
            {
                TempData["ErrorMessage"] = "ERROR: La cuenta base no se puede eliminar. Debe borrar primero las cuentas LOL/Valorant que dependen de ella.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar la cuenta base.";
            }

            return RedirectToAction("Listado");
        }
    }
}