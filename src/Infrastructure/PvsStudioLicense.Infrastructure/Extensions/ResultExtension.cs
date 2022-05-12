namespace PvsStudioLicense.Infrastructure.Extensions;

using CSharpFunctionalExtensions;

/// <summary>
/// Extension for <see cref="Result"/>.
/// </summary>
public static class ResultExtension
{
    /// <summary>
    /// Executes the given action if the calling result is a success and condition is true.
    /// Returns the calling result.
    /// </summary>
    /// <param name="result"><see cref="Result{T}"/>.</param>
    /// <param name="predicate">Predicate.</param>
    /// <param name="func">Action.</param>
    /// <typeparam name="T">Input type.</typeparam>
    /// <typeparam name="TK">Output type.</typeparam>
    public static Result<TK> MapIf<T, TK>(
        this Result<T> result,
        Func<T, bool> predicate,
        Func<T, TK> func)
    {
        return result.IsSuccess && predicate(result.Value)
            ? result.Map(func)
            : Result.Failure<TK>(result.Error);
    }
}