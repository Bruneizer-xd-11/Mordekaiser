// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;
// using Mordekaiser.Core;

// public class LoginController : Controller
// {
//     private readonly IDao _dao;

//     public LoginController(IDao dao) => _dao = dao;
//     [HttpGet]
//     public IActionResult Login() => View();

//     [HttpPost]
//     public async Task<IActionResult> Login(LoginViewModel model)
//     {
//         if (!ModelState.IsValid)
//             return View(model);

//         var cuenta = await _dao.LoginAsync(model.UserOrEmail, model.Password);
    
//         if (cuenta == null)
//         {
//             ModelState.AddModelError("", "Usuario/Email o contraseña incorrectos.");
//             return View(model);
//         }
//         HttpContext.Session.SetString("UsuarioRol", cuenta.Rol.ToString());
//         HttpContext.Session.SetString("UsuarioNombre", cuenta.Nombre);
//         HttpContext.Session.SetInt32("UsuarioId", (int)cuenta.IdCuenta);

//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.Name, cuenta.Nombre),
//             new Claim("IdCuenta", cuenta.IdCuenta.ToString())
//         };

//         var identity = new ClaimsIdentity(claims,
//             CookieAuthenticationDefaults.AuthenticationScheme);

//         await HttpContext.SignInAsync(
//             CookieAuthenticationDefaults.AuthenticationScheme,
//             new ClaimsPrincipal(identity));

//         return RedirectToAction("Index", "Home");
//     }

//     [HttpPost]
//       public async Task<IActionResult> Logout()
//     {
//         await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//         return RedirectToAction("Login");
//     }
    
// }
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Mordekaiser.Core;

public class LoginController : Controller
{
    private readonly IDao _dao;

    public LoginController(IDao dao) => _dao = dao;
    [HttpGet]
    public IActionResult Login() {
       
    if(User.Identity.IsAuthenticated)
    {
        return RedirectToAction("Index", "Home");{
           
            
        }}
    return View();
    }
        

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

        HttpContext.Session.SetString("UsuarioRol", cuenta.Rol.ToString());
        HttpContext.Session.SetString("UsuarioNombre", cuenta.Nombre);
        HttpContext.Session.SetInt32("UsuarioId", cuenta.IdCuenta);
        Console.WriteLine(cuenta.IdCuenta);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, cuenta.Nombre),
            new Claim("IdCuenta", cuenta.IdCuenta.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public async Task<IActionResult> Registro()
    {
        ViewBag.Servidores = await _dao.ObtenerServidoresAsync();
        return View(); // --> Buscará la vista en Views/Login/Registro.cshtml
    }

    [HttpPost]
    public async Task<IActionResult> Registro(Cuenta model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Servidores = await _dao.ObtenerServidoresAsync();
            return View(model);
        }

        var existenteEmail = await _dao.BuscarCuentaPorEmailAsync(model.Email);
        if (existenteEmail != null)
        {
            ModelState.AddModelError("", "El correo ya está registrado.");
            ViewBag.Servidores = await _dao.ObtenerServidoresAsync();
            return View(model);
        }

        var existenteNombre = await _dao.BuscarCuentaPorNombreAsync(model.Nombre);
        if (existenteNombre != null)
        {
            ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
            ViewBag.Servidores = await _dao.ObtenerServidoresAsync();
            return View(model);
        }

        await _dao.AltaCuentaAsync(model);

        return RedirectToAction("Login");
    }
}

