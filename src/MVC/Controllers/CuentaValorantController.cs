using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

namespace MVC.Controllers;

public class CuentaValorantController : Controller
{
    private readonly IDao _dao;
    public CuentaValorantController(IDao dao) => _dao = dao;
    public async Task<IActionResult> Listado()
    {
        var cuentasVal = await _dao.ObtenerCuentasValorantAsync();
        return View(cuentasVal);
    }

    public async Task<IActionResult> Crear()
    {
        ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
        ViewBag.Rangos = await _dao.ObtenerRangosValorantAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(CuentaValorant model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
            ViewBag.Rangos = await _dao.ObtenerRangosValorantAsync();
            return View(model);
        }

        await _dao.AltaCuentaValorantAsync(model);
        return RedirectToAction(nameof(Listado));
    }
}
