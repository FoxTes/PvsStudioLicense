namespace PvsStudioLicense.Infrastructure.Services
{
    using System.IO.Abstractions;
    using System.Text.RegularExpressions;
    using CSharpFunctionalExtensions;
    using Domain.Abstractions;
    using Scrutor.AspNetCore;
    using Serilog;

    /// <inheritdoc cref="PvsStudioLicense.Domain.Abstractions.IFileSearcher" />
    public class FileSearcher : IFileSearcher, ISingletonLifetime
    {
        private readonly Regex _searchPattern = new(@"$(?<=\.(cs|txt))", RegexOptions.IgnoreCase);
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSearcher"/> class.
        /// </summary>
        /// <param name="fileSystem"><see cref="FileSystem"/>.</param>
        public FileSearcher(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _logger = Log.ForContext<FileSearcher>();
        }

        /// <inheritdoc />
        public Result<IEnumerable<string>> GetFiles(string path)
        {
            try
            {
                var result = _fileSystem.Directory
                    .EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(f => _searchPattern.IsMatch(f));

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Не удается получить доступ к файлам.");
                return Result.Failure<IEnumerable<string>>("Не удается получить доступ к файлам.");
            }
        }
    }
}