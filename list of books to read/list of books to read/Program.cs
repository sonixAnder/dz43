using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("У книги должно же быть название, не может быть книги ' '.");
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("У книги должен быть автор, кто-то же должен был её написать.");

        Title = title;
        Author = author;
    }

    // Переопределение Equals для сравнения книг
    public override bool Equals(object obj)
    {
        if (obj is Book book)
            return Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase) &&
                   Author.Equals(book.Author, StringComparison.OrdinalIgnoreCase);
        return false;
    }

    public override int GetHashCode()
    {
        return Title.GetHashCode() ^ Author.GetHashCode();
    }

    public override string ToString()
    {
        return $"\"{Title}\" Автор: {Author}";
    }
}

class ReadingList
{
    private List<Book> books = new List<Book>();

    // Добавление книги в список
    public void AddBook(Book book)
    {
        if (!books.Contains(book))
            books.Add(book);
        else
            Console.WriteLine($"Книга {book} уже в списке.");
    }

    // Удаление книги из списка
    public void RemoveBook(Book book)
    {
        if (books.Contains(book))
            books.Remove(book);
        else
            Console.WriteLine($"Книга {book} не найдена в списке.");
    }

    // Проверка наличия книги
    public bool Contains(Book book) => books.Contains(book);

    // Индексатор для доступа к книгам по индексу
    public Book this[int index]
    {
        get
        {
            if (index < 0 || index >= books.Count)
                throw new IndexOutOfRangeException("Неверный индекс.");
            return books[index];
        }
        set
        {
            if (index < 0 || index >= books.Count)
                throw new IndexOutOfRangeException("Неверный индекс.");
            books[index] = value;
        }
    }

    // Перегрузка оператора +
    public static ReadingList operator +(ReadingList list, Book book)
    {
        list.AddBook(book);
        return list;
    }

    // -
    public static ReadingList operator -(ReadingList list, Book book)
    {
        list.RemoveBook(book);
        return list;
    }

    // Переопределение ToString для отображения списка
    public override string ToString()
    {
        if (books.Count == 0)
            return "Список книг пуст.";

        string result = "Список книг для прочтения:\n";
        for (int i = 0; i < books.Count; i++)
        {
            result += $"{i + 1}. {books[i]}\n";
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ReadingList readingList = new ReadingList();

        var book1 = new Book("451 градус по Фаренгейту", "Рэй Брэдбери");
        var book2 = new Book("S.T.A.L.K.E.R.", "Николай Грошев и т.д");
        var book3 = new Book("Метро 2033", "Дмитрий Глуховский");

        // Добавление книг
        readingList += book1;
        readingList += book2;
        readingList.AddBook(book3);

        Console.WriteLine(readingList);

        // Проверка наличия книги
        Console.WriteLine($"Книга \"451 градус по Фаренгейту\" в списке: {readingList.Contains(book1)}. Удалена последняя книга.");

        // Удаление книги
        readingList -= book2;
        Console.WriteLine(readingList);

        // Доступ к книге по индексу
        Console.WriteLine($"Книга под номером 1: {readingList[0]} изменена на другую.");

        // Изменение книги по индексу
        readingList[0] = new Book("Преступление и наказание", "Федор Достоевский");
        Console.WriteLine(readingList);
    }
}
