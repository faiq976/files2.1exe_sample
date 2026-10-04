using System;

class Book
{
    int Bookid;
    string Title;
    string Author;
    double Price;

    public Book()
    {
        Bookid = 0;
        Title = "Unknown";
        Author = "Unknown";
        Price = 0;
    }

    public Book(int Bookid, string Title, string Author, double Price)
    {
        this.Bookid = Bookid;
        this.Title = Title;
        this.Author = Author;
        this.Price = Price;
    }

    public void Display()
    {
        Console.WriteLine("Book ID: " + Bookid);
        Console.WriteLine("Title: " + Title);
        Console.WriteLine("Author: " + Author);
        Console.WriteLine("Price: " + Price);
        Console.WriteLine();
    }

    public void UpdatePrice(double price)
    {
        Price = price;
    }

    public bool CheckAuthor(string author)
    {
        if (Author == author)
        {
            return true;
        }

        return false;
    }

    public int GetBookid()
    {
        return Bookid;
    }

    static void Main(string[] args)
    {
        List<Book> books = new List<Book>();

        Book book1 = new Book();
        Book book2 = new Book(2, "The Alchemist", "Paulo Coelho", 1500);
        Book book3 = new Book(3, "Atomic Habits", "James Clear", 2000);

        books.Add(book1);
        books.Add(book2);
        books.Add(book3);

        Console.WriteLine("All Books:");

        foreach (Book book in books)
        {
            book.Display();
        }

        Console.Write("Enter Book ID to update price: ");
        int searchId = Convert.ToInt(Console.ReadLine());

        Console.Write("Enter New Price: ");
        double newPrice = Convert.ToDouble(Console.ReadLine());

        UpdateBookPrice(books, searchId, newPrice);

        Console.WriteLine("\nBooks after updating price:");

        foreach (Book book in books)
        {
            book.Display();
        }
    }

    static void UpdateBookPrice(List<Book> books, int id, double price)
    {
        foreach (Book book in books)
        {
            if (book.GetBookid() == id)
            {
                book.UpdatePrice(price);
                Console.WriteLine("Price updated successfully!");
                return price;
            }
        }

        Console.WriteLine("Book not found!");
    }
}