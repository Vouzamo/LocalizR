using LocalizR.Common.Models;
using System.Globalization;

namespace LocalizR.Common.Accessors
{
    public class DefaultLocalizationHierarchyAccessor<T> : ILocalizationHierarchyAccessor<T>
    {
        private Hierarchy<T> Hierarchy { get; }

        public DefaultLocalizationHierarchyAccessor(Hierarchy<T> hierarchy)
        {
            Hierarchy = hierarchy;
        }

        public Hierarchy<T> GetHierarchy()
        {
            return Hierarchy;
        }
    }
}
