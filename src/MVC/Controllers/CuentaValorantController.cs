using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;
using MySqlConnector;

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

        try
        {
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
            throw;
        }

        return RedirectToAction(nameof(Listado));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
   [HttpPost]
public async Task<IActionResult> BorrarPorId(int id)
{
    var cuenta = await _dao.ObtenerCuentaValorantPorIdAsync(id);

    if (cuenta == null)
        return NotFound();

    int userId = int.Parse(User.FindFirst("IdCuenta")!.Value);

    if (cuenta.idCuenta != userId && !User.IsInRole("Admin"))
        return Forbid();

    await _dao.BajaCuentaValorantAsync(id);
    return RedirectToAction("Listado");
}
}
