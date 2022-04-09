namespace PvsStudioLicense.Infrastructure.Services;

using System.Reactive.Linq;
using Akavache;
using Domain.Abstractions;
using Domain.Models;
using Scrutor.AspNetCore;

/// <inheritdoc cref="PvsStudioLicense.Domain.Abstractions.IProjectManager" />
public class ProjectManager : IProjectManager, ISingletonLifetime
{
    /// <inheritdoc />
    public IObservable<IEnumerable<Project>> GetAll() =>
        BlobCache.LocalMachine.GetAllObjects<Project>();

    /// <inheritdoc />
    public async Task<Project> Get(string key)
    {
        try
        {
            return await BlobCache.LocalMachine.GetObject<Project>(key);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task Add(Project project) =>
        await BlobCache.LocalMachine.InsertObject(project.Path, project);

    /// <inheritdoc />
    public async Task Delete(Project project) =>
        await BlobCache.LocalMachine.InvalidateObject<Project>(project.Path);
}