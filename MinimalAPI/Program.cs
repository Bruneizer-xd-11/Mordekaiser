using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construcción de la app
var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

// Endpoint de prueba
app.MapGet("/hola", () => "Hola, Scalar!");

// Ejecutar
app.Run();
