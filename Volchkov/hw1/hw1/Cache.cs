using System;
using System.Collections.Generic;

namespace hw1
{
    public class Cache<T> where T : class, new()
    {
        private List<(T Item, DateTime CreatedAt)> _items = new List<(T, DateTime)>();
        private readonly TimeSpan _expirationTime;

        public Cache(TimeSpan expiration)
        {
            _expirationTime = expiration;
        }

        public void Add(T item)
        {
            _items.Add((item, DateTime.Now));
        }

        public void RemoveExpired()
        {
            DateTime now = DateTime.Now;
            _items.RemoveAll(x => now - x.CreatedAt > _expirationTime);
        }

        public IEnumerable<T> GetAll()
        {
            RemoveExpired();
            foreach (var entry in _items)
                yield return entry.Item;
        }

        public void Sort(IComparer<T> comparer)
        {
            for (int i = 1; i < _items.Count; i++)
            {
                var key = _items[i];
                int j = i - 1;

                while (j >= 0 && comparer.Compare(_items[j].Item, key.Item) > 0)
                {
                    _items[j + 1] = _items[j];
                    j--;
                }
                _items[j + 1] = key;
            }
        }
    }
}
