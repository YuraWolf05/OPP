using System;
using System.Linq;

namespace Lab5v8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Створюємо пакети
                var package1 = new Package("Яблука", 10, 2.5);
                var package2 = new Package("Груші", 5, 3.0);
                var package3 = new Package("Банани", 7, 2.0);

                // Створюємо доставку
                var delivery1 = new Delivery("Zone A", true);
                delivery1.Packages.AddRange(new[] { package1, package2 });

                var delivery2 = new Delivery("Zone B", false);
                delivery2.Packages.Add(package3);

                // Репозиторій доставок
                var repo = new Repository<Delivery>();
                repo.Add(delivery1);
                repo.Add(delivery2);

                // Вивід даних
                foreach (var d in repo.All())
                {
                    Console.WriteLine($"Доставка в {d.Zone}: Вчасно? {d.IsOnTime}");
                    Console.WriteLine($"  Кiлькiсть пакетiв: {d.Packages.Count}");
                    Console.WriteLine($"  Сумарна маса: {d.TotalWeight()} кг");
                    Console.WriteLine($"  Сумарна вартість: {d.TotalPrice()} грн");
                    Console.WriteLine();
                }

                // Обчислення SLA
                var totalDeliveries = repo.All().Count();
                var onTimeDeliveries = repo.Where(d => d.IsOnTime).Count();
                var slaPercent = (double)onTimeDeliveries / totalDeliveries * 100;
                Console.WriteLine($"SLA: {slaPercent}% доставок вчасно");

                // Групування по зоні
                var grouped = repo.All().GroupBy(d => d.Zone);
                foreach (var group in grouped)
                {
                    Console.WriteLine($"Зона {group.Key}: {group.Count()} доставок");
                }

                // Демонстрація винятку
                var badPackage = new Package("Негативна маса", -1, 10);
            }
            catch (InvalidPackageException ex)
            {
                Console.WriteLine($"Помилка пакета: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Невідома помилка: {ex.Message}");
            }
        }
    }
}
