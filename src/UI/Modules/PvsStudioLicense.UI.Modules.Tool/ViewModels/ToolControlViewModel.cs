namespace PvsStudioLicense.UI.Modules.Tool.ViewModels
{
    using System.Reactive.Linq;
    using System.Windows.Forms;
    using Domain.Abstractions;
    using Domain.Models;
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
        private ToolControlViewModel(
            IRegionManager regionManager,
            IProjectManager projectManager)
        {
            _projectManager = projectManager;

            Projects = _projectManager
                .GetAll()
                .SelectMany(x => x)
                .ToReactiveCollection();

            RemoveProject.WithSubscribe(x =>
            {
                _projectManager.Delete(x);
                Projects.RemoveOnScheduler(x);
            });

            OpenSettings.Subscribe(() =>
                regionManager.RequestNavigate(RegionNames.MainContent, ModulesNames.Settings));
        }

        /// <summary>
        /// Projects.
        /// </summary>
        public ReactiveCollection<Project> Projects { get; }

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
        public ReactiveCommand<Project> RemoveProject { get; } = new();

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

            var project = new Project
            {
                Name = dialog.SelectedPath,
                Path = dialog.SelectedPath
            };
            await _projectManager.Add(project);
            return project;
        }
    }
}