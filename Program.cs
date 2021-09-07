using System.Text.Json;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MockDbContext>(options => options.UseInMemoryDatabase("MockDB"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Database.Preparacao(app);

app.MapGet("/api/fornecedores", async (http) => 
{
    var dbContext = http.RequestServices.GetService<MockDbContext>();
    var fornecedores = await dbContext.Fornecedores.ToListAsync();
    await http.Response.WriteAsJsonAsync(fornecedores);
});

app.MapGet("/api/fornecedores/{id}", async (http) => 
{
    if (!http.Request.RouteValues.TryGetValue("id", out var id))
    {
        http.Response.StatusCode = 400;
        return;
    }
    
    int number;
    bool success = int.TryParse(id.ToString(), out number);
    if (!success)
    {
        http.Response.StatusCode = 400;
        return;
    }

    var dbContext = http.RequestServices.GetService<MockDbContext>();
    var fornecedor = await dbContext.Fornecedores.FindAsync(int.Parse(id.ToString()));
    if (fornecedor == null){
        http.Response.StatusCode = 404;
        return;
    }
    await http.Response.WriteAsJsonAsync(fornecedor);
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
