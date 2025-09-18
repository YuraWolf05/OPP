using System;

namespace Lab4
{
    public class Lamp : Device
    {
        public Lamp(string name, double power, double hoursPerDay)
            : base(name, power, hoursPerDay) { }

        public override double GetDailyConsumption()
        {
            return (Power * HoursPerDay) / 1000.0;
        }

        public override double GetWeeklyConsumption()
        {
            return GetDailyConsumption() * 7;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Lamp: {Name}, Power: {Power}W, Hours: {HoursPerDay}/day");
        }
    }
}
