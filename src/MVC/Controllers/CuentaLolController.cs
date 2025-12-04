using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;
using MySqlConnector; // <--- importante, para MySqlException

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
                throw; // re-lanzar si es otro error
            }

            return RedirectToAction(nameof(Listado));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarTodasLasCuentasLol()
        {
            var cuentas = await _dao.ObtenerCuentasLolAsync();
            foreach (var c in cuentas)
            {
                await _dao.BajaCuentaLolAsync(c.IdCuenta);
            }
            return RedirectToAction(nameof(Listado));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarPorId(uint id)
        {
            await _dao.BajaCuentaLolAsync(id);
            return RedirectToAction(nameof(Listado));
        }
    }
}
