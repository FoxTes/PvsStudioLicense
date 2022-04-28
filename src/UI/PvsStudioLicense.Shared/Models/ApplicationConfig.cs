namespace PvsStudioLicense.Shared.Models;

using System.Text.Json;
using Application.Enums;
using HandyControl.Tools;

/// <inheritdoc />
public class ApplicationConfig : GlobalDataHelper
{
    /// <summary>
    /// Language app.
    /// </summary>
    public Language Language { get; set; }

    /// <inheritdoc />
    public override string FileName { get; set; }

    /// <inheritdoc />
    public override JsonSerializerOptions JsonSerializerOptions { get; set; }

    /// <inheritdoc />
    public override int FileVersion { get; set; }
}