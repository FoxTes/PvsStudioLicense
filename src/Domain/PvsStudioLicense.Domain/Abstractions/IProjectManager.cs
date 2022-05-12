namespace PvsStudioLicense.Domain.Abstractions;

using CSharpFunctionalExtensions;
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
    Result<Project> Get(string path);

    /// <summary>
    /// Add project.
    /// </summary>
    /// <param name="project">Project.</param>
    Result Add(Project project);

    /// <summary>
    /// Update project.
    /// </summary>
    /// <param name="project">Project.</param>
    Result Update(Project project);

    /// <summary>
    /// Delete project.
    /// </summary>
    /// <param name="project">Project.</param>
    Result Delete(Project project);
}