using System;
using System.Threading;

namespace hw1
{
    class Program
    {
        static void Main()
        {
            var cache = new Cache<Item>(TimeSpan.FromSeconds(3));

            cache.Add(new Item("Task A", 3));
            cache.Add(new Item("Task B", 1));
            cache.Add(new Item("Task C", 2));

            Console.WriteLine("Початковий кеш:");
            foreach (var item in cache.GetAll())
                Console.WriteLine(item);

            // Демонстрація видалення застарілих елементів
            Console.WriteLine("\nОчiкуємо 4 секунди...");
            Thread.Sleep(4000);
            cache.RemoveExpired();

            Console.WriteLine("Кеш пiсля очищення:");
            foreach (var item in cache.GetAll())
                Console.WriteLine(item);

           
            cache.Add(new Item("Task D", 5));
            cache.Add(new Item("Task E", 2));

            Console.WriteLine("\nКеш перед сортуванням:");
            foreach (var item in cache.GetAll())
                Console.WriteLine(item);

           
            cache.Sort(new ItemPriorityComparer());

            Console.WriteLine("\nКеш пiсля сортування:");
            foreach (var item in cache.GetAll())
                Console.WriteLine(item);

            Console.WriteLine("\n--- Кiнець демонстрації ---");
        }
    }
}
