using System;
using System.Collections.Generic;

class Book
{
    public string Bookid;
    public string Title;
    public string Author;

    public Book(string Bookid, string Title, string Author)
    {
        this.Bookid = Bookid;
        this.Title = Title;
        this.Author = Author;
    }

    public void Display()
    {
        Console.WriteLine("Book ID: " + Bookid);
        Console.WriteLine("Title: " + Title);
        Console.WriteLine("Author: " + Author);
        Console.WriteLine();
    }
}

class Program
{
    static List<Book> books = new List<Book>();

    static void AddBook()
    {
        Console.Write("Enter Book ID: ");
        string id = Console.ReadLine();

        Console.Write("Enter Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Author: ");
        string author = Console.ReadLine();

        Book book = new Book(id, title, author);

        books.Add(book);

        Console.WriteLine("Book Added Successfully!");
    }

    static void ShowBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No Books Found!");
            return;
        }

        foreach (Book book in books)
        {
            book.Display();
        }
    }

    static void ViewBook()
    {
        Console.Write("Enter Book ID: ");
        string id = Console.ReadLine();

        foreach (Book book in books)
        {
            if (book.Bookid == id)
            {
                book.Display();
                return;
            }
        }

        Console.WriteLine("Book Not Found!");
    }

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== LIBRARY MENU =====");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Display All Books");
            Console.WriteLine("3. View Book by ID");
            Console.WriteLine("4. Exit");

            Console.Write("Enter Choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                AddBook();
            }
            else if (choice == 2)
            {
                ShowBooks();
            }
            else if (choice == 3)
            {
                ViewBook();
            }
            else if (choice == 4)
            {
                Console.WriteLine("Program Ended.");
            }
            else
            {
                Console.WriteLine("Invalid Choice!");
            }

        } while (choice != 4);
    }
}
