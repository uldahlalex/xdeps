using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using serversidevalidation;

namespace tests;

public class Setup : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScope _testScope;

    public Setup()
    {
        var services = new ServiceCollection();
        Program.ConfigureServices(services);
        services.RemoveAll(typeof(MyDbContext));
        
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();
        services.AddDbContext<MyDbContext>(options =>
            options.UseSqlite(_connection));
        
        _serviceProvider = services.BuildServiceProvider();
        _testScope = _serviceProvider.CreateScope();
        
        // Create database schema
        var context = GetService<MyDbContext>();
        context.Database.EnsureCreated();
    }
    
    public T GetService<T>() where T : notnull
    {
        return _testScope.ServiceProvider.GetRequiredService<T>();
    }
    
    public void Dispose()
    {
        _testScope?.Dispose();
        (_serviceProvider as IDisposable)?.Dispose();
        _connection?.Dispose();
    }
}