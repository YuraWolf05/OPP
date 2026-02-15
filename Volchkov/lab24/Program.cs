using System;
using System.Collections.Generic;

namespace lab24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Strategy
            INumericOperationStrategy strategy = new SquareOperationStrategy();
            NumericProcessor processor = new NumericProcessor(strategy);

            // Observer (events)
            ResultPublisher publisher = new ResultPublisher();

            var consoleObserver = new ConsoleLoggerObserver();
            var historyObserver = new HistoryLoggerObserver();
            var thresholdObserver = new ThresholdNotifierObserver(threshold: 50);

            publisher.ResultCalculated += consoleObserver.OnResultCalculated;
            publisher.ResultCalculated += historyObserver.OnResultCalculated;
            publisher.ResultCalculated += thresholdObserver.OnResultCalculated;

            // Demo inputs
            double[] inputs = { 4, 9, 16, 64, 100 };

            // 1) Square
            processor.SetStrategy(new SquareOperationStrategy());
            RunBatch("Square", processor, publisher, inputs);

            // 2) Cube
            processor.SetStrategy(new CubeOperationStrategy());
            RunBatch("Cube", processor, publisher, new double[] { 2, 3, 4, 5 });

            // 3) SquareRoot
            processor.SetStrategy(new SquareRootOperationStrategy());
            RunBatch("SquareRoot", processor, publisher, new double[] { 9, 16, 25, 81, 144 });

            Console.WriteLine("\n=== HISTORY (HistoryLoggerObserver) ===");
            foreach (var line in historyObserver.History)
            {
                Console.WriteLine(line);
            }
        }

        private static void RunBatch(
            string operationName,
            NumericProcessor processor,
            ResultPublisher publisher,
            IEnumerable<double> inputs)
        {
            Console.WriteLine($"\n--- Operation: {operationName} ---");

            foreach (var input in inputs)
            {
                double result = processor.Process(input);
                publisher.PublishResult(result, operationName);
            }
        }
    }

    // =========================
    // Strategy
    // =========================

    public interface INumericOperationStrategy
    {
        double Execute(double value);
        string Name { get; }
    }

    public class SquareOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Square";
        public double Execute(double value) => value * value;
    }

    public class CubeOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Cube";
        public double Execute(double value) => value * value * value;
    }

    public class SquareRootOperationStrategy : INumericOperationStrategy
    {
        public string Name => "SquareRoot";

        public double Execute(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Square root is not defined for negative numbers in real domain.");

            return Math.Sqrt(value);
        }
    }

    public class NumericProcessor
    {
        private INumericOperationStrategy strategy;

        public NumericProcessor(INumericOperationStrategy strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void SetStrategy(INumericOperationStrategy strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public double Process(double input)
        {
            return strategy.Execute(input);
        }
    }

    // =========================
    // Observer (Events)
    // =========================

    public class ResultPublisher
    {
        public event Action<double, string> ResultCalculated;

        public void PublishResult(double result, string operationName)
        {
            ResultCalculated?.Invoke(result, operationName);
        }
    }

    public class ConsoleLoggerObserver
    {
        public void OnResultCalculated(double result, string operationName)
        {
            Console.WriteLine($"[{operationName}] Result = {result}");
        }
    }

    public class HistoryLoggerObserver
    {
        private readonly List<string> history = new List<string>();
        public IReadOnlyList<string> History => history;

        public void OnResultCalculated(double result, string operationName)
        {
            history.Add($"[{DateTime.Now:HH:mm:ss}] {operationName}: {result}");
        }
    }

    public class ThresholdNotifierObserver
    {
        private readonly double threshold;

        public ThresholdNotifierObserver(double threshold)
        {
            this.threshold = threshold;
        }

        public void OnResultCalculated(double result, string operationName)
        {
            if (result > threshold)
            {
                Console.WriteLine($"!!! Threshold exceeded: {operationName} result {result} > {threshold}");
            }
        }
    }
}
