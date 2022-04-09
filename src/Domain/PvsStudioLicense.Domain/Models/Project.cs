namespace PvsStudioLicense.Domain.Models;

/// <summary>
/// TODO.
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
    /// TODO.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// TODO.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// TODO.
    /// </summary>
    public bool IsValid => GetStatusValid();

    /// <summary>
    /// TODO.
    /// </summary>
    public bool IsPined { get; private set; }

    /// <summary>
    /// Changed status pined.
    /// </summary>
    /// <param name="status">Status.</param>
    public void ChangedStatusPined(bool status) => IsPined = status;

    private bool GetStatusValid() => Directory.Exists(Path);
}