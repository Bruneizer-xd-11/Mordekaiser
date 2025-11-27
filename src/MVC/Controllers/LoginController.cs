using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Mordekaiser.Core;

public class AuthController : Controller
{
    private readonly IDao _dao;

    public AuthController(IDao dao) => _dao = dao;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var cuenta = await _dao.LoginAsync(model.UserOrEmail, model.Password);

        if (cuenta == null)
        {
            ModelState.AddModelError("", "Usuario/Email o contraseña incorrectos.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, cuenta.Nombre),
            new Claim("IdCuenta", cuenta.IdCuenta.ToString())
        };

        var identity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
}
