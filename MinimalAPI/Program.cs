
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/hola", () => "Hola, Scalar!");
app.MapGet("/Rango", () => "Hello World!");
app.MapGet("/cuentas", () => cuentas);


app.Run();
