# Домашня робота №3

## Принципи ISP та DIP (SOLID)

### 1. Принцип ISP (Interface Segregation Principle)

**ISP** стверджує: *клієнти не повинні залежати від інтерфейсів, які вони не використовують*. Іншими словами, краще мати декілька «вузьких» інтерфейсів, ніж один «товстий».

#### Приклад інтерфейсу, що порушує ISP

```csharp
public interface IMultifunctionDevice
{
    void Print();
    void Scan();
    void Fax();
}

public class OldPrinter : IMultifunctionDevice
{
    public void Print() { /* ok */ }
    public void Scan() { throw new NotImplementedException(); }
    public void Fax() { throw new NotImplementedException(); }
}
```

**Проблема:** `OldPrinter` змушений реалізовувати методи, які йому не потрібні.

#### Вирішення (дотримання ISP)

```csharp
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public class OldPrinter : IPrinter
{
    public void Print() { /* ok */ }
}
```

Тепер кожен клас реалізує лише потрібний функціонал.


### 2. Принцип DIP (Dependency Inversion Principle)

**DIP** говорить: *модулі верхнього рівня не повинні залежати від модулів нижнього рівня — обидва повинні залежати від абстракцій*.

#### Без DIP

```csharp
public class OrderService
{
    private SqlDatabase db = new SqlDatabase();
}
```

Клас жорстко прив’язаний до конкретної реалізації.

#### З DIP та Dependency Injection

```csharp
public interface IDatabase
{
    void Save();
}

public class SqlDatabase : IDatabase
{
    public void Save() { }
}

public class OrderService
{
    private readonly IDatabase _database;

    public OrderService(IDatabase database)
    {
        _database = database;
    }
}
```

#### Переваги DIP:

* слабка зв’язаність компонентів
* легка заміна реалізацій (SQL → InMemory → Mock)
* краща масштабованість
* простіше тестування


### 3. Як ISP покращує DI та тестування

«Вузькі» інтерфейси:

* спрощують **Dependency Injection**, бо клас отримує лише те, що реально використовує
* зменшують кількість залежностей у конструкторах
* полегшують написання **mock-об’єктів** для unit-тестів


#### Приклад у тестуванні

```csharp
public class FakePrinter : IPrinter
{
    public bool WasCalled = false;
    public void Print() => WasCalled = true;
}
```
Тест не залежить від зайвих методів (`Scan`, `Fax`).

### 4. Висновок

* **ISP** робить інтерфейси простими та зрозумілими
* **DIP** зменшує зв’язаність між компонентами
* Разом вони забезпечують гнучку архітектуру, зручну для DI та автоматизованого тестування



