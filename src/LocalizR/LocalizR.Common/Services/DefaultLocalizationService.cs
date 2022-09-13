using LocalizR.Common.Accessors;
using LocalizR.Common.Models;

namespace LocalizR.Common.Services
{
    public abstract class DefaultLocalizationService<TLocalization> : ILocalizationService
    {
        private ILocalizationHierarchyAccessor<TLocalization> LocalizationHierarchyAccessor { get; }
        private ILocalizationAccessor<TLocalization> LocalizationAccessor { get; }

        public DefaultLocalizationService(ILocalizationHierarchyAccessor<TLocalization> localizationHierarchyAccessor, ILocalizationAccessor<TLocalization> localizationAccessor)
        {
            LocalizationHierarchyAccessor = localizationHierarchyAccessor;
            LocalizationAccessor = localizationAccessor;
        }

        public T? Localize<T>(Localizable<T> localizable, T? defaultValue = default)
        {
            var localizationHierarchy = LocalizationHierarchyAccessor.GetHierarchy();
            var localization = LocalizationAccessor.GetLocalization();

            if (localizationHierarchy.TryFindDependencyChain(ci => ci.Equals(localization), out var chain))
            {
                foreach (var link in chain)
                {
                    var key = GetKey(link);

                    if (localizable.TryGetValue(key, out T? value))
                    {
                        return value;
                    }
                }
            }

            return defaultValue;
        }

        public abstract string GetKey(TLocalization localization);
    }
}