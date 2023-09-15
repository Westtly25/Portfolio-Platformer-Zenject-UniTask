using Scripts.Model.Definitions.Localization;

namespace Scripts.Utils
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string key)
        {
            return LocalizationManager.I.Localize(key);
        }
    }
}