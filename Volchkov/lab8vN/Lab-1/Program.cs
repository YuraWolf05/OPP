
using System;

namespace Lab1
{
    class Animal
    {
        // поля
        private string species;
        private string nickname;

        // властивість
        public int Age { get; set; }

        // конструктор
        public Animal(string species, string nickname, int age)
        {
            this.species = species;
            this.nickname = nickname;
            Age = age;
            Console.WriteLine($"Створено тварину: {nickname} ({species}), вік: {Age}");
        }

        // деструктор
        ~Animal()
        {
            Console.WriteLine($"Об'єкт {nickname} ({species}) знищено.");
        }

        // метод
        public void Speak()
        {
            Console.WriteLine($"{nickname} the {species} каже: Hello!");
        }

        // виводу інформації
        public void Info()
        {
            Console.WriteLine($"Вид: {species}, Кличка: {nickname}, Вік: {Age}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // об'єкти
            Animal dog = new Animal("Dog", "Rex", 5);
            Animal cat = new Animal("Cat", "Murka", 3);
            Animal parrot = new Animal("Parrot", "Kesha", 2);

            // виклик 
            dog.Info();
            dog.Speak();

            cat.Info();
            cat.Speak();

            parrot.Info();
            parrot.Speak();

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadLine();
        }
    }
}
