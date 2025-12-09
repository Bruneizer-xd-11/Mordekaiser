using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;
using MySqlConnector;
using Microsoft.AspNetCore.Authentication;

namespace MVC.Controllers
{
    public class CuentaLolController : Controller
    {
        private readonly IDao _dao;
        public CuentaLolController(IDao dao) => _dao = dao;

        public async Task<IActionResult> Listado()
        {
            var cuentasLol = await _dao.ObtenerCuentasLolAsync();
            return View(cuentasLol);
        }

        public async Task<IActionResult> Crear()
        {
            ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
            ViewBag.Rangos = await _dao.ObtenerRangosLolAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CuentaLol model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
                ViewBag.Rangos = await _dao.ObtenerRangosLolAsync();
                return View(model);
            }

            try
            {
                await _dao.AltaCuentaLolAsync(model);
            }
            catch (MySqlException ex)
            {
                if (ex.Message.Contains("Duplicate"))
                {
                    ViewBag.Error = "No se puede crear 2 cuentas LoL en la misma cuenta.";
                    ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
                    ViewBag.Rangos = await _dao.ObtenerRangosLolAsync();
                    return View(model);
                }
                throw;
            }

            return RedirectToAction(nameof(Listado));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarPorId(uint id)
        {
            var cuenta = await _dao.ObtenerCuentaLolPorIdAsync(id);

            if (cuenta == null)
            {
                TempData["Error"] = "La cuenta LoL no existe.";
                return RedirectToAction("Listado");
            }

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuarioRol = HttpContext.Session.GetString("UsuarioRol");

            if (usuarioId == null)
                return Forbid();

            bool esAdmin = usuarioRol == "1";
            bool esPropietario = cuenta.IdCuenta == usuarioId; // <-- ahora está bien

            if (!esAdmin && !esPropietario)
                return Forbid();

            await _dao.BajaCuentaLolAsync(id);

            TempData["Success"] = "Cuenta LoL eliminada correctamente.";
            return RedirectToAction("Listado");
        }
    }
}
