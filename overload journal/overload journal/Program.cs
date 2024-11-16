using System;

class Journal
{
    private string title;
    private int employees;

    public Journal(string title, int employees)
    {
        this.Title = title;
        this.Employees = employees;
    }

    public string Title
    {
        get => title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название не может быть пустым.");
            title = value;
        }
    }

    public int Employees
    {
        get => employees;
        set
        {
            if (value < 0)
                throw new ArgumentException("Количество сотрудников не может быть отрицательным.");
            employees = value;
        }
    }

    // Перегрузка оператора +
    public static Journal operator +(Journal journal, int increase)
    {
        if (increase < 0)
            throw new ArgumentException("Увеличение должно быть положительным.");
        return new Journal(journal.Title, journal.Employees + increase);
    }

    // -
    public static Journal operator -(Journal journal, int decrease)
    {
        if (decrease < 0)
            throw new ArgumentException("Уменьшение должно быть положительным.");
        if (journal.Employees - decrease < 0)
            throw new InvalidOperationException("Количество сотрудников не может быть меньше нуля.");
        return new Journal(journal.Title, journal.Employees - decrease);
    }

    // ==
    public static bool operator ==(Journal journal1, Journal journal2)
    {
        return journal1?.Employees == journal2?.Employees;
    }

    // !=
    public static bool operator !=(Journal journal1, Journal journal2)
    {
        return !(journal1 == journal2);
    }

    // >
    public static bool operator >(Journal journal1, Journal journal2)
    {
        return journal1?.Employees > journal2?.Employees;
    }

    // <
    public static bool operator <(Journal journal1, Journal journal2)
    {
        return journal1?.Employees < journal2?.Employees;
    }

    public override bool Equals(object obj)
    {
        if (obj is Journal journal)
            return this.Employees == journal.Employees;
        return false;
    }

    // Переопределение Equals и GetHashCode
    public override int GetHashCode()
    {
        return Employees.GetHashCode();
    }

    public override string ToString()
    {
        return $"Журнал \"{Title}\" с количеством сотрудников: {Employees}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var journal1 = new Journal("Наука и жизнь", 10);
        var journal2 = new Journal("Техника молодежи", 15);

        Console.WriteLine(journal1);
        Console.WriteLine(journal2);

        journal1 += 4;
        Console.WriteLine($"После увеличения: {journal1}");
        // Увеличение и уменьшение количества сотрудников
        journal2 -= 9;
        Console.WriteLine($"После уменьшения: {journal2}");

        // Сравнение количества сотрудников
        Console.WriteLine(journal1 == journal2); // False
        Console.WriteLine(journal1 != journal2); // True
        Console.WriteLine(journal1 > journal2);  // True
        Console.WriteLine(journal1 < journal2);  // False
    }
}
