namespace PvsStudioLicense.UI.Modules.TreeStructure;

using Prism.Ioc;
using Prism.Modularity;
using ViewModels;
using Views;

/// <inheritdoc />
public class TreeStructureModule : IModule
{
    /// <inheritdoc />
    public void OnInitialized(IContainerProvider containerProvider)
    {
    }

    /// <inheritdoc />
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<TreeStructureControl, TreeStructureControlViewModel>();
    }
}