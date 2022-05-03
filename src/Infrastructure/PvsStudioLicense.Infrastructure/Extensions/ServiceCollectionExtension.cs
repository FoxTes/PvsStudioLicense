namespace PvsStudioLicense.Infrastructure.Extensions;

using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension for <see cref="ServiceCollectionExtension"/>.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Added service <see cref="IFileSystem"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    public static IServiceCollection AddFileSystem(this IServiceCollection services)
    {
        services.AddTransient<IFileSystem, FileSystem>();

        return services;
    }
}