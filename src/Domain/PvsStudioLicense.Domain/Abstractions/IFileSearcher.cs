namespace PvsStudioLicense.Domain.Abstractions;

using CSharpFunctionalExtensions;

/// <summary>
/// File searcher.
/// </summary>
public interface IFileSearcher
{
    /// <summary>
    /// Returns an enumerable collection of full file names that match a search pattern in a specified path,
    /// and optionally searches subdirectories.
    /// </summary>
    /// <param name="path">The relative or absolute path to the directory to search.
    /// This string is not case-sensitive.</param>
    /// <returns> An enumerable collection of the full names (including paths)
    /// for the files in the directory.</returns>
    Result<IEnumerable<string>> GetFiles(string path);
}