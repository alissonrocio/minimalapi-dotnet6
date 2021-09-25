using Microsoft.EntityFrameworkCore;
using web;
using web.database;
using web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MockDbContext>(options => options.UseInMemoryDatabase("MockDB"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Database.Preparacao(app);

// GET
app.MapGet("/api/fornecedores", async (HttpContext http, MockDbContext dbContext) =>
{
    var fornecedores = await dbContext.Fornecedores.ToListAsync();
    await http.Response.WriteAsJsonAsync(fornecedores);
});

// GET
app.MapGet("/api/fornecedores/{id}", async (HttpContext http, MockDbContext dbContext, int id) =>
{
    int number;
    bool success = int.TryParse(id.ToString(), out number);
    if (!success)
    {
        http.Response.StatusCode = 400;
        return;
    }

    var fornecedor = await dbContext.Fornecedores.FindAsync(int.Parse(id.ToString()));
    if (fornecedor == null)
    {
        http.Response.StatusCode = 404;
        return;
    }

    await http.Response.WriteAsJsonAsync(fornecedor);
});

// POST
app.MapPost("/api/fornecedor", async (HttpContext http, MockDbContext dbContext, Fornecedor fornecedor) =>
{
    dbContext.Fornecedores.Add(fornecedor);
    await dbContext.SaveChangesAsync();
    await http.Response.WriteAsJsonAsync(fornecedor);
});

// PUT
app.MapPut("/api/fornecedor/{id}", async (HttpContext http, MockDbContext dbContext, int id, Fornecedor fornecedor) =>
{
    var forn = await dbContext.Fornecedores.FirstOrDefaultAsync(a => a.Id == id);
    if (forn == null)
    {
        http.Response.StatusCode = 404;
        return;
    }

    forn.Nome = fornecedor.Nome;
    forn.Tipo = fornecedor.Tipo;
    forn.CpfCnpj = fornecedor.CpfCnpj;

    dbContext.Fornecedores.Update(forn);
    await dbContext.SaveChangesAsync();
});

// DELETE
app.MapDelete("/api/fornecedor/{id}", async (HttpContext http, MockDbContext dbContext, int id) =>
{
    var fornecedor = await dbContext.Fornecedores.FirstOrDefaultAsync(a => a.Id == id);
    if (fornecedor == null)
    {
        http.Response.StatusCode = 404;
        return;
    }

    dbContext.Fornecedores.Remove(fornecedor);
    await dbContext.SaveChangesAsync();
});

app.Run();