using System;

// ===== Strategy Interface =====
public interface IShippingStrategy
{
    decimal CalculateCost(decimal distance, decimal weight);
}

// ===== Concrete Strategies =====
public class StandardShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal weight)
    {
        return distance * 1.5m + weight * 0.5m;
    }
}

public class ExpressShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal weight)
    {
        return (distance * 2.5m + weight * 1.0m) + 50m;
    }
}

public class InternationalShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal weight)
    {
        decimal baseCost = distance * 5.0m + weight * 2.0m;
        return baseCost + baseCost * 0.15m; // 15% податок
    }
}

// ===== OCP Extension: Night Shipping =====
public class NightShippingStrategy : IShippingStrategy
{
    private readonly IShippingStrategy _baseStrategy =
        new StandardShippingStrategy();

    public decimal CalculateCost(decimal distance, decimal weight)
    {
        return _baseStrategy.CalculateCost(distance, weight) + 30m;
    }
}

// ===== Factory Method =====
public static class ShippingStrategyFactory
{
    public static IShippingStrategy CreateStrategy(string deliveryType)
    {
        return deliveryType.ToLower() switch
        {
            "standard" => new StandardShippingStrategy(),
            "express" => new ExpressShippingStrategy(),
            "international" => new InternationalShippingStrategy(),
            "night" => new NightShippingStrategy(),
            _ => throw new ArgumentException("Невiдомий тип доставки")
        };
    }
}

// ===== Service (depends only on interface) =====
public class DeliveryService
{
    public decimal CalculateDeliveryCost(
        decimal distance,
        decimal weight,
        IShippingStrategy strategy)
    {
        return strategy.CalculateCost(distance, weight);
    }
}

// ===== Program =====
class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Тип доставки (Standard, Express, International, Night):");
        string type = Console.ReadLine()!;

        Console.WriteLine("Вiдстань (км):");
        decimal distance = decimal.Parse(Console.ReadLine()!);

        Console.WriteLine("Вага (кг):");
        decimal weight = decimal.Parse(Console.ReadLine()!);

        try
        {
            IShippingStrategy strategy =
                ShippingStrategyFactory.CreateStrategy(type);

            DeliveryService service = new DeliveryService();
            decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);

            Console.WriteLine($"Вартiсть доставки: {cost} грн");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}