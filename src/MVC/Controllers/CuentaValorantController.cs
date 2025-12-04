using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;
using MySqlConnector;  // <--- correcto para MySqlConnector

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
        Console.WriteLine(model.idCuenta);
        try{
            await _dao.AltaCuentaValorantAsync(model);

        }
       catch (MySqlException ex)
        {
            if (ex.Message.Contains("Duplicate"))
            {
                ViewBag.Error = "No se puede crear 2 cuentas Valorant en la misma cuenta.";
                ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
                ViewBag.Rangos = await _dao.ObtenerRangosValorantAsync();
                return View(model);
            }
            throw; // re-lanzar si es otra excepción
        }

        return RedirectToAction(nameof(Listado));
    }
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> BorrarTodasLasCuentasValorant()
{
    var cuentasVal = await _dao.ObtenerCuentasValorantAsync();
    foreach (var c in cuentasVal)
    {
        await _dao.BajaCuentaValorantAsync((int)c.idCuenta);
    }
    return RedirectToAction(nameof(Listado));
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> BorrarPorId(uint id)
{
    await _dao.BajaCuentaValorantAsync((int)id);
    return RedirectToAction(nameof(Listado));
}

}
