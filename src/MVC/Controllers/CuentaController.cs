using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;
namespace MVC.Controllers;

public class CuentaController : Controller
{
    private readonly IDao _idao;
    public CuentaController(IDao dao)
    {
        _idao = dao;
    }
    public async Task<IActionResult> Index()
    {
        var cuentas =  await _idao.ObtenerCuentaAsync();
        return View(cuentas);
    }

}
