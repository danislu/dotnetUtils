namespace Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;
        private ReadOnlyCollection<TKey> keys;
        private ReadOnlyCollection<TValue> values;

        public ReadOnlyDictionary()
        {
            this.dictionary = new Dictionary<TKey, TValue>();
        }

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.dictionary as IEnumerable).GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException("Not supported by a Read-Only Dictionary");
        }

        public void Clear()
        {
            throw new NotSupportedException("Not supported by a Read-Only Dictionary");
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException("Not supported by a Read-Only Dictionary");
        }

        public int Count
        {
            get { return this.dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException("Not supported by a Read-Only Dictionary");
        }

        public bool Remove(TKey key)
        {
            throw new NotSupportedException("Not supported by a Read-Only Dictionary");
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return this.dictionary[key]; }
            set { throw new NotSupportedException("Not supported by a Read-Only Dictionary"); }
        }

        public ICollection<TKey> Keys
        {
            get { return this.keys ?? (this.keys = new ReadOnlyCollection<TKey>(this.dictionary.Keys.ToList())); }
        }

        public ICollection<TValue> Values
        {
            get { return this.values ?? (this.values = new ReadOnlyCollection<TValue>(this.dictionary.Values.ToList())); }
        }
    }
}
