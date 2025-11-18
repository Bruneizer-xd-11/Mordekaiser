using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

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
            ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();            // cuentas base
            ViewBag.Rangos = await _dao.ObtenerRangosLolAsync();         // rangos LoL
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

            await _dao.AltaCuentaLolAsync(model);
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
