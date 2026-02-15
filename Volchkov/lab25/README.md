# lab25 — інтеграція Factory Method + Singleton + Strategy + Observer

## Мета
Реалізувати інтегровану систему:
- Factory Method: створення логера через фабрики
- Singleton: глобальний менеджер логування
- Strategy: вибір стратегії обробки даних під час виконання
- Observer: сповіщення через події C# про результат обробки


## Компоненти

### 1) Factory Method (логери)
- `ILogger` — інтерфейс логера
- `ConsoleLogger`, `FileLogger` — реалізації
- `LoggerFactory` — абстрактна фабрика
- `ConsoleLoggerFactory`, `FileLoggerFactory` — конкретні фабрики

**Навіщо:** клієнт працює з абстракцією фабрики, а не створює логер напряму.w


### 2) Singleton
- `LoggerManager` — singleton, що зберігає активний логер.
- `SetFactory(factory)` — встановлює фабрику та створює логер.
- `GetLogger()` — повертає поточний логер.

**Навіщо:** одна точка доступу до логування в системі.


### 3) Strategy (обробка даних)
- `IDataProcessorStrategy` — контракт обробки
- `EncryptDataStrategy` — “шифрування” (Base64)
- `CompressDataStrategy` — “стискання” (RLE-подібне)

- `DataContext` — контекст, що:
  - приймає стратегію в конструкторі
  - дозволяє змінювати її через `SetStrategy`
  - виконує обробку через `Process`


### 4) Observer (події)
- `DataPublisher` має подію `DataProcessed`
- `ProcessingLoggerObserver` підписується на `DataProcessed`
  і логує подію через `LoggerManager` (Singleton)

## Демонстраційні сценарії (Main)

### Сценарій 1: Повна інтеграція
- LoggerManager = ConsoleLoggerFactory
- DataContext = EncryptDataStrategy
- Observer підписаний на DataPublisher
- Результат друкується в консоль

### Сценарій 2: Динамічна зміна логера
- Перша обробка з ConsoleLogger
- Потім LoggerManager перемикається на FileLoggerFactory
- Друга обробка логується у файл `lab25-log.txt`

### Сценарій 3: Динамічна зміна стратегії
- Перша обробка: Encrypt
- Потім DataContext перемикається на Compress
- Друга обробка відбувається за новою стратегією


## Висновки
Інтеграція чотирьох патернів показує:
- Factory Method усуває пряме створення логерів
- Singleton дає централізоване керування логером
- Strategy дозволяє змінювати алгоритм обробки без зміни контексту
- Observer роз’єднує публікатора подій і спостерігачів, забезпечуючи розширюваність
