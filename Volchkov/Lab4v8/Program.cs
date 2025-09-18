using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
             Random rnd = new Random();

            List<Device> devices = new List<Device>
            {
                
                new Laptop("Dell XPS", 65, rnd.Next(1, 25)), // 65W, 5 год/день
                new Lamp("Philips LED", 10, rnd.Next(1, 25)) // 10W, 6 год/день
            };

            Console.WriteLine("=== Iнформацiя про прилади ===");
            foreach (var d in devices)
            {
                d.ShowInfo();
                Console.WriteLine($"Добове споживання: {d.GetDailyConsumption()} кВт·год");
                Console.WriteLine($"Тижневе споживання: {d.GetWeeklyConsumption()} кВт·год\n");
            }

            // Загальне споживання за день і тиждень
            double totalDay = devices.Sum(d => d.GetDailyConsumption());
            double totalWeek = devices.Sum(d => d.GetWeeklyConsumption());

            Console.WriteLine($"Загальне споживання за день: {totalDay:F2} кВт·год");
            Console.WriteLine($"Загальне споживання за тиждень: {totalWeek:F2} кВт·год");
        }
    }
}
