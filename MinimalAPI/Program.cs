using System.Data;
using MySqlConnector;
using Scalar.AspNetCore;
using Mordekaiser.Core;
using Mordekaiser.AdoDapper;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));
builder.Services.AddScoped<IDao, DaoDapper>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mordekaiser API V1");
    });
    app.MapScalarApiReference();
}

// Endpoints Minimal API

// endpoint para servidores
app.MapGet("/servidores", async (IDao dao) =>
{
    var servidores = await dao.ObtenerServidoresAsync();
    return Results.Ok(servidores);
}).WithTags("Servidor");

app.MapGet("/servidores/{id}", async (byte id, IDao dao) =>
{
    var servidor = await dao.ObtenerServidorAsync(id);
    // operacion ternaria--------- condición ? devuelve_valor_si_verdadero : devuelve_valor_si_falso;
    return servidor is null ? Results.NotFound("Servidor no encontrado") :Results.Ok(servidor) ;
}).WithTags("Servidor");

app.MapPost("/servidores", async (Servidor nuevoServidor, IDao dao) =>
{
    await dao.AltaServidorAsync(nuevoServidor);
    return Results.Created($"/servidores/{nuevoServidor.idServidor}", nuevoServidor);
}).WithTags("Servidor");

app.MapDelete("/servidores/{id}", async (byte id, IDao dao) =>
{
     var servidor = await dao.DeleteServidorAsync(id);
    return servidor > 0 ? Results.NoContent()  : Results.NotFound("Servidor no encontrado");
}).WithTags("Servidor");

// endpoints para cuentas uwuwuwuwuw
app.MapGet("/cuentas", async (IDao dao) =>
{
    var cuentas = await dao.ObtenerCuentaAsync();
    return Results.Ok(cuentas);
}).WithTags("Cuenta");

app.MapGet("/cuentas/{id}", async (uint id, IDao dao) =>
{
    var cuenta = (await dao.ObtenerCuentaAsync()).FirstOrDefault(c => c.IdCuenta == id);
    return cuenta is not null ? Results.Ok(cuenta) : Results.NotFound("Cuenta no encontrada");
}).WithTags("Cuenta");

app.MapPost("/cuentas", async (Cuenta nuevaCuenta, IDao dao) =>
{
    await dao.AltaCuentaAsync(nuevaCuenta);
    return Results.Created($"/cuentas/{nuevaCuenta.IdCuenta}", nuevaCuenta);
}).WithTags("Cuenta");

app.MapDelete("/cuentas/{id}", async (byte id, IDao dao) =>
{
    var cuentas = await dao.DeleteCuentaAsync(id);
    
    return cuentas > 0 ? Results.NoContent() : Results.NotFound("Cuenta no encontrada");

}).WithTags("Cuenta");

app.Run();
