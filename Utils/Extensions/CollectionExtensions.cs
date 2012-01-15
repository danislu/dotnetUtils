namespace System.Collections.Generic
{
    using System.Linq;

    public static class CollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var each in items)
            {
                collection.Add(each);
            }
            return collection;
        }

        public static bool Remove<T>(this ICollection<T> collection, Func<T, bool> filter)
        {
            var items = collection.Where(filter);
            return items.Aggregate(items.Any(), (current, item) => current & collection.Remove(item));
        }

        public static bool RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            var result = false;
            foreach (var item in items)
            {
                if (collection.Remove(item))
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool RemoveAll<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
            {
                return false;
            }

            while (collection.Remove(item)) { }
            return true;
        }
    }
}
