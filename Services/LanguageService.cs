using Kolm_keelt.Resources.Localization;
using System.Globalization;


namespace Kolm_keelt.Services
{
    public static class LanguageService
    {
        public static event Action? LanguageChanged;

        public static void ChangeLanguage(string languageCode)
        {
            var culture = new CultureInfo(languageCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            AppResources.Culture = culture;
            LanguageChanged?.Invoke();
            Preferences.Set("AppLanguage", languageCode);
        }
    }
}
