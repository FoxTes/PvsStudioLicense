namespace PvsStudioLicense.Infrastructure.Extensions;

using System.Text;

/// <summary>
/// TODO.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Determines a text file's encoding by analyzing its byte order mark (BOM).
    /// </summary>
    /// <param name="file">The text file to analyze.</param>
    /// <returns>The detected encoding.</returns>
    public static Encoding GetEncoding(this string file)
    {
        var utf8NoBom = new UTF8Encoding(false);

        using var reader = new StreamReader(file, utf8NoBom);
        reader.Read();

        return Equals(reader.CurrentEncoding, utf8NoBom)
            ? new UTF8Encoding(false)
            : new UTF8Encoding(true);
    }
}