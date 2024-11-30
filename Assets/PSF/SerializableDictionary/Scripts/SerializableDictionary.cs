using System;
using System.Collections.Generic;
using UnityEngine;

namespace SerializableDictionary.Scripts
{
    [Serializable] 
    public struct KeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey _key;
        [SerializeField] private TValue _value;
        public TKey Key => _key;
        public TValue Value => _value;
        
        public KeyValuePair(System.Collections.Generic.KeyValuePair<TKey, TValue> pair)
        {
            _key = pair.Key;
            _value = pair.Value;
        }
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField] private KeyValuePair<TKey, TValue>[] _pairs;
        private Dictionary<TKey, TValue> _dictionary;

        public Dictionary<TKey, TValue> Dictionary
        {
            get
            {
                if (_dictionary != null)
                    return _dictionary;

                _dictionary = new Dictionary<TKey, TValue>(_pairs.Length);
                for (int i = 0; i < _pairs.Length; i++)
                    _dictionary.Add(_pairs[i].Key, _pairs[i].Value);

                return _dictionary;
            }
        }

        public int Count => Dictionary.Count;
        public void Add(TKey key, TValue value) => Dictionary.Add(key, value);
        public bool Remove(TKey key) => Dictionary.Remove(key);
        public void Clear() => Dictionary.Clear();
        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);
        public bool ContainsValue(TValue value) => Dictionary.ContainsValue(value);
        public TValue Get(TKey key) => Dictionary[key];

        public KeyValuePair<TKey, TValue>[] GenerateSerializableArray()
        {
            KeyValuePair<TKey, TValue>[] pairs = new KeyValuePair<TKey, TValue>[Dictionary.Count];
            Dictionary<TKey, TValue>.Enumerator enumerator = Dictionary.GetEnumerator();

            int index = 0;
            while (enumerator.MoveNext())
            {
                pairs[index] = new KeyValuePair<TKey, TValue>(enumerator.Current);
                index++;
            }
            
            return pairs;
        }
    }
}