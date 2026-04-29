using Despesas.Api.Application.Services;
using Despesas.Api.Endpoints;
using Despesas.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DespesasDbContext>(options =>
    options.UseInMemoryDatabase("DespesasDb"));

builder.Services.AddScoped<DespesaService>();

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Despesas API";
        options.Theme = ScalarTheme.BluePlanet;
    });
}

app.MapDespesasEndpoints();

app.Run();

public partial class Program { }
