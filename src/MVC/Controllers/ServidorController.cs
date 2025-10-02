using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

public class ServidorController : Controller
{
    private readonly IDao _dao;
    public ServidorController(IDao dao) => _dao = dao;

    // GET: /Servidor/Crear
    public IActionResult Crear() => View();

    // POST: /Servidor/Crear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Servidor servidor)
    {
        if (!ModelState.IsValid) return View(servidor);

        await _dao.AltaServidorAsync(servidor);
        return RedirectToAction(nameof(Listado)); // <- volver a la lista
    }

    // GET: /Servidor/Listado
    public async Task<IActionResult> Listado()
    {
        var servidores = await _dao.ObtenerServidoresAsync();
        return View(servidores);
    }
}
