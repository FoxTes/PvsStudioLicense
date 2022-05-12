namespace PvsStudioLicense.Domain.Abstractions;

using CSharpFunctionalExtensions;

/// <summary>
/// File editor.
/// </summary>
public interface IFileEditor
{
    /// <summary>
    /// Sets the commentary to the specified file..
    /// </summary>
    /// <param name="files">Files.</param>
    /// <param name="comment">Comment.</param>
    Result<int> WriteComment(IEnumerable<string> files, string comment);

    /// <summary>
    /// Asynchronously sets a comment to the specified file.
    /// </summary>
    /// <param name="files">Files.</param>
    /// <param name="comment">Comment.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<Result<int>> WriteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleted the commentary to the specified file..
    /// </summary>
    /// <param name="files">Files.</param>
    /// <param name="comment">Comment.</param>
    Result<int> DeleteComment(IEnumerable<string> files, string comment);

    /// <summary>
    /// Asynchronously deleted a comment to the specified file.
    /// </summary>
    /// <param name="files">Files.</param>
    /// <param name="comment">Comment.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<Result<int>> DeleteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken = default);
}