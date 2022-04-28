namespace PvsStudioLicense.UI.Modules.Tool.ViewModels;

using System.Reactive.Linq;
using System.Windows.Forms;
using Domain.Abstractions;
using Domain.Entities;
using Prism.Regions;
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

        Projects = Observable
            .FromAsync(() => Task.FromResult(_projectManager.GetAll().ToList()))
            .SelectMany(x => x)
            .ToReactiveCollection();

        AddProject.Subscribe(_ =>
        {
            var project = ShowFolderBrowserDialog();
            if (project != null)
                Projects.Add(project);
        });

        RemoveProject.Subscribe(viewModel =>
        {
            _projectManager.Delete(viewModel.Project);
            Projects.Remove(viewModel.Project);
        });

        ProjectViewModels = Projects
            .ToReadOnlyReactiveCollection(x => new ProjectViewModel(fileEditor, fileSearcher, x));

        OpenSettings.Subscribe(() =>
            regionManager.RequestNavigate(RegionNames.MainContent, ModulesNames.Settings));

        OpenStructure.Subscribe(viewModel =>
            regionManager.RequestNavigate(
                RegionNames.MainContent,
                ModulesNames.Structure,
                new NavigationParameters { { "Path", viewModel.Project.Path } }));
    }

    /// <summary>
    /// Project view models.
    /// </summary>
    public ReadOnlyReactiveCollection<ProjectViewModel> ProjectViewModels { get; }

    /// <summary>
    /// Open settings.
    /// </summary>
    public ReactiveCommand OpenSettings { get; } = new();

    /// <summary>
    /// Add project.
    /// </summary>
    public ReactiveCommand AddProject { get; } = new();

    /// <summary>
    /// Remove project.
    /// </summary>
    public ReactiveCommand<ProjectViewModel> RemoveProject { get; } = new();

    /// <summary>
    /// Open settings.
    /// </summary>
    public ReactiveCommand<ProjectViewModel> OpenStructure { get; } = new();

    /// <summary>
    /// Projects.
    /// </summary>
    private ReactiveCollection<Project> Projects { get; }

    private Project ShowFolderBrowserDialog()
    {
        using var dialog = new FolderBrowserDialog();
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        var dialogResult = dialog.ShowDialog();
        if (dialogResult != DialogResult.OK)
            return null;

        var projectCache = _projectManager.Get(dialog.SelectedPath);
        if (projectCache is not null)
            return null;

        var project = Project.Create(dialog.SelectedPath);
        _projectManager.Add(project);
        return project;
    }
}