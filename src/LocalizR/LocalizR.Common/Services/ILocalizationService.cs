using LocalizR.Common.Models;

namespace LocalizR.Common.Services
{
    public interface ILocalizationService
    {
        T? Localize<T>(Localizable<T> localizable, T? defaultValue = default);
    }
}
