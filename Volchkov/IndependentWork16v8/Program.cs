using System;

#region Поганий клас (порушення SRP)

class ConfigurationManager
{
    public void LoadConfig()
    {
        Console.WriteLine("Завантаження конфiгурацiї...");
    }

    public bool ValidateConfig()
    {
        Console.WriteLine("Валiдацiя конфiгурацiї...");
        return true;
    }

    public void SaveConfig()
    {
        Console.WriteLine("Збереження конфiгурацiї...");
    }

    public void NotifyChanges()
    {
        Console.WriteLine("Сповiщення про змiни...");
    }

    public void ProcessConfiguration()
    {
        LoadConfig();

        if (ValidateConfig())
        {
            SaveConfig();
            NotifyChanges();
        }
    }
}

#endregion

#region Iнтерфейси (SRP + DIP)

interface IConfigLoader
{
    string Load();
}

interface IConfigValidator
{
    bool Validate(string config);
}

interface IConfigSaver
{
    void Save(string config);
}

interface IChangeNotifier
{
    void Notify(string message);
}

#endregion

#region Реалiзацiї (заглушки)

class FileConfigLoader : IConfigLoader
{
    public string Load()
    {
        Console.WriteLine("Завантаження конфiгурацiї з файлу...");
        return "config-data";
    }
}

class SimpleConfigValidator : IConfigValidator
{
    public bool Validate(string config)
    {
        Console.WriteLine("Валiдацiя конфiгурацiї...");
        return !string.IsNullOrEmpty(config);
    }
}

class FileConfigSaver : IConfigSaver
{
    public void Save(string config)
    {
        Console.WriteLine("Збереження конфiгурацiї...");
    }
}

class ConsoleChangeNotifier : IChangeNotifier
{
    public void Notify(string message)
    {
        Console.WriteLine("Сповiщення: " + message);
    }
}

#endregion

#region Головний сервiс (правильний SRP)

class ConfigurationService
{
    private IConfigLoader _loader;
    private IConfigValidator _validator;
    private IConfigSaver _saver;
    private IChangeNotifier _notifier;

    public ConfigurationService(
        IConfigLoader loader,
        IConfigValidator validator,
        IConfigSaver saver,
        IChangeNotifier notifier)
    {
        _loader = loader;
        _validator = validator;
        _saver = saver;
        _notifier = notifier;
    }

    public void ProcessConfiguration()
    {
        string config = _loader.Load();

        if (_validator.Validate(config))
        {
            _saver.Save(config);
            _notifier.Notify("Конфiгурацiю успiшно збережено");
        }
        else
        {
            _notifier.Notify("Помилка конфiгурацiї");
        }
    }
}

#endregion

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Independent Work 16 (SRP) ===\n");

        // Створення залежностей
        IConfigLoader loader = new FileConfigLoader();
        IConfigValidator validator = new SimpleConfigValidator();
        IConfigSaver saver = new FileConfigSaver();
        IChangeNotifier notifier = new ConsoleChangeNotifier();

        // Головний сервіс
        ConfigurationService service =
            new ConfigurationService(loader, validator, saver, notifier);

        // Демонстрація роботи
        service.ProcessConfiguration();

        Console.ReadKey();
    }
}
