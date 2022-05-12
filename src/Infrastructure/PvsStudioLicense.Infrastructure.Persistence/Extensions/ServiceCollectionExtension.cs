namespace PvsStudioLicense.Infrastructure.Persistence.Extensions;

using Contexts;
using EntityFramework.Exceptions.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

/// <summary>
/// Extension for <see cref="ServiceCollectionExtension"/>.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Added persistence.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
            options
                .UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                .UseExceptionProcessor()
                .LogTo(Log.Logger.Information, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

        return services;
    }
}