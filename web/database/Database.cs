using System.Text.Json;
using web.Models;

namespace web.database
{
    public static class Database
    {
        public static void Preparacao(IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.CreateScope())
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
}
