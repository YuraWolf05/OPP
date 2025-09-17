using System;

namespace Lab3
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        //конструктор
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        //метод для відображення інформації
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Product: {Name}, Price: {Price} UAH");
        }
    }
}
