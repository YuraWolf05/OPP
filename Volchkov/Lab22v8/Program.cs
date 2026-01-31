using System;
using System.Collections.Generic;

namespace lab22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Порушення LSP ===");

            CustomList list = new ReadOnlyList();
            TryAdd(list);

            Console.WriteLine("\n=== Дотримання LSP ===");

            IWritableList writableList = new WritableList();
            IReadOnlyList readOnlyList = new SafeReadOnlyList();

            AddItemSafely(writableList);
            ShowCount(readOnlyList);
        }

        static void TryAdd(CustomList list)
        {
            list.Add(5);
        }

        static void AddItemSafely(IWritableList list)
        {
            list.Add(10);
            Console.WriteLine("Елемент успішно додано");
        }

        static void ShowCount(IReadOnlyList list)
        {
            Console.WriteLine($"Кількість елементів: {list.Count}");
        }
    }

    // ===== ПОРУШЕННЯ LSP =====

    class CustomList
    {
        protected List<int> items = new List<int>();

        public virtual void Add(int item)
        {
            items.Add(item);
        }
    }

    class ReadOnlyList : CustomList
    {
        public override void Add(int item)
        {
            throw new InvalidOperationException("ReadOnlyList не пiдтримує додавання");
        }
    }

    // ===== КОРЕКТНЕ РІШЕННЯ (LSP) =====

    interface IReadOnlyList
    {
        int Count { get; }
    }

    interface IWritableList : IReadOnlyList
    {
        void Add(int item);
    }

    class WritableList : IWritableList
    {
        private List<int> items = new List<int>();

        public int Count => items.Count;

        public void Add(int item)
        {
            items.Add(item);
        }
    }

    class SafeReadOnlyList : IReadOnlyList
    {
        private List<int> items = new List<int> { 1, 2, 3 };

        public int Count => items.Count;
    }
}
