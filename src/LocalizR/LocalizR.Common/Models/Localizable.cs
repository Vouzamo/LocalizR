namespace LocalizR.Common.Models
{

    public class Localizable<T> : Dictionary<string, T>, ILocalizable<T>
    {
        
    }
}