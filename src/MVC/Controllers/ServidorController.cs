using Microsoft.AspNetCore.Mvc;
using Mordekaiser.Core;

public class ServidorController : Controller
{
    private readonly IDao _idao;
    private readonly AuthService _auth;

    public ServidorController(IDao dao, UsuarioActualService usuarioActualService)
    {
        _idao = dao;

        var usuarioActual = usuarioActualService.ObtenerUsuario();
        _auth = new AuthService(usuarioActual); // <<< LO QUE FALTABA
    }

    public IActionResult Crear()
    {
        var check = _auth.RequiereRol(Rol.Admin);
        if (check != null) return check;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Servidor servidor)
    {
        var check = _auth.RequiereRol(Rol.Admin);
        if (check != null) return check;

        if (!ModelState.IsValid)
            return View(servidor);

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
        var check = _auth.RequiereRol(Rol.Admin);
        if (check != null) return check;

        await _idao.DeleteServidorAsync(id);
        return RedirectToAction(nameof(Listado));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarTodos()
    {
        var check = _auth.RequiereRol(Rol.Admin);
        if (check != null) return check;

        await _idao.BorrarTodosServidoresAsync();
        return RedirectToAction(nameof(Listado));
    }

}
