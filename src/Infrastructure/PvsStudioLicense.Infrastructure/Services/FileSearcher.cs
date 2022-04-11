namespace PvsStudioLicense.Infrastructure.Services;

using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Domain.Abstractions;
using Scrutor.AspNetCore;

/// <inheritdoc cref="PvsStudioLicense.Domain.Abstractions.IFileSearcher" />
public class FileSearcher : IFileSearcher, ISingletonLifetime
{
    private readonly Regex _searchPattern = new(@"$(?<=\.(cs|txt))", RegexOptions.IgnoreCase);
    private readonly IFileSystem _fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSearcher"/> class.
    /// </summary>
    /// <param name="fileSystem"><see cref="FileSystem"/>.</param>
    public FileSearcher(IFileSystem fileSystem) => _fileSystem = fileSystem;

    /// <inheritdoc />
    public FileSearcher()
        : this(new FileSystem())
    {
    }

    /// <inheritdoc />
    public IEnumerable<string> GetFiles(string path)
    {
        return _fileSystem.Directory
            .EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
            .Where(f => _searchPattern.IsMatch(f));
    }
}