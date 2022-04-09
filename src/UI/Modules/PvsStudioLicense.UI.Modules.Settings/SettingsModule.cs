namespace PvsStudioLicense.UI.Modules.Settings;

using Prism.Ioc;
using Prism.Modularity;
using Views;
using ViewModels;

/// <inheritdoc />
public class SettingsModule : IModule
{
    /// <inheritdoc />
    public void OnInitialized(IContainerProvider containerProvider)
    {
    }

    /// <inheritdoc />
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<SettingsControl, SettingsControlViewModel>();
    }
}