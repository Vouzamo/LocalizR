using LocalizR.Common.Accessors;
using System.Globalization;

namespace LocalizR.Common.Services
{
    public class CultureInfoLocalizationService : DefaultLocalizationService<CultureInfo>
    {
        public CultureInfoLocalizationService(ILocalizationHierarchyAccessor<CultureInfo> localizationHierarchyAccessor, ILocalizationAccessor<CultureInfo> localizationAccessor) : base(localizationHierarchyAccessor, localizationAccessor)
        {

        }

        public override string GetKey(CultureInfo localization)
        {
            return localization.Name;
        }
    }
}