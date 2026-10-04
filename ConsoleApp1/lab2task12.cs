using System;

class Book
{
    int Bookid;
    string Title;
    string Author;

    public Book(int Bookid, string Title, string Author)
    {
        this.Bookid = Bookid;
        this.Title = Title;
        this.Author = Author;
    }
    public void Display()
    {
        Console.WriteLine("Book ID:" + Bookid);
        Console.WriteLine("Title:" + Title);
        Console.WriteLine("Author" + Author);
        Console.WriteLine();
    }
    public int GetBookid()
    {
        return Bookid;
    }
    static void Main(string[] args)
    {
        List<Book> Books = new List<Book>();
        books.Add(new Book(1, "The Alchemist", "Paulo Coelho"));
        books.Add(new Book(2, "Harry Potter", "J.K. Rowling"));

        Console.WriteLine("All Books:");
        foreach (Book book in books)
        {
            book.Display();
        }
        books.Add(new Book(3, "Atomic Habits", "James Clear"));
        foreach (Book book in books)
        {
            book.Display();
        }
        int id = 2;
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].GetBookid() == id)
            {
                books.RemoeAt(i);
                break;
            }
        }
        Console.WriteLine("After Removing Book:");
        foreach (Book book in books)
        {
            book.Display();
        }
    }
}