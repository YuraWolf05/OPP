using System;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace RetryExample
{
    public class FileProcessor
    {
        private int _saveAttempts = 0;

        public void SaveUserData(string path, string userData)
        {
            _saveAttempts++;

            Console.WriteLine($"[FileProcessor] Спроба {_saveAttempts}");

            if (_saveAttempts <= 4)
            {
                throw new IOException("Тимчасова помилка запису файла.");
            }

            Console.WriteLine($"Данi успiшно збережено у файл: {path}");
        }
    }

    public class NetworkClient
    {
        private int _postAttempts = 0;

        public bool PostUserData(string url, string userData)
        {
            _postAttempts++;

            Console.WriteLine($"[NetworkClient] Спроба {_postAttempts}");

            if (_postAttempts <= 2)
            {
                throw new HttpRequestException("Тимчасова помилка мережi.");
            }

            Console.WriteLine("Данi успiшно вiдправлено на сервер");
            return true;
        }
    }

    public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool>? shouldRetry = null)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(500);

        int attempt = 0;

        while (true)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                attempt++;

                bool retryAllowed = shouldRetry?.Invoke(ex) ?? true;

                Console.WriteLine($"Помилка: {ex.GetType().Name} – {ex.Message}");

                if (!retryAllowed)
                {
                    Console.WriteLine("Повторна спроба заборонена policy shouldRetry.");
                    throw;
                }

                if (attempt > retryCount)
                {
                    Console.WriteLine("Досягнуто максимуму спроб. Операцiю перервано.");
                    throw;
                }

                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));

                Console.WriteLine($"Очiкування {delay.TotalMilliseconds} мс перед повтором #{attempt}...");
                Thread.Sleep(delay);
            }
        }
    }
}

    class Program
    {
        static void Main()
        {
            var fileProcessor = new FileProcessor();
            var networkClient = new NetworkClient();

            Func<Exception, bool> retryPolicy = ex =>
                ex is IOException || ex is HttpRequestException;

            Console.WriteLine("=== Тест FileProcessor ===");

            RetryHelper.ExecuteWithRetry(
                operation: () =>
                {
                    fileProcessor.SaveUserData("user.txt", "USER_DATA");
                    return true;
                },
                retryCount: 6,
                initialDelay: TimeSpan.FromMilliseconds(300),
                shouldRetry: retryPolicy
            );

            Console.WriteLine("\n=== Тест NetworkClient ===");

            RetryHelper.ExecuteWithRetry(
                operation: () =>
                {
                    return networkClient.PostUserData("https://example.com/api/user", "USER_DATA");
                },
                retryCount: 5,
                initialDelay: TimeSpan.FromMilliseconds(300),
                shouldRetry: retryPolicy
            );

            Console.WriteLine("\nУсi операцiї завершено.");
        }
    }
}
