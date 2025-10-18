using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5v8
{
    public class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item) => items.Add(item);
        public void Remove(T item) => items.Remove(item);
        public IEnumerable<T> All() => items;
        public IEnumerable<T> Where(Func<T, bool> predicate) => items.Where(predicate);
        public T Find(Func<T, bool> predicate) => items.FirstOrDefault(predicate);
    }
}
