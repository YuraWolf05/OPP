using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //список продуктів (поліморфізм)
            List<Product> basket = new List<Product>
            {
                new Book("C# Basics", 250, "Іваненко"),
                new Food("Яблуко", 30, DateTime.Now.AddDays(5)),
                new Book("Design Patterns", 400, "Гамма"),
                new Food("Хліб", 20, DateTime.Now.AddDays(2))
            };

            Console.WriteLine("=== Вміст кошика ===");
            foreach (var item in basket)
            {
                item.DisplayInfo();
            }

            //сумарної вартості кошика
            double total = basket.Sum(p => p.Price);
            Console.WriteLine($"\nЗагальна вартість кошика: {total} UAH");

            // середня ціна по категоріях
            double avgBooks = basket.OfType<Book>().Average(b => b.Price);
            double avgFoods = basket.OfType<Food>().Average(f => f.Price);

            Console.WriteLine($"Середня ціна книжок: {avgBooks} UAH");
            Console.WriteLine($"Середня ціна їжі: {avgFoods} UAH");
        }

    }
    
}
