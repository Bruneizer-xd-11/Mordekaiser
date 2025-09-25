using System.Data;
using Mordekaiser.AdoDapper;
using Mordekaiser.Core;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Inyección de conexión
builder.Services.AddScoped<IDbConnection>(_ =>
{
    var cs = builder.Configuration.GetConnectionString("MySQL");
    return new MySqlConnection(cs);
});

// Inyección del DAO
builder.Services.AddScoped<IDao, DaoDapper>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
