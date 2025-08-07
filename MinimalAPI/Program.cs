using System.Data;
using MySqlConnector;
using Scalar.AspNetCore;
using Mordekaiser.Core;
using Mordekaiser.AdoDapper;
using Mordekaiser.AdoDapper.Test;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(_ => new MySqlConnection("server=localhost;database=5to_RiotGames;user=root;password=root;"));

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

// Servidores
app.MapGet("/servidores", async (IDao dao) =>
{
    var servidores = await dao.ObtenerServidoresAsync();
    return Results.Ok(servidores);
}).WithTags("Servidor");

app.MapGet("/servidores/{id}", async (byte id, IDao dao) =>
{
    var servidor = await dao.ObtenerServidorAsync(id);
    return servidor is not null ? Results.Ok(servidor) : Results.NotFound();
}).WithTags("Servidor");

app.MapPost("/servidores", async (Servidor nuevoServidor, IDao dao) =>
{
    await dao.AltaServidorAsync(nuevoServidor);
    return Results.Created($"/servidores/{nuevoServidor.idServidor}", nuevoServidor);
}).WithTags("Servidor");

// Cuentas
app.MapGet("/cuentas", async (IDao dao) =>
{
    var cuentas = await dao.ObtenerCuentaAsync();
    return Results.Ok(cuentas);
}).WithTags("Cuenta");

app.MapGet("/cuentas/{id}", async (uint id, IDao dao) =>
{
    var cuenta = (await dao.ObtenerCuentaAsync()).FirstOrDefault(c => c.IdCuenta == id);
    return cuenta is not null ? Results.Ok(cuenta) : Results.NotFound();
}).WithTags("Cuenta");

app.MapPost("/cuentas", async (Cuenta nuevaCuenta, IDao dao) =>
{
    await dao.AltaCuentaAsync(nuevaCuenta);
    return Results.Created($"/cuentas/{nuevaCuenta.IdCuenta}", nuevaCuenta);
}).WithTags("Cuenta");

app.Run();
