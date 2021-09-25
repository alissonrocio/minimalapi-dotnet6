using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web;

public class MockDbContext : DbContext
{
    public MockDbContext(DbContextOptions options) : base(options) { }

    protected MockDbContext() { }

    public DbSet<Fornecedor> Fornecedores { get; set; }
}