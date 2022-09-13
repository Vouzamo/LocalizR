using LocalizR.Common.Models;

namespace LocalizR.Common.Services
{
    public class ChainLocalizationService : ILocalizationService
    {
        private IEnumerable<string> Chain { get; }

        public ChainLocalizationService(IEnumerable<string> chain)
        {
            Chain = chain;
        }

        public T? Localize<T>(Localizable<T> localizable, T? defaultValue = default)
        {
            foreach (var link in Chain)
            {
                if (localizable.TryGetValue(link, out T? value))
                {
                    return value;
                }
            }

            return defaultValue;
        }
    }
}
