namespace PvsStudioLicense.Infrastructure.Extensions;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension for <see cref="ServiceCollectionExtension"/>.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Added persistence.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        Akavache.Registrations.Start("PvsStudioLicense");

        return services;
    }
}