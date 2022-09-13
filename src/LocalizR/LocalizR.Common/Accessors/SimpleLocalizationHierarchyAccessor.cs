using LocalizR.Common.Models;
using System.Globalization;

namespace LocalizR.Common.Accessors
{
    public class SimpleLocalizationHierarchyAccessor<T> : ILocalizationHierarchyAccessor<T>
    {
        private Hierarchy<T> Hierarchy { get; }

        public SimpleLocalizationHierarchyAccessor(Hierarchy<T> hierarchy)
        {
            Hierarchy = hierarchy;
        }

        public Hierarchy<T> GetHierarchy()
        {
            return Hierarchy;
        }
    }
}
