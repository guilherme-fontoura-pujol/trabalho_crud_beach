using BeachAula4.Data;
using BeachAula4.Entidades;
using BeachAula4.Interfaces;
using BeachAula4.Repositorios;
using BeachAula4.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IQuadraRepository, QuadraRepository>();
builder.Services.AddSingleton<QuadraService>();

// Adicione estes registros no Program.cs
builder.Services.AddScoped<IQuadraRepository, QuadraRepository>();
builder.Services.AddScoped<IQuadraService, QuadraService>();

// Add services to the container.
var app = builder.Build();
app.UseHttpsRedirection();

//Listar todas as quadras -----------------------------------------------------------------------
app.MapGet("/api/quadras", async (IQuadraRepository repository) =>
{
    var quadras = await repository.ListarAsync();

    return Results.Ok(quadras);
});

//Buscar uma quadra pelo ID ---------------------------------------------------------------------
app.MapGet("/api/quadras/{id:int}", async (int id, IQuadraRepository repository) =>
{
    var quadra = await repository.BuscarPorIdAsync(id);

    return quadra == null ? Results.NotFound() : Results.Ok(quadra);
});
//Buscar quadras ativas
app.MapGet("/api/quadras/ativas", async (IQuadraRepository repository) =>
{
    var quadras = await repository.ListarAtivasAsync();

    return Results.Ok(quadras);
});
//Cadastrar uma nova quadra ---------------------------------------------------------------------
app.MapPost("/api/quadras", async (Quadra quadra, IQuadraRepository repository) =>
{
    await repository.AdicionarAsync(quadra);
    return Results.Created($"/api/quadras/{quadra.Id}", quadra);
});

//Atualizar uma quadra --------------------------------------------------------------------------
app.MapPut("/api/quadras/{id:int}", async (int id, Quadra quadra, IQuadraRepository repository) =>
{
    quadra.Id = id;
    var atualizado = await repository.AtualizarAsync(quadra);

    return atualizado ? Results.NoContent() : Results.NotFound();
});

//Excluir uma quadra ----------------------------------------------------------------------------
app.MapDelete("/api/quadras/{id:int}", async (int id, IQuadraRepository repository) =>
{
    var excluido = await repository.ExcluirAsync(id);

    return excluido ? Results.NoContent() : Results.NotFound();
});

// Configure the HTTP request pipeline.

app.Run();

