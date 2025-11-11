using System;
using System.Collections.Generic;
using System.Linq;

namespace lab6_v8
{
    
    delegate int Operation(int a, int b);

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №6: Лямбда-вирази, анонімні функції та делегати ===\n");

            Operation add = delegate (int x, int y)
            {
                return x + y;
            };
            Console.WriteLine("Анонімний метод: 5 + 3 = " + add(5, 3));

            Operation multiply = (x, y) => x * y;
            Console.WriteLine("Лямбда-вираз: 5 * 3 = " + multiply(5, 3));

            // 3️ Використання стандартного делегата Func<int, int, bool>
            // Перевірка, чи є число простим
            Func<int, int, bool> isPrime = (n, _) =>
            {
                if (n < 2) return false;
                for (int i = 2; i <= Math.Sqrt(n); i++)
                    if (n % i == 0)
                        return false;
                return true;
            };

            // Формування колекції чисел
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            // Використання LINQ і лямбда-виразів для обробки колекції
            var primeNumbers = numbers.Where(n => isPrime(n, 0)).ToList();

            // 4️ Action<List<int>> — метод для друку списку
            Action<List<int>> printList = list =>
            {
                Console.WriteLine(string.Join(", ", list));
            };

            Console.WriteLine("\nСписок усіх чисел:");
            printList(numbers);

            Console.WriteLine("\nПрості числа:");
            printList(primeNumbers);

            // 5️ Predicate<int> — для видалення непотрібних елементів (парних)
            Predicate<int> removeEven = n => n % 2 == 0;
            numbers.RemoveAll(removeEven);

            Console.WriteLine("\nСписок після видалення парних чисел (Predicate<int>):");
            printList(numbers);

            // 6️ LINQ-операції: Select, OrderBy, Aggregate
            var squared = numbers.Select(n => n * n).OrderBy(n => n).ToList();
            Console.WriteLine("\nКвадрати непарних чисел (Select + OrderBy):");
            printList(squared);

            int sum = squared.Aggregate((acc, val) => acc + val);
            Console.WriteLine("\nСума квадратів (Aggregate): " + sum);

            Console.WriteLine("\n=== Кінець виконання програми ===");
        }
    }
}
