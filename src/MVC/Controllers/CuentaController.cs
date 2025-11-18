using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

namespace MVC.Controllers;

public class CuentaController : Controller
{
    private readonly IDao _idao;

    public CuentaController(IDao dao) => _idao = dao;

    public async Task<IActionResult> Listado()
    {
        var cuentas = await _idao.ObtenerCuentaAsync();
        return View(cuentas);
    }
    public async Task<IActionResult> Crear()
    {
        ViewBag.Servidores = await _idao.ObtenerServidoresAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Cuenta cuenta)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Servidores = await _idao.ObtenerServidoresAsync();
            return View(cuenta);
        }
        await _idao.AltaCuentaAsync(cuenta);

        return RedirectToAction(nameof(Listado));
    }

    public async Task<IActionResult> Borrar(uint id)
    {
        await _idao.DeleteCuentaAsync(id);
        return RedirectToAction(nameof(Listado));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarTodos()
    {
        var cuentas = await _idao.ObtenerCuentaAsync();

        foreach (var cuenta in cuentas)
        {
            await _idao.DeleteCuentaAsync((uint)cuenta.IdCuenta);
        }

        return RedirectToAction(nameof(Listado));
    }
}
