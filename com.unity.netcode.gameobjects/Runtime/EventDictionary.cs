using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Netcode
{
    public class EventDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public event EventHandler<KeyValuePair<TKey, TValue>> onItemAdded;
        public event EventHandler<TKey> onItemRemoved;

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            onItemAdded?.Invoke(this, new KeyValuePair<TKey, TValue>(key, value));
        }

        public new bool Remove(TKey key)
        {
            bool isRemoved = base.Remove(key);
            if (isRemoved)
                onItemRemoved?.Invoke(this, key);

            return isRemoved;
        }

        public new void Clear()
        {
            foreach (var keys in base.Keys)
            {
                onItemRemoved?.Invoke(this, keys);
            }
            base.Clear();
        }

        public void Dispose()
        {
            Clear();
            onItemAdded = null;
            onItemRemoved = null;
        }
    }

}
