namespace PvsStudioLicense.Domain.Abstractions;

using Entities;

/// <summary>
/// Project manager.
/// </summary>
public interface IProjectManager
{
    /// <summary>
    /// Get all project.
    /// </summary>
    IEnumerable<Project> GetAll();

    /// <summary>
    /// Get project.
    /// </summary>
    /// <param name="path">Path to project.</param>
    Project Get(string path);

    /// <summary>
    /// Add project.
    /// </summary>
    /// <param name="project">Project.</param>
    void Add(Project project);

    /// <summary>
    /// Delete project.
    /// </summary>
    /// <param name="project">Project.</param>
    void Delete(Project project);
}