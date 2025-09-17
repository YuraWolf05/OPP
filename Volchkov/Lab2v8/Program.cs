using System;

namespace Lab2
{
    class Fraction
    {
        // Приватні поля
        private int numerator;
        private int denominator;

        // Властивості
        public int Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }

        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Denominator cannot be zero!");
                denominator = value;
            }
        }

        // Конструктор
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        // Індексатор (0 - чисельник, 1 - знаменник)
        public int this[int index]
        {
            get
            {
                if (index == 0) return Numerator;
                else if (index == 1) return Denominator;
                else throw new IndexOutOfRangeException("Index must be 0 or 1");
            }
            set
            {
                if (index == 0) Numerator = value;
                else if (index == 1)
                {
                    if (value == 0)
                        throw new ArgumentException("Denominator cannot be zero!");
                    Denominator = value;
                }
                else throw new IndexOutOfRangeException("Index must be 0 or 1");
            }
        }

        // Перевантаження операторів
        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
                throw new DivideByZeroException("Cannot divide by zero fraction!");
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator == b.Numerator * a.Denominator;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        // Перевизначення Equals і GetHashCode 
        public override bool Equals(object obj)
        {
            if (obj is Fraction f)
                return this == f;
            return false;
        }

        public override int GetHashCode()
        {
            return (Numerator / (double)Denominator).GetHashCode();
        }

       
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Fraction f1 = new Fraction(1, 2);
            Fraction f2 = new Fraction(3, 4);

            Console.WriteLine($"f1 = {f1}");
            Console.WriteLine($"f2 = {f2}");

            Console.WriteLine($"f1 + f2 = {f1 + f2}");
            Console.WriteLine($"f1 - f2 = {f1 - f2}");
            Console.WriteLine($"f1 * f2 = {f1 * f2}");
            Console.WriteLine($"f1 / f2 = {f1 / f2}");

            Console.WriteLine($"f1 == f2? {f1 == f2}");
            Console.WriteLine($"f1 != f2? {f1 != f2}");

            // Демонстрація індексатора
            Console.WriteLine($"f1[0] = {f1[0]} (numerator)");
            Console.WriteLine($"f1[1] = {f1[1]} (denominator)");

            f1[0] = 5; // змінюємо чисельник
            f1[1] = 7; // змінюємо знаменник
            Console.WriteLine($"f1 після змін через індексатор: {f1}");
        }
    }
}
