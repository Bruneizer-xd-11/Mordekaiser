namespace MVC.Controllers;
public class ServidorController
{

public class ServidorController : Controller
{
private readonly Idao idaoo;
public ServidorController(Idao idao)
{
 idaoo = idao;
}
}
public async Task<IactionResult> Index()
{
    var servidores = await idaoo.ObtenerServidoresAsync();
    return View(servidores);
}
}


