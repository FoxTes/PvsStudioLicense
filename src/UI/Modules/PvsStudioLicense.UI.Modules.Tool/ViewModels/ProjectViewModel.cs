namespace PvsStudioLicense.UI.Modules.Tool.ViewModels;

using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities;
using HandyControl.Controls;
using HandyControl.Data;
using Infrastructure.Constants;
using PvsStudioLicense.Shared.ViewModels;
using Reactive.Bindings;

/// <inheritdoc />
public class ProjectViewModel : ViewModelBase
{
    private readonly ReactiveProperty<bool> _commandCanExecuteState = new(true);

    /// <inheritdoc />
    public ProjectViewModel(
        IFileEditor fileEditor,
        IFileSearcher fileSearcher,
        Project project)
    {
        Project = project;

        AddСomment = _commandCanExecuteState
            .ToAsyncReactiveCommand()
            .WithSubscribe(async () =>
            {
                await fileSearcher.GetFiles(Project.Path)
                    .Bind(x => fileEditor.WriteCommentAsync(x, TypeLicense.OpenSource))
                    .Tap(ShowResultСommentNotification)
                    .OnFailure(error => Growl.Error(error));
            });

        DeleteComment = _commandCanExecuteState
            .ToAsyncReactiveCommand()
            .WithSubscribe(async () =>
            {
                await fileSearcher.GetFiles(Project.Path)
                    .Bind(x => fileEditor.DeleteCommentAsync(x, TypeLicense.OpenSource))
                    .Tap(ShowResultUncommentNotification)
                    .OnFailure(error => Growl.Error(error));
            });
    }

    /// <summary>
    /// Model.
    /// </summary>
    public Project Project { get; }

    /// <summary>
    /// Add comment.
    /// </summary>
    public AsyncReactiveCommand AddСomment { get; }

    /// <summary>
    /// Delete comment.
    /// </summary>
    public AsyncReactiveCommand DeleteComment { get; }

    private static void ShowResultСommentNotification(int count)
    {
        if (count > 0)
            Growl.Success(CreateGrowlInfo($"Успешно установлено коментариев - {count}."));
        else
            Growl.Warning(CreateGrowlInfo("Не удалось найти файлы для коментирования."));
    }

    private static void ShowResultUncommentNotification(int count)
    {
        if (count > 0)
            Growl.Success(CreateGrowlInfo($"Успешно удалено коментариев - {count}."));
        else
            Growl.Warning(CreateGrowlInfo("Не удалось найти файлы для разкоментирования."));
    }

    private static GrowlInfo CreateGrowlInfo(string message) => new() { Message = message, WaitTime = 3 };
}