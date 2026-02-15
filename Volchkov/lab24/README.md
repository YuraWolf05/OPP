# lab24 — Strategy + Observer

## Мета
Реалізувати систему обробки числових даних з використанням:
- Strategy (вибір операції над числом під час виконання)
- Observer через події C# (оповіщення підписників про результат)

---

## 1) Strategy

### Інтерфейс
`INumericOperationStrategy` визначає контракт операції:
- `double Execute(double value)` — виконати обчислення
- `string Name` — назва операції

### Реалізації
- `SquareOperationStrategy` — квадрат числа
- `CubeOperationStrategy` — куб числа
- `SquareRootOperationStrategy` — квадратний корінь (з перевіркою на від’ємні значення)

### Контекст
`NumericProcessor`:
- приймає стратегію через конструктор
- дозволяє змінити стратегію через `SetStrategy`
- виконує обчислення через `Process`, делегуючи роботу стратегії

---

## 2) Observer (через події C#)

### Subject
`ResultPublisher` має подію:
`event Action<double, string> ResultCalculated;`
та метод:
`PublishResult(result, operationName)` який викликає подію.

### Observers
- `ConsoleLoggerObserver` — виводить результат у консоль
- `HistoryLoggerObserver` — записує історію в `List<string>`
- `ThresholdNotifierObserver` — повідомляє, якщо результат > порога

---

## 3) Демонстрація в Main
- Створюються `NumericProcessor` і `ResultPublisher`
- Створюються спостерігачі та підписуються на `ResultCalculated`
- Послідовно встановлюються різні стратегії (Square, Cube, SquareRoot)
- Після кожної обробки викликається `PublishResult`
- Наприкінці друкується історія з `HistoryLoggerObserver`

---

## Висновок
Strategy дозволяє легко змінювати алгоритм обробки без зміни коду контексту.
Observer дозволяє розсилати результати багатьом підписникам без жорстких залежностей.
