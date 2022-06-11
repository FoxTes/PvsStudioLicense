namespace PvsStudioLicense.Infrastructure.Constants;

/// <summary>
/// License type.
/// </summary>
public static class TypeLicenseDescription
{
    /// <summary>
    /// Individual.
    /// </summary>
    public const string Individual = "// This is an independent project of an individual developer. " + MainText;

    /// <summary>
    /// OpenSource.
    /// </summary>
    public const string OpenSource = "// This is an open source non-commercial project. " + MainText;

    /// <summary>
    /// Student.
    /// </summary>
    public const string Student = "// This is a personal academic project. " + MainText;

    private const string MainText =
        "Dear PVS-Studio, please check it." + "\r" + "\n" + "\r" + "\n" +
        "// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com" + "\r" + "\n" + "\r" + "\n";
}