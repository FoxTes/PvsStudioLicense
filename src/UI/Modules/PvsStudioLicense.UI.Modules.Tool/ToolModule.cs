namespace PvsStudioLicense.UI.Modules.Tool;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Shared.Constants;
using ViewModels;
using Views;

/// <inheritdoc />
public class ToolModule : IModule
{
    private readonly IRegionManager _regionManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolModule"/> class.
    /// </summary>
    /// <param name="regionManager"><see cref="IRegionManager"/>.</param>
    public ToolModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    /// <inheritdoc />
    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RequestNavigate(RegionNames.MainContent, nameof(ToolControl));
    }

    /// <inheritdoc />
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ToolControl, ToolControlViewModel>();
    }
}