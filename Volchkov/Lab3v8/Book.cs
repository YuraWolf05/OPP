using System;

namespace Lab3
{
    public class Book : Product
    {
        public string Author { get; set; }

        //виклик конструктора базового класу через base
        public Book(string name, double price, string author) : base(name, price)
        {
            Author = author;
        }

        //перевизначення методу DisplayInfo()
        public override void DisplayInfo()
        {
            Console.WriteLine($"Book: {Name}, Author: {Author}, Price: {Price} UAH");
        }
    }
}
