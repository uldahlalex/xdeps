using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using serversidevalidation;

namespace xdeps;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Program.ConfigureServices(services);
        services.RemoveAll(typeof(MyDbContext));
        
        // Register a scoped DbContext factory that creates a new database per test
        services.AddScoped<MyDbContext>(provider =>
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(connection)
                .Options;
            var context = new MyDbContext(options);
            context.Database.EnsureCreated();
            return context;
        });
        
        // Remove scoped PetService registration and re-add as scoped for per-test isolation
        services.RemoveAll(typeof(IPetService));
        services.AddScoped<IPetService, PetService>();
    }
}