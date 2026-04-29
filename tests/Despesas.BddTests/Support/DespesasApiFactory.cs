using Despesas.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Despesas.BddTests.Support;

public class DespesasApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"DespesasTestDb-{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DespesasDbContext>));
            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<DespesasDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName));
        });
    }
}
