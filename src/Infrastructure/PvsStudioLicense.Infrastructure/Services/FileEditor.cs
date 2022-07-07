namespace PvsStudioLicense.Infrastructure.Services;

using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Extensions;
using Scrutor.AspNetCore;
using Serilog;

/// <inheritdoc cref="PvsStudioLicense.Domain.Abstractions.IFileEditor" />
public class FileEditor : IFileEditor, ISingletonLifetime
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileEditor"/> class.
    /// </summary>
    public FileEditor()
    {
        _logger = Log.ForContext<FileSearcher>();
    }

    /// <inheritdoc />
    public Result<int> WriteComment(IEnumerable<string> files, string comment)
    {
        var count = 0;

        try
        {
            Parallel.ForEach(files, file =>
            {
                var currentContent = File.ReadAllText(file);
                var encoding = file.GetEncoding();

                if (currentContent.Contains(comment))
                    return;

                File.WriteAllText(file, comment + currentContent, encoding);
                Interlocked.Increment(ref count);
            });
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Не удается установить коментирий у файлов");
            return Result.Failure<int>("Не удается установить коментирий у файлов.");
        }

        return Result.Success(count);
    }

    /// <inheritdoc />
    public async Task<Result<int>> WriteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken)
    {
        var count = 0;

        try
        {
            await Parallel.ForEachAsync(files, cancellationToken, async (file, token) =>
            {
                var currentContent = await File.ReadAllTextAsync(file, token);
                var encoding = file.GetEncoding();

                if (!currentContent.Contains(comment))
                {
                    await File.WriteAllTextAsync(file, comment + currentContent, encoding, token);
                    Interlocked.Increment(ref count);
                }
            });
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Не удается установить коментирий у файлов");
            return Result.Failure<int>("Не удается установить коментирий у файлов.");
        }

        return Result.Success(count);
    }

    /// <inheritdoc />
    public Result<int> DeleteComment(IEnumerable<string> files, string comment)
    {
        var count = 0;

        try
        {
            Parallel.ForEach(files, file =>
            {
                var currentContent = File.ReadAllText(file);
                var encoding = file.GetEncoding();

                if (!currentContent.Contains(comment))
                    return;

                File.WriteAllText(file, currentContent[comment.Length..], encoding);
                Interlocked.Increment(ref count);
            });
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Не удается удалить коментирий у файлов");
            return Result.Failure<int>("Не удается удалить коментирий у файлов.");
        }

        return Result.Success(count);
    }

    /// <inheritdoc />
    public async Task<Result<int>> DeleteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken)
    {
        var count = 0;

        try
        {
            await Parallel.ForEachAsync(files, cancellationToken, async (file, token) =>
            {
                var currentContent = await File.ReadAllTextAsync(file, token);
                var encoding = file.GetEncoding();

                if (currentContent.Contains(comment))
                {
                    await File.WriteAllTextAsync(file, currentContent[comment.Length..], encoding, token);
                    Interlocked.Increment(ref count);
                }
            });
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Не удается удалить коментирий у файлов");
            return Result.Failure<int>("Не удается удалить коментирий у файлов.");
        }

        return Result.Success(count);
    }
}