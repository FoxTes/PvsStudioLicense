namespace PvsStudioLicense.UI.Modules.Tool.ViewModels;

using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Constants;
using PvsStudioLicense.Shared.ViewModels;
using Reactive.Bindings;

/// <inheritdoc />
public class ProjectViewModel : ViewModelBase
{
    /// <inheritdoc />
    public ProjectViewModel(
        IFileEditor fileEditor,
        IFileSearcher fileSearcher,
        Project project)
    {
        Project = project;

        AddСomment.WithSubscribe(async _ =>
        {
            var files = fileSearcher
                .GetFiles(Project.Path)
                .ToArray();
            await fileEditor.WriteCommentAsync(files, TypeLicense.OpenSource);
        });

        DeleteComment.WithSubscribe(async _ =>
        {
            var files = fileSearcher
                .GetFiles(Project.Path)
                .ToArray();
            await fileEditor.DeleteCommentAsync(files, TypeLicense.OpenSource);
        });
    }

    /// <summary>
    /// Model.
    /// </summary>
    public Project Project { get; }

    /// <summary>
    /// Add comment.
    /// </summary>
    public AsyncReactiveCommand AddСomment { get; } = new();

    /// <summary>
    /// Delete comment.
    /// </summary>
    public AsyncReactiveCommand DeleteComment { get; } = new();
}