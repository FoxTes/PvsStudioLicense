namespace PvsStudioLicense.UI.Modules.Tool.ViewModels;

using System.Reactive.Linq;
using System.Windows.Forms;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities;
using HandyControl.Controls;
using Infrastructure.Extensions;
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
    /// <inheritdoc />
    public ToolControlViewModel(
        IFileEditor fileEditor,
        IFileSearcher fileSearcher,
        IRegionManager regionManager,
        IProjectManager projectManager)
    {
        Projects = Observable
            .FromAsync(() => Task.FromResult(projectManager.GetAll().Value))
            .SelectMany(x => x)
            .ToReactiveCollection();

        ProjectViewModels = Projects
            .ToReadOnlyReactiveCollection(x => new ProjectViewModel(fileEditor, fileSearcher, x));

        PinedProject.Subscribe(viewModel =>
        {
            viewModel.Project.ChangedStatusPined(true)
                .Bind(() => projectManager.Update(viewModel.Project))
                .OnFailure(error => Growl.Error(error));
        });

        AddProject.Subscribe(_ =>
        {
            ShowFolderBrowserDialog()
                .MapIf(x => projectManager.Get(x).IsSuccess, Project.Create)
                .TapIf(project => projectManager.Add(project).IsSuccess, project => Projects.Add(project))
                .OnFailure(error =>
                {
                    if (error != Result.Configuration.ErrorMessagesSeparator)
                        Growl.Error(error);
                });
        });

        RemoveProject.Subscribe(viewModel =>
        {
            projectManager.Delete(viewModel.Project)
                .Tap(() => Projects.Remove(viewModel.Project))
                .OnFailure(error => Growl.Error(error));
        });

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
    /// Pined project.
    /// </summary>
    public ReactiveCommand<ProjectViewModel> PinedProject { get; } = new();

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

    private static Result<string> ShowFolderBrowserDialog()
    {
        using var dialog = new FolderBrowserDialog();
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        var dialogResult = dialog.ShowDialog();
        return dialogResult != DialogResult.OK
            ? Result.Failure<string>(Result.Configuration.ErrorMessagesSeparator)
            : Result.Success(dialog.SelectedPath);
    }
}