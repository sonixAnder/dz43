using System;

class Shop
{
    private string name;
    private double area;

    public Shop(string name, double area)
    {
        this.Name = name;
        this.Area = area;
    }

    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название не может быть пустым.");
            name = value;
        }
    }

    public double Area
    {
        get => area;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Площадь должна быть больше нуля.");
            area = value;
        }
    }

    // Перегрузка оператора +
    public static Shop operator +(Shop shop, double increase)
    {
        return new Shop(shop.Name, shop.Area + increase);
    }

    // -
    public static Shop operator -(Shop shop, double decrease)
    {
        if (shop.Area - decrease <= 0)
            throw new InvalidOperationException("Площадь не может быть меньше или равна нулю.");
        return new Shop(shop.Name, shop.Area - decrease);
    }

    // ==
    public static bool operator ==(Shop shop1, Shop shop2)
    {
        return shop1?.Area == shop2?.Area;
    }

    // !=
    public static bool operator !=(Shop shop1, Shop shop2)
    {
        return !(shop1 == shop2);
    }

    // >
    public static bool operator >(Shop shop1, Shop shop2)
    {
        return shop1?.Area > shop2?.Area;
    }

    // <
    public static bool operator <(Shop shop1, Shop shop2)
    {
        return shop1?.Area < shop2?.Area;
    }

    public override bool Equals(object obj)
    {
        if (obj is Shop shop)
            return this.Area == shop.Area;
        return false;
    }

    // Переопределение Equals и GetHashCode
    public override int GetHashCode()
    {
        return Area.GetHashCode();
    }

    public override string ToString()
    {
        return $"Магазин \"{Name}\" с площадью {Area} кв.м.";
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        var shop1 = new Shop("ДНС", 100);
        var shop2 = new Shop("Ситилинк", 150);

        Console.WriteLine(shop1);
        Console.WriteLine(shop2);

        shop1 += 100;
        Console.WriteLine($"После увеличения: {shop1}");
        // Увеличение и уменьшение площади
        shop2 -= 95;
        Console.WriteLine($"После уменьшения: {shop2}");

        // Сравнение площадей
        Console.WriteLine(shop1 == shop2); // False
        Console.WriteLine(shop1 != shop2); // True
        Console.WriteLine(shop1 > shop2);  // True
        Console.WriteLine(shop1 < shop2);  // False
    }
}
