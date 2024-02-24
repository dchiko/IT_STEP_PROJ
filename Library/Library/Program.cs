using System;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearOfRelease { get; set; }

    public Book(string title, string author, int yearOfRelease)
    {
        Title = title;
        Author = author;
        YearOfRelease = yearOfRelease;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Author: {Author}, Year of Release: {YearOfRelease}";
    }
}

public class BookManager
{
    private List<Book> books;

    public BookManager()
    {
        books = new List<Book>
        {
            //here are some books as a default reference of sorts
            new Book("Vefxistyaosani", "Shota Rustaveli", 1200),
            new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925),
            new Book("1984", "George Orwell", 1949),
            new Book("Pride and Prejudice", "Jane Austen", 1813),
            new Book("The Catcher in the Rye", "J.D. Salinger", 1951),
            new Book("Animal Farm", "George Orwell", 1945),
            new Book("The Hobbit", "J.R.R. Tolkien", 1937),
            new Book("Brave New World", "Aldous Huxley", 1932),
            new Book("The Lord of the Rings", "J.R.R. Tolkien", 1954),
            new Book("The Chronicles of Narnia", "C.S. Lewis", 1950),
            new Book("To Kill a Mockingbird", "Harper Lee", 1960),
            new Book("Moby-Dick", "Herman Melville", 1851),
            new Book("The Adventures of Huckleberry Finn", "Mark Twain", 1884),
            new Book("The Picture of Dorian Gray", "Oscar Wilde", 1890),
            new Book("War and Peace", "Leo Tolstoy", 1869)
        };
    }

    public void AddBook(string title, string author, int yearOfRelease)
    {
        if (yearOfRelease > DateTime.Now.Year)
        {
            Console.WriteLine("Invalid year of release.");
            return;
        }

        books.Add(new Book(title, author, yearOfRelease));
        Console.WriteLine("Book added successfully.");
    }

    public void ShowAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        Console.WriteLine("List of all books:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void SearchByTitle(string title)
    {
        var matchingBooks = books.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchingBooks.Count == 0)
        {
            Console.WriteLine("No matching books found.");
            return;
        }

        Console.WriteLine($"Matching books with title '{title}':");
        foreach (var book in matchingBooks)
        {
            Console.WriteLine(book);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BookManager bookManager = new BookManager();

        while (true)
        {
            Console.WriteLine("\nThe Book Manager: ");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. Show All Books");
            Console.WriteLine("3. Search Books by Title");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the Title of the Book: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter the Author of the Book: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter the Year of Release of the Book: ");
                    int year;
                    if (!int.TryParse(Console.ReadLine(), out year))
                    {
                        Console.WriteLine("Invalid year format.");
                        continue;
                    }
                    bookManager.AddBook(title, author, year);
                    break;
                case 2:
                    bookManager.ShowAllBooks();
                    break;
                case 3:
                    Console.Write("Enter the Title to Search for: ");
                    string searchTitle = Console.ReadLine();
                    bookManager.SearchByTitle(searchTitle);
                    break;
                case 4:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
