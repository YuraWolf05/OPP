using System;

namespace lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ПОРУШЕННЯ ISP та DIP ===");
            BadPayrollSystem bad = new BadPayrollSystem();
            bad.ProcessPayroll("Yurii", 160, 10);

            Console.WriteLine("\n=== Дотримання ISP та DIP ===");

            ISalaryCalculator calculator = new BasicSalaryCalculator();
            IReportExporter exporter = new PdfReportExporter();
            IDataStorage storage = new SqlDatabaseStorage();

            GoodPayrollSystem good = new GoodPayrollSystem(calculator, exporter, storage);
            good.ProcessPayroll("Yurii", 160, 10);
        }
    }

    // ПОЧАТКОВА СТРУКТУРА (ПОРУШЕННЯ ISP + DIP)
    class SalaryCalculator
    {
        public double Calculate(string name, int hours, double rate)
        {
            return hours * rate;
        }
    }

    class PdfExporter
    {
        public void Export(string content)
        {
            Console.WriteLine($"PDF Report: {content}");
        }
    }

    class SqlDatabase
    {
        public void Save(string data)
        {
            Console.WriteLine($"Saved to SQL DB: {data}");
        }
    }

    class BadPayrollSystem
    {
        private SalaryCalculator calculator = new SalaryCalculator();
        private PdfExporter exporter = new PdfExporter();
        private SqlDatabase database = new SqlDatabase();

        public void ProcessPayroll(string name, int hours, double rate)
        {
            double salary = calculator.Calculate(name, hours, rate);

            string report = $"Employee: {name}, Salary: {salary}";

            exporter.Export(report);
            database.Save(report);
        }
    }

    //  РЕФАКТОРИНГ (ISP + DIP)
    interface ISalaryCalculator
    {
        double Calculate(string name, int hours, double rate);
    }

    interface IReportExporter
    {
        void Export(string content);
    }

    interface IDataStorage
    {
        void Save(string data);
    }

    class BasicSalaryCalculator : ISalaryCalculator
    {
        public double Calculate(string name, int hours, double rate)
        {
            return hours * rate;
        }
    }

    class PdfReportExporter : IReportExporter
    {
        public void Export(string content)
        {
            Console.WriteLine($"PDF Report: {content}");
        }
    }

    class SqlDatabaseStorage : IDataStorage
    {
        public void Save(string data)
        {
            Console.WriteLine($"Saved to SQL DB: {data}");
        }
    }

    class GoodPayrollSystem
    {
        private readonly ISalaryCalculator calculator;
        private readonly IReportExporter exporter;
        private readonly IDataStorage storage;

        public GoodPayrollSystem(
            ISalaryCalculator calculator,
            IReportExporter exporter,
            IDataStorage storage)
        {
            this.calculator = calculator;
            this.exporter = exporter;
            this.storage = storage;
        }

        public void ProcessPayroll(string name, int hours, double rate)
        {
            double salary = calculator.Calculate(name, hours, rate);

            string report = $"Employee: {name}, Salary: {salary}";

            exporter.Export(report);
            storage.Save(report);
        }
    }
}
