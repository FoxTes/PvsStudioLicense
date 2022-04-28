namespace PvsStudioLicense.Domain.Entities;

using Common;

/// <summary>
/// Project.
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    /// Path.
    /// </summary>
    public string Path { get; private init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; private init; }

    /// <summary>
    /// IsValid.
    /// </summary>
    public bool IsValid => GetStatusValid();

    /// <summary>
    /// IsPined.
    /// </summary>
    public bool IsPined { get; private set; }

    /// <summary>
    /// Create project.
    /// </summary>
    /// <param name="path">Path.</param>
    public static Project Create(string path)
    {
        return new Project
        {
            Path = path,
            Name = path
        };
    }

    /// <summary>
    /// Changed status pined.
    /// </summary>
    /// <param name="status">Status.</param>
    public void ChangedStatusPined(bool status) => IsPined = status;

    private bool GetStatusValid() => Directory.Exists(Path);
}