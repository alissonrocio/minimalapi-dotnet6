using System.Text.Json;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MockDbContext>(options => options.UseInMemoryDatabase("MockDB"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Database.Preparacao(app);

// GET
app.MapGet("/api/fornecedores", async (HttpContext http ,MockDbContext dbContext) => 
{
    var fornecedores = await dbContext.Fornecedores.ToListAsync();
    await http.Response.WriteAsJsonAsync(fornecedores);
});

// GET
app.MapGet("/api/fornecedores/{id}", async (HttpContext http , MockDbContext dbContext , int id) => 
{
    int number;
    bool success = int.TryParse(id.ToString(), out number);
    if (!success)
    {
        http.Response.StatusCode = 400;
        return;
    }

    var fornecedor = await dbContext.Fornecedores.FindAsync(int.Parse(id.ToString()));
    if (fornecedor == null){
        http.Response.StatusCode = 404;
        return;
    }

    await http.Response.WriteAsJsonAsync(fornecedor);
});

// POST
app.MapPost("/api/fornecedor", async (HttpContext http , MockDbContext dbContext , Fornecedor fornecedor) => 
{
    dbContext.Fornecedores.Add(fornecedor);
    await dbContext.SaveChangesAsync();
    await http.Response.WriteAsJsonAsync(fornecedor);
});

// PUT
app.MapPut("/api/fornecedor/{id}", async (HttpContext http , MockDbContext dbContext , int id , Fornecedor fornecedor) => 
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
app.MapDelete("/api/fornecedor/{id}", async (HttpContext http ,MockDbContext dbContext , int id) => 
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

public class MockDbContext : DbContext
{
    public MockDbContext(DbContextOptions options) : base(options) {}

    protected MockDbContext() {}

    public DbSet<Fornecedor> Fornecedores { get; set; }
}

public class Fornecedor
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Tipo { get; set; }

    public string CpfCnpj { get; set; }
}

public static class Database
{
    public static void Preparacao(IApplicationBuilder app)
    {
        using(var service = app.ApplicationServices.CreateScope())
        {
            PreencheDados(service.ServiceProvider.GetService<MockDbContext>());
        }
    }

    private static void PreencheDados(MockDbContext context)
    {
        var json_fornecedores = File.ReadAllText(@"./database/fornecedores.json");
        var obj = JsonSerializer.Deserialize<IEnumerable<Fornecedor>>(json_fornecedores);

        foreach (var item in obj)
        {
            context.Fornecedores.Add(item);
        }
        
        context.SaveChanges();
    }

}
