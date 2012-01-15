namespace Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ListDictionary<TKey, TValue> : IDictionary<TKey, IList<TValue>> 
    {
        private readonly Dictionary<TKey, IList<TValue>> dictionary = new Dictionary<TKey, IList<TValue>>();

        public void Add(TKey key)
        {
            if (!ContainsKey(key))
            {
                Add(key, new List<TValue>());
            }
        }

        public void Add(TKey key, TValue value)
        {
            IList<TValue> list;
            if (TryGetValue(key, out list))
            {
                list.Add(value);
            }
            else
            {
                Add(key, new List<TValue> { value });
            }
        }

        public void Clear(TKey key)
        {
            if (!ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            IList<TValue> list;
            if (TryGetValue(key, out list))
            {
                list.Clear();    
            }
        }

        public bool Remove(TKey key, TValue value)
        {
            if (!ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            return dictionary[key].Remove(value);
        }

        public bool Remove(TKey key, IList<TValue> values)
        {
            if (!ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            return dictionary[key].RemoveRange(values);
        }

        public bool RemoveAll(TValue value)
        {
            return Values.Select(v => v.RemoveAll(value)).All(t => t);
        }

        public bool ContainsValue(TValue value)
        {
            return Keys.Any(key => dictionary[key].Contains(value));
        }

        public bool ContainsKeyWithValue(TKey key, TValue value)
        {
            return Keys.Contains(key) && dictionary[key].Contains(value);
        }

        public bool ContainsKeyWithAllValues(TKey key, IList<TValue> values)
        {
            return values.Aggregate(true, (current, value) => current & ContainsKeyWithValue(key, value));
        }

        public IEnumerable<TValue> GetAllValues(Func<TValue, bool> filter)
        {
            return from valueList in Values 
                   from value in valueList 
                   where filter(value) 
                   select value;
        }

        public IEnumerable<TValue> GetAllValuesForKey(Func<TKey, bool> filter)
        {
            return Keys.Where(filter).SelectMany(key => dictionary[key]);
        }

        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<TKey, IList<TValue>>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,IList<TValue>>>

        public void Add(KeyValuePair<TKey, IList<TValue>> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, IList<TValue>> item)
        {
            return item.Value.Aggregate(true, (current, value) => current & ContainsKeyWithValue(item.Key, value));
        }

        public void CopyTo(KeyValuePair<TKey, IList<TValue>>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, IList<TValue>> item)
        {
            return Remove(item.Key, item.Value);
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IDictionary<TKey,IList<TValue>>

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Add(TKey key, IList<TValue> value)
        {
            if (ContainsKey(key))
            {
                dictionary[key].AddRange(value);
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out IList<TValue> value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public IList<TValue> this[TKey key]
        {
            get
            {
                return dictionary[key];
            }
            set
            {
                dictionary[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get { return this.dictionary.Keys; }
        }

        public ICollection<IList<TValue>> Values
        {
            get { return dictionary.Values; }
        }

        #endregion
    }
}
