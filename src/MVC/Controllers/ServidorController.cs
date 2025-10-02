using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

public class ServidorController : Controller
{
    private readonly IDao _dao;
    public ServidorController(IDao dao) => _dao = dao;
    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Servidor servidor)
    {
        if (!ModelState.IsValid) return View(servidor);

        await _dao.AltaServidorAsync(servidor);
        return RedirectToAction(nameof(Listado));
    }

    public async Task<IActionResult> Listado()
    {
        var servidores = await _dao.ObtenerServidoresAsync();
        return View(servidores);
    }
}
