namespace PvsStudioLicense.Domain.Models;

/// <summary>
/// Project.
/// </summary>
public record Project
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    /// <param name="path">Path.</param>
    public Project(string path)
    {
        Path = path;
        Name = path;
    }

    /// <summary>
    /// Path.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// IsValid.
    /// </summary>
    public bool IsValid => GetStatusValid();

    /// <summary>
    /// IsPined.
    /// </summary>
    public bool IsPined { get; private set; }

    /// <summary>
    /// Changed status pined.
    /// </summary>
    /// <param name="status">Status.</param>
    public void ChangedStatusPined(bool status) => IsPined = status;

    private bool GetStatusValid() => Directory.Exists(Path);
}