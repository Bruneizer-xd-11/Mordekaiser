// using Microsoft.AspNetCore.Mvc;
// using Mordekaiser.Core;
// using MySqlConnector;

// namespace MVC.Controllers;

// public class CuentaValorantController : Controller
// {
//     private readonly IDao _dao;
//     public CuentaValorantController(IDao dao) => _dao = dao;

//     public async Task<IActionResult> Listado()
//     {
//         var cuentasVal = await _dao.ObtenerCuentasValorantAsync();
//         return View(cuentasVal);
//     }

//     public async Task<IActionResult> Crear()
//     {
//         ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
//         ViewBag.Rangos = await _dao.ObtenerRangosValorantAsync();
//         return View();
//     }

//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Crear(CuentaValorant model)
//     {
//         if (!ModelState.IsValid)
//         {
//             ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
//             ViewBag.Rangos = await _dao.ObtenerRangosValorantAsync();
//             return View(model);
//         }

//         try
//         {
//             await _dao.AltaCuentaValorantAsync(model);
//         }
//         catch (MySqlException ex)
//         {
//             if (ex.Message.Contains("Duplicate"))
//             {
//                 ViewBag.Error = "No se puede crear 2 cuentas Valorant en la misma cuenta.";
//                 ViewBag.Cuentas = await _dao.ObtenerCuentaAsync();
//                 ViewBag.Rangos = await _dao.ObtenerRangosValorantAsync();
//                 return View(model);
//             }
//             throw;
//         }

//         return RedirectToAction(nameof(Listado));
//     }

//     [HttpPost]
//     [ValidateAntiForgeryToken]
//    [HttpPost]
// public async Task<IActionResult> BorrarPorId(int id)
// {
//     var cuenta = await _dao.ObtenerCuentaValorantPorIdAsync(id);

//     if (cuenta == null)
//         return NotFound();

//     int userId = int.Parse(User.FindFirst("IdCuenta")!.Value);

//     if (cuenta.idCuenta != userId && !User.IsInRole("Cuenta"))
//         return Forbid();

//     await _dao.BajaCuentaValorantAsync(id);
//     return RedirectToAction("Listado");
// }
// }
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
public async Task<IActionResult> BorrarPorId(int id)
{
    var cuenta = await _dao.ObtenerCuentaValorantPorIdAsync(id);

    if (cuenta == null)
    {
        return NotFound();
    }
    if (User.FindFirst("IdCuenta") == null)
    {
        return Unauthorized();
    }

    int userId = int.Parse(User.FindFirst("IdCuenta")!.Value);
    if (cuenta.idCuenta != userId && !User.IsInRole("Admin"))
    {

        return Forbid();
    }

    try
    {
        await _dao.BajaCuentaValorantAsync(id);
        TempData["SuccessMessage"] = "Cuenta Valorant eliminada correctamente.";
    }
    catch (MySqlException ex)
    {

        if (ex.Message.Contains("FOREIGN KEY constraint failed") || ex.Number == 1451)
        {
             TempData["ErrorMessage"] = "No se puede eliminar la cuenta. Hay otros datos dependientes (como transacciones o referencias) que deben eliminarse primero.";
        }
        else
        {
             TempData["ErrorMessage"] = $"Error inesperado al eliminar la cuenta: {ex.Message}";
        }
    }
    catch (Exception)
    {
        TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar la cuenta.";
    }

    return RedirectToAction(nameof(Listado));
}
}
