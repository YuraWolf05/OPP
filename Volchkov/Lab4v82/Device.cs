using System;

namespace Lab4
{
    // Абстрактний клас для спільних властивостей усіх приладів
    public abstract class Device : IPowerUsage
    {
        public string Name { get; set; }
        public double Power { get; set; }  // потужність у Ватах
        public double HoursPerDay { get; set; } // години роботи на добу

        public Device(string name, double power, double hoursPerDay)
        {
            Name = name;
            Power = power;
            HoursPerDay = hoursPerDay;
        }

        // Абстрактні методи, які обов’язково мають реалізувати похідні класи
        public abstract double GetDailyConsumption();
        public abstract double GetWeeklyConsumption();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{Name}: {Power}W, {HoursPerDay} год/день");
        }
    }
}
