using LocalizR.Common.Models;

namespace LocalizR.Common.Services
{
    public class CurrentCultureLocalizationService : ILocalizationService
    {
        public T? Localize<T>(Localizable<T> localizable, T? defaultValue = default)
        {
            var locale = Thread.CurrentThread.CurrentCulture;

            if(localizable.TryGetValue(locale.Name, out T? value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}
