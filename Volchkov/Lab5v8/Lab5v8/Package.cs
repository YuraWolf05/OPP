using System;

namespace Lab5v8
{
    // Власний виняток для некоректних даних пакета
    public class InvalidPackageException : Exception
    {
        public InvalidPackageException(string message) : base(message) { }
    }

    public class Package
    {
        public string Name { get; set; }
        private double _weight;
        private double _pricePerKg;

        public double Weight
        {
            get => _weight;
            set
            {
                if (value < 0)
                    throw new InvalidPackageException("Маса пакета не може бути від'ємною.");
                _weight = value;
            }
        }

        public double PricePerKg
        {
            get => _pricePerKg;
            set
            {
                if (value < 0)
                    throw new InvalidPackageException("Ціна за кг не може бути від'ємною.");
                _pricePerKg = value;
            }
        }

        public Package(string name, double weight, double pricePerKg)
        {
            Name = name;
            Weight = weight;
            PricePerKg = pricePerKg;
        }

        public double GetTotalPrice() => Weight * PricePerKg;
    }
}
