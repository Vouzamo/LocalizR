namespace LocalizR.Common.Models
{
    public class Hierarchy<T> : List<Hierarchy<T>>
    {
        public T Value { get; set; }

        public Hierarchy(T value)
        {
            Value = value;
        }

        public bool TryFindDependencyChain(Func<T, bool> predicate, out List<T> chain)
        {
            chain = new List<T>();

            if (predicate.Invoke(Value))
            {
                chain.Add(Value);

                return true;
            }

            foreach (var prospect in this)
            {
                if (prospect.TryFindDependencyChain(predicate, out chain))
                {
                    chain.Add(Value);

                    return true;
                }
            }

            return false;
        }
    }
}