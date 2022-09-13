using System.Globalization;

namespace LocalizR.Common.Accessors
{
    public class CurrentThreadLocalizationAccessor : ILocalizationAccessor<CultureInfo>
    {
        public CultureInfo GetLocalization()
        {
            return Thread.CurrentThread.CurrentCulture;
        }
    }
}
