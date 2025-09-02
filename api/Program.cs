using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using serversidevalidation;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();
        Configure(app);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>(options =>
            options.UseNpgsql("Server=ep-muddy-pine-adt1rxmn-pooler.c-2.us-east-1.aws.neon.tech;DB=neondb;UID=neondb_owner;PWD=npg_7ZhobSkaOmY1;SslMode=require"));

        services.AddScoped<IPetService, PetService>();

        services.AddOpenApiDocument();

        services.AddControllers();

    }

    public static void Configure(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                db.Database.EnsureCreated();
                Console.WriteLine(db.Database.GenerateCreateScript());
                

        }

        app.UseOpenApi();
        app.UseSwaggerUi();
        app.MapControllers();

        app.Run();

    }
}



