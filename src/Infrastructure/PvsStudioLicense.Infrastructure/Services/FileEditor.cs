namespace PvsStudioLicense.Infrastructure.Services;

using Domain.Abstractions;
using Extensions;
using Scrutor.AspNetCore;

/// <inheritdoc cref="PvsStudioLicense.Domain.Abstractions.IFileEditor" />
public class FileEditor : IFileEditor, ISingletonLifetime
{
    /// <inheritdoc />
    public void WriteComment(IEnumerable<string> files, string comment)
    {
        Parallel.ForEach(files, file =>
        {
            var currentContent = File.ReadAllText(file);
            var encoding = file.GetEncoding();

            if (!currentContent.Contains(comment))
                File.WriteAllText(file, comment + currentContent, encoding);
        });
    }

    /// <inheritdoc />
    public async Task WriteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(files, cancellationToken, async (file, token) =>
        {
            var currentContent = await File.ReadAllTextAsync(file, token);
            var encoding = file.GetEncoding();

            if (!currentContent.Contains(comment))
                await File.WriteAllTextAsync(file, comment + currentContent, encoding, token);
        });
    }

    /// <inheritdoc />
    public void DeleteComment(IEnumerable<string> files, string comment)
    {
        Parallel.ForEach(files, file =>
        {
            var currentContent = File.ReadAllText(file);
            var encoding = file.GetEncoding();

            if (currentContent.Contains(comment))
                File.WriteAllText(file, currentContent[comment.Length..], encoding);
        });
    }

    /// <inheritdoc />
    public async Task DeleteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(files, cancellationToken, async (file, token) =>
        {
            var currentContent = await File.ReadAllTextAsync(file, token);
            var encoding = file.GetEncoding();

            if (currentContent.Contains(comment))
                await File.WriteAllTextAsync(file, currentContent[comment.Length..], encoding, token);
        });
    }
}