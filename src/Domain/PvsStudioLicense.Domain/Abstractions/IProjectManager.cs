namespace PvsStudioLicense.Domain.Abstractions;

using Models;

/// <summary>
/// TODO.
/// </summary>
public interface IProjectManager
{
    /// <summary>
    /// TODO.
    /// </summary>
    IObservable<IEnumerable<Project>> GetAll();

    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="key">Todo.</param>
    Task<Project> Get(string key);

    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="project">Todo.</param>
    Task Add(Project project);

    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="project">Todo.</param>
    Task Delete(Project project);
}