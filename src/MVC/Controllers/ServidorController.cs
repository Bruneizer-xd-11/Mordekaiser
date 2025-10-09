using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

public class ServidorController : Controller
{
    private readonly IDao _idao;
    public ServidorController(IDao dao) => _idao = dao;
    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Servidor servidor)
    {
        if (!ModelState.IsValid) return View(servidor);

        await _idao.AltaServidorAsync(servidor);
        return RedirectToAction(nameof(Listado));
    }

    public async Task<IActionResult> Listado()
    {
        var servidores = await _idao.ObtenerServidoresAsync();
        return View(servidores);
    }
    public async Task<IActionResult> Borrar(byte id)
    {
        await _idao.DeleteServidorAsync(id);
        return RedirectToAction(nameof(Listado));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarTodos()
    {
        await _idao.BorrarTodosServidoresAsync();
        return RedirectToAction(nameof(Listado));
    }

}
