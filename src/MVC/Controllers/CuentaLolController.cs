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
                throw;
            }

            return RedirectToAction(nameof(Listado));
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> BorrarTodasLasCuentasLol()
        // {
        //     var cuentas = await _dao.ObtenerCuentasLolAsync();
        //     foreach (var c in cuentas)
        //     {
        //         await _dao.BajaCuentaLolAsync(c.IdCuenta);
        //     }
        //     return RedirectToAction(nameof(Listado));
        // }

        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> BorrarPorId(uint id)
{
    // Obtener la cuenta LoL
    var cuenta = await _dao.ObtenerCuentaLolPorIdAsync(id);

    if (cuenta == null)
    {
        TempData["Error"] = "La cuenta LoL no existe.";
        return RedirectToAction("Listado", "CuentaLol");
    }
    var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

    if (usuarioId == null)
    {
        return Unauthorized();
    }
    if (cuenta.IdCuenta != usuarioId)
    {
        TempData["Error"] = "No tienes permiso para borrar esta cuenta LoL.";
        return RedirectToAction("Listado", "CuentaLol");
    }

    await _dao.BajaCuentaLolAsync(id);

    return RedirectToAction("Listado", "CuentaLol");
}

    }
}
