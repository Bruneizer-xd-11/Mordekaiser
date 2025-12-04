using System.Data;
using Mordekaiser.AdoDapper;
using Mordekaiser.Core;
using MySqlConnector;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Inyección para la conexión
builder.Services.AddScoped<IDbConnection>(_ =>
{
    var cs = builder.Configuration.GetConnectionString("MySQL");
    return new MySqlConnection(cs);
});
builder.Services.AddScoped<UsuarioActualService>();


// Inyección del Dao
builder.Services.AddScoped<IDao, DaoDapper>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Login/Logout";
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseSession();          
app.UseAuthentication();   
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
