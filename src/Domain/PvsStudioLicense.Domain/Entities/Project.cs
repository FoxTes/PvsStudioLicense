namespace PvsStudioLicense.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Project.
/// </summary>
[Index(nameof(Path), IsUnique = true)]
public class Project : Entity
{
    /// <summary>
    /// Path.
    /// </summary>
    [Required]
    public string Path { get; private init; } = null!;

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; private init; } = null!;

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
            Name = System.IO.Path.GetFileName(path)
        };
    }

    /// <summary>
    /// Changed status pined.
    /// </summary>
    /// <param name="status">Status.</param>
    public Result ChangedStatusPined(bool status)
    {
        IsPined = status;
        return Result.Success();
    }

    private bool GetStatusValid() => Directory.Exists(Path);
}