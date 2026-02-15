using System;
using System.IO;
using System.Text;

namespace lab25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------
            // Scenario 1: Full integration
            // -------------------------------
            PrintScenarioHeader(1, "Full integration (ConsoleLogger + Encrypt)");

            LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());
            LoggerManager.Instance.GetLogger().Log("LoggerManager initialized with ConsoleLoggerFactory");

            var dataContext = new DataContext(new EncryptDataStrategy());
            var publisher = new DataPublisher();
            var observer = new ProcessingLoggerObserver(publisher);

            string input1 = "Hello, SOLID & Patterns!";
            string processed1 = dataContext.Process(input1);
            publisher.PublishDataProcessed(processed1, dataContext.CurrentStrategyName);

            // cleanup subscription (optional good practice)
            observer.Unsubscribe();

            // -------------------------------
            // Scenario 2: Dynamic logger change
            // -------------------------------
            PrintScenarioHeader(2, "Dynamic logger change (ConsoleLogger -> FileLogger)");

            LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());
            LoggerManager.Instance.GetLogger().Log("LoggerManager initialized with ConsoleLoggerFactory");

            dataContext = new DataContext(new EncryptDataStrategy());
            publisher = new DataPublisher();
            observer = new ProcessingLoggerObserver(publisher);

            string input2a = "First message (console logger)";
            string processed2a = dataContext.Process(input2a);
            publisher.PublishDataProcessed(processed2a, dataContext.CurrentStrategyName);

            // Change logger factory dynamically
            LoggerManager.Instance.SetFactory(new FileLoggerFactory("lab25-log.txt"));
            LoggerManager.Instance.GetLogger().Log("LoggerManager switched to FileLoggerFactory");

            string input2b = "Second message (file logger)";
            string processed2b = dataContext.Process(input2b);
            publisher.PublishDataProcessed(processed2b, dataContext.CurrentStrategyName);

            observer.Unsubscribe();

            // -------------------------------
            // Scenario 3: Dynamic strategy change
            // -------------------------------
            PrintScenarioHeader(3, "Dynamic strategy change (Encrypt -> Compress)");

            LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());
            LoggerManager.Instance.GetLogger().Log("LoggerManager initialized with ConsoleLoggerFactory");

            dataContext = new DataContext(new EncryptDataStrategy());
            publisher = new DataPublisher();
            observer = new ProcessingLoggerObserver(publisher);

            string input3a = "aaaaabbbbcccddeeeeeeeeeeeee";
            string processed3a = dataContext.Process(input3a);
            publisher.PublishDataProcessed(processed3a, dataContext.CurrentStrategyName);

            // Change strategy dynamically
            dataContext.SetStrategy(new CompressDataStrategy());
            LoggerManager.Instance.GetLogger().Log("DataContext strategy switched to CompressDataStrategy");

            string input3b = "aaaaabbbbcccddeeeeeeeeeeeee";
            string processed3b = dataContext.Process(input3b);
            publisher.PublishDataProcessed(processed3b, dataContext.CurrentStrategyName);

            observer.Unsubscribe();

            Console.WriteLine("\nDone. If you used FileLoggerFactory, check lab25-log.txt near the executable.");
        }

        private static void PrintScenarioHeader(int number, string title)
        {
            Console.WriteLine("\n==============================================");
            Console.WriteLine($"SCENARIO {number}: {title}");
            Console.WriteLine("==============================================\n");
        }
    }

    // ============================================================
    // LOGGER (Factory Method + Implementations)
    // ============================================================

    public interface ILogger
    {
        void Log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[ConsoleLogger] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }

    public class FileLogger : ILogger
    {
        private readonly string filePath;

        public FileLogger(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));

            this.filePath = filePath;
        }

        public void Log(string message)
        {
            string line = $"[FileLogger] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(filePath, line + Environment.NewLine);
        }
    }

    // Factory Method
    public abstract class LoggerFactory
    {
        public abstract ILogger CreateLogger();
    }

    public class ConsoleLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger() => new ConsoleLogger();
    }

    public class FileLoggerFactory : LoggerFactory
    {
        private readonly string filePath;

        public FileLoggerFactory(string filePath)
        {
            this.filePath = filePath;
        }

        public override ILogger CreateLogger() => new FileLogger(filePath);
    }

    // ============================================================
    // LOGGER MANAGER (Singleton)
    // ============================================================

    public sealed class LoggerManager
    {
        private static readonly Lazy<LoggerManager> lazy =
            new Lazy<LoggerManager>(() => new LoggerManager());

        public static LoggerManager Instance => lazy.Value;

        private LoggerFactory factory;
        private ILogger logger;

        private LoggerManager() { }

        public void SetFactory(LoggerFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.logger = this.factory.CreateLogger(); // recreate logger when factory changes
        }

        public ILogger GetLogger()
        {
            if (logger == null)
                throw new InvalidOperationException("LoggerManager is not initialized. Call SetFactory(...) first.");

            return logger;
        }
    }

    // ============================================================
    // DATA PROCESSING (Strategy)
    // ============================================================

    public interface IDataProcessorStrategy
    {
        string Name { get; }
        string Process(string data);
    }

    public class EncryptDataStrategy : IDataProcessorStrategy
    {
        public string Name => "Encrypt";

        // Simple demo "encryption": Base64 encoding
        public string Process(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
    }

    public class CompressDataStrategy : IDataProcessorStrategy
    {
        public string Name => "Compress";

        // Simple demo "compression": RLE-like encoding (aaaaabb -> a5b2)
        public string Process(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) return string.Empty;

            var sb = new StringBuilder();
            char current = data[0];
            int count = 1;

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == current)
                {
                    count++;
                }
                else
                {
                    sb.Append(current);
                    sb.Append(count);
                    current = data[i];
                    count = 1;
                }
            }

            sb.Append(current);
            sb.Append(count);

            return sb.ToString();
        }
    }

    public class DataContext
    {
        private IDataProcessorStrategy strategy;

        public DataContext(IDataProcessorStrategy strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public string CurrentStrategyName => strategy.Name;

        public void SetStrategy(IDataProcessorStrategy strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public string Process(string data)
        {
            return strategy.Process(data);
        }
    }

    // ============================================================
    // PUBLISHER (Observer via C# events)
    // ============================================================

    public class DataPublisher
    {
        // processedData, strategyName
        public event Action<string, string> DataProcessed;

        public void PublishDataProcessed(string processedData, string strategyName)
        {
            DataProcessed?.Invoke(processedData, strategyName);
        }
    }

    public class ProcessingLoggerObserver
    {
        private readonly DataPublisher publisher;

        public ProcessingLoggerObserver(DataPublisher publisher)
        {
            this.publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            this.publisher.DataProcessed += OnDataProcessed;
        }

        public void Unsubscribe()
        {
            publisher.DataProcessed -= OnDataProcessed;
        }

        private void OnDataProcessed(string processedData, string strategyName)
        {
            LoggerManager.Instance.GetLogger()
                .Log($"Observer received DataProcessed. Strategy={strategyName}, Data={processedData}");
        }
    }
}
