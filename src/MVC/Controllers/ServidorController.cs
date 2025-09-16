using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;
namespace MVC.Controllers;




public class ServidorController : Controller
{
    private readonly IDao _idao;
    public ServidorController(IDao idao)
    {
        _idao = idao;
    }
    public async Task<IActionResult> Index()
    {
        var servidores = await _idao.ObtenerServidoresAsync();
        return View(servidores);
    }
}


