
using System;
using System.Collections.Generic;

class Book
{
    private string title;
    private string[] authors = new string[4];
    private int numberOfAuthors;
    private string publisher;
    private string ISBN;
    private double price;
    private int stock;

    public Book()
    {
        title = "";
        publisher = "";
        ISBN = "";
        price = 0;
        stock = 0;
        numberOfAuthors = 0;
    }

    public Book(string t, string p, string isbn,
                double pr, int s)
    {
        title = t;
        publisher = p;
        ISBN = isbn;
        price = pr;
        stock = s;
        numberOfAuthors = 0;
    }

    public void SetTitle(string t) { title = t; }
    public string GetTitle() { return title; }
    public bool CheckTitle(string t) { return title == t; }

    public void AddAuthor(string a)
    {
        if (numberOfAuthors < 4)
            authors[numberOfAuthors++] = a;
        else
            Console.WriteLine("Maximum 4 authors allowed.");
    }

    public void ShowAuthors()
    {
        for (int i = 0; i < numberOfAuthors; i++)
            Console.WriteLine(authors[i]);
    }

    public void SetPublisher(string p) { publisher = p; }
    public string GetPublisher() { return publisher; }
    public bool CheckPublisher(string p) { return publisher == p; }

    public void SetISBN(string i) { ISBN = i; }
    public string GetISBN() { return ISBN; }
    public bool CheckISBN(string i) { return ISBN == i; }

    public void SetPrice(double p)
    {
        if (p >= 0) price = p;
    }

    public double GetPrice() { return price; }

    public void SetStock(int s)
    {
        if (s >= 0) stock = s;
    }

    public int GetStock() { return stock; }

    public void UpdateStock(int quantity)
    {
        if (stock + quantity >= 0)
            stock += quantity;
        else
            Console.WriteLine("Insufficient stock.");
    }

    public void Display()
    {
        Console.WriteLine("\nTitle: " + title);
        Console.WriteLine("Authors:");
        ShowAuthors();
        Console.WriteLine("Publisher: " + publisher);
        Console.WriteLine("ISBN: " + ISBN);
        Console.WriteLine("Price: $" + price);
        Console.WriteLine("Stock: " + stock);
    }
}

class Program
{
    static void Main()
    {
        List<Book> books = new List<Book>();
        int choice;

        do
        {
            Console.WriteLine("\n1. Add Book");
            Console.WriteLine("2. Display Books");
            Console.WriteLine("3. Search by Title");
            Console.WriteLine("4. Search by ISBN");
            Console.WriteLine("5. Update Stock");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Title: ");
                string t = Console.ReadLine() ?? "";

                Console.Write("Publisher: ");
                string p = Console.ReadLine() ?? "";

                Console.Write("ISBN: ");
                string isbn = Console.ReadLine() ?? "";

                Console.Write("Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                Console.Write("Stock: ");
                int stock = Convert.ToInt32(Console.ReadLine());

                Console.Write("Number of authors (0-4): ");
                int n = Convert.ToInt32(Console.ReadLine());

                if (price < 0 || stock < 0 || n < 0 || n > 4)
                {
                    Console.WriteLine("Invalid information.");
                    continue;
                }

                Book b = new Book(t, p, isbn, price, stock);

                for (int i = 0; i < n; i++)
                {
                    Console.Write("Author " + (i + 1) + ": ");
                    b.AddAuthor(Console.ReadLine() ?? "");
                }

                books.Add(b);
                Console.WriteLine("Book added.");
            }
            else if (choice == 2)
            {
                foreach (Book b in books)
                    b.Display();
            }
            else if (choice == 3)
            {
                Console.Write("Enter title: ");
                string t = Console.ReadLine() ?? "";
                bool found = false;

                foreach (Book b in books)
                {
                    if (b.CheckTitle(t))
                    {
                        b.Display();
                        found = true;
                    }
                }

                if (!found) Console.WriteLine("Book not found.");
            }
            else if (choice == 4)
            {
                Console.Write("Enter ISBN: ");
                string isbn = Console.ReadLine() ?? "";
                bool found = false;

                foreach (Book b in books)
                {
                    if (b.CheckISBN(isbn))
                    {
                        b.Display();
                        found = true;
                    }
                }

                if (!found) Console.WriteLine("Book not found.");
            }
            else if (choice == 5)
            {
                Console.Write("Enter ISBN: ");
                string isbn = Console.ReadLine() ?? "";

                Book? found = books.Find(b => b.CheckISBN(isbn));

                if (found == null)
                {
                    Console.WriteLine("Book not found.");
                    continue;
                }

                Console.Write("Enter quantity (+ add, - remove): ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                found.UpdateStock(quantity);
                Console.WriteLine("Current stock: " + found.GetStock());
            }

        } while (choice != 0);
    }
}
