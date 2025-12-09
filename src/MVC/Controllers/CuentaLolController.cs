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
            var cuentaLol = await _dao.ObtenerCuentaLolPorIdAsync(id);

            if (cuentaLol == null)
            {
                return NotFound();
            }
            if (User.FindFirst("IdCuenta") == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(User.FindFirst("IdCuenta")!.Value);
            if (cuentaLol.IdCuenta != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }


            try
            {

                await _dao.BajaCuentaLolAsync(id);
                TempData["SuccessMessage"] = "Cuenta League of Legends eliminada correctamente.";
            }
            catch (MySqlException ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar la cuenta LoL. Razón: {ex.Message}";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar la cuenta LoL.";
            }

            return RedirectToAction("Listado"); 
        }
    }
}
