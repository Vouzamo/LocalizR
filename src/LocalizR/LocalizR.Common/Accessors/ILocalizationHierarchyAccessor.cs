using LocalizR.Common.Models;

namespace LocalizR.Common.Accessors
{
    public interface ILocalizationHierarchyAccessor<T>
    {
        public Hierarchy<T> GetHierarchy();
    }
}
