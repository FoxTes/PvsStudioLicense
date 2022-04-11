namespace PvsStudioLicense.Domain.Abstractions;

/// <summary>
/// Todo.
/// </summary>
public interface IFileEditor
{
    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="files"></param>
    /// <param name="comment"></param>
    void WriteComment(IEnumerable<string> files, string comment);

    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="files"></param>
    /// <param name="comment"></param>
    /// <param name="cancellationToken"></param>
    Task WriteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="files"></param>
    /// <param name="comment"></param>
    void DeleteComment(IEnumerable<string> files, string comment);

    /// <summary>
    /// TODO.
    /// </summary>
    /// <param name="files"></param>
    /// <param name="comment"></param>
    /// <param name="cancellationToken"></param>
    Task DeleteCommentAsync(
        IEnumerable<string> files,
        string comment,
        CancellationToken cancellationToken = default);
}