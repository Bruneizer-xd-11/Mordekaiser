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

// Inyección del Dao
builder.Services.AddScoped<IDao, DaoDapper>();

// 🔹 Autenticación con cookies (antes de Build)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// 🔹 Middlewares (después de Build)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
