using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using web.Models;
using Xunit;
using System.Linq;

namespace tests;

public class UnitTestFornecedor
{
    [Fact]
    public async Task GetAll()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var fornecedores = await client.GetFromJsonAsync<List<Fornecedor>>("/api/fornecedores");
        Assert.True(fornecedores.Count > 0 );
    }

    public async Task GetById()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var fornecedores = await client.GetFromJsonAsync<Fornecedor>("/api/fornecedores/1");
        Assert.Equal(fornecedores.Nome,"Jason Bournes");
    }
}