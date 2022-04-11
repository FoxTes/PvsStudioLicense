namespace PvsStudioLicense.UI.Modules.Tool.ViewModels;

using System.Reactive.Linq;
using System.Windows.Forms;
using Domain.Abstractions;
using Domain.Models;
using Prism.Regions;
using Infrastructure.Constants;
using PvsStudioLicense.Shared.ViewModels;
using Reactive.Bindings;
using Shared.Constants;
using Views;

/// <summary>
/// View model for <see cref="ToolControl"/>.
/// </summary>
public class ToolControlViewModel : NavigationViewModelBase
{
    private readonly IProjectManager _projectManager;

    /// <inheritdoc />
    public ToolControlViewModel(
        IFileEditor fileEditor,
        IFileSearcher fileSearcher,
        IRegionManager regionManager,
        IProjectManager projectManager)
    {
        _projectManager = projectManager;

        Projects = _projectManager
            .GetAll()
            .SelectMany(x => x)
            .ToReactiveCollection();

        AddProject.WithSubscribe(async _ =>
        {
            var project = await ShowFolderBrowserDialog();
            if (project != null)
                Projects.AddOnScheduler(project);
        });

        RemoveProject.WithSubscribe(async project =>
        {
            await _projectManager.Delete(project);
            Projects.RemoveOnScheduler(project);
        });

        AddСomment.WithSubscribe(async project =>
        {
            var files = fileSearcher
                .GetFiles(project.Path)
                .ToArray();
            await fileEditor.WriteCommentAsync(files, TypeLicense.OpenSource);
        });

        DeleteComment.WithSubscribe(async project =>
        {
            var files = fileSearcher
                .GetFiles(project.Path)
                .ToArray();
            await fileEditor.DeleteCommentAsync(files, TypeLicense.OpenSource);
        });

        OpenSettings.Subscribe(() =>
            regionManager.RequestNavigate(RegionNames.MainContent, ModulesNames.Settings));
    }

    /// <summary>
    /// Projects.
    /// </summary>
    public ReactiveCollection<Project> Projects { get; set; }

    /// <summary>
    /// Open settings.
    /// </summary>
    public ReactiveCommand OpenSettings { get; } = new();

    /// <summary>
    /// Add comment.
    /// </summary>
    public AsyncReactiveCommand<Project> AddСomment { get; } = new();

    /// <summary>
    /// Delete comment.
    /// </summary>
    public AsyncReactiveCommand<Project> DeleteComment { get; } = new();

    /// <summary>
    /// Add project.
    /// </summary>
    public AsyncReactiveCommand AddProject { get; } = new();

    /// <summary>
    /// Remove project.
    /// </summary>
    public AsyncReactiveCommand<Project> RemoveProject { get; } = new();

    private async Task<Project> ShowFolderBrowserDialog()
    {
        using var dialog = new FolderBrowserDialog();
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        var dialogResult = dialog.ShowDialog();
        if (dialogResult != DialogResult.OK)
            return null;

        var projectCache = await _projectManager.Get(dialog.SelectedPath);
        if (projectCache is not null)
            return null;

        var project = new Project(dialog.SelectedPath);
        await _projectManager.Add(project);
        return project;
    }
}