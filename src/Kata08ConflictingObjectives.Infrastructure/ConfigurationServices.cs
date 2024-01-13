using Kata08ConflictingObjectives.Application.Common.Data;
using Kata08ConflictingObjectives.Application.Common.Data.Repositories;
using Kata08ConflictingObjectives.Infrastructure.Data;
using Kata08ConflictingObjectives.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kata08ConflictingObjectives.Infrastructure;

public static class ConfigurationServices
{
    public static IServiceCollection AddInfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSource = Path.Combine(configuration.GetValue<string>("DataSource")!, "data");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                $"Data Source={dataSource}",
                x => x.MigrationsHistoryTable("__ef_migrations_history")
            )
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWordRepository, WordRepository>();
        
        return services;
    }
    
    public static void RunInfrastructurePersistenceMigration(this IHost host, bool isDevelopment)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
            if (isDevelopment)
            {
                // SeedData.PopulateTestData(context);
            }
        }
    }
}