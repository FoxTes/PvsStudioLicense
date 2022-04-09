namespace PvsStudioLicense.UI.Modules.Settings.ViewModels;

using Application.Enums;
using FastEnumUtility;
using HandyControl.Tools;
using Prism.Regions;
using Shared.Models;
using Reactive.Bindings;
using Shared.Constants;
using Shared.ViewModels;

/// <summary>
/// View model for <see cref="PvsStudioLicense.UI.Modules.Settings.Views.SettingsControl"/>.
/// </summary>
public class SettingsControlViewModel : NavigationViewModelBase
{
    /// <inheritdoc />
    public SettingsControlViewModel(IRegionManager regionManager)
    {
        LanguageSelected.Subscribe(language =>
        {
            ConfigHelper.Instance.SetLang(language.GetEnumMemberValue());

            var settings = GlobalDataHelper.Load<ApplicationConfig>();
            settings.Language = language;
            settings.Save();
        });
        Back.Subscribe(() =>
            regionManager.RequestNavigate(RegionNames.MainContent, ModulesNames.Tool));
    }

    /// <summary>
    /// Open settings.
    /// </summary>
    public ReactiveProperty<Language> LanguageSelected { get; } = new(GetCurrentLanguage());

    /// <summary>
    /// Open settings.
    /// </summary>
    public ReactiveCommand Back { get; } = new();

    private static Language GetCurrentLanguage()
    {
        var settings = GlobalDataHelper.Load<ApplicationConfig>();
        return settings.Language;
    }
}