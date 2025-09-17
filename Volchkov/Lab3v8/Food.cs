using System;

namespace Lab3
{
    public class Food : Product
    {
        public DateTime ExpirationDate { get; set; }

        public Food(string name, double price, DateTime expirationDate) : base(name, price)
        {
            ExpirationDate = expirationDate;
        }

        //перевизначення методу DisplayInfo()
        public override void DisplayInfo()
        {
            Console.WriteLine($"Food: {Name}, Price: {Price} UAH, Expiration: {ExpirationDate.ToShortDateString()}");
        }
    }
}
