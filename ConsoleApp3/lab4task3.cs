
using System;
using System.Collections.Generic;

class Book
{
    public string Title;
    public string Publisher;
    public string ISBN;
    public string[] Authors = new string[4];
    public int NumberOfAuthors;
    public double Price;
    public int Stock;

    public Book(string title, string publisher, string isbn,
                double price, int stock)
    {
        Title = title;
        Publisher = publisher;
        ISBN = isbn;
        Price = price;
        Stock = stock;
        NumberOfAuthors = 0;
    }

    public void AddAuthor(string author)
    {
        if (NumberOfAuthors < 4)
            Authors[NumberOfAuthors++] = author;
    }

    public void Display()
    {
        Console.WriteLine("\nTitle: " + Title);
        Console.WriteLine("Publisher: " + Publisher);
        Console.WriteLine("ISBN: " + ISBN);
        Console.WriteLine("Price: $" + Price);
        Console.WriteLine("Stock: " + Stock);
        Console.Write("Authors: ");

        for (int i = 0; i < NumberOfAuthors; i++)
            Console.Write(Authors[i] + " ");

        Console.WriteLine();
    }
}

class Member
{
    public string Name;
    public int ID;
    public List<string> BooksBought = new List<string>();
    public List<double> RecentPrices = new List<double>();
    public int NumberOfBooks;
    public double MoneyInBank;
    public double AmountSpent;

    public Member(string name, int id, double money)
    {
        Name = name;
        ID = id;
        MoneyInBank = money;
        NumberOfBooks = 0;
        AmountSpent = 0;
    }

    public double RecordPurchase(string title, double price)
    {
        double bonus = 0;

        // Every 11th book gets an extra discount.
        if (ID != 0 && NumberOfBooks > 0 &&
            (NumberOfBooks + 1) % 11 == 0 &&
            RecentPrices.Count == 10)
        {
            double sum = 0;

            foreach (double p in RecentPrices)
                sum += p;

            bonus = sum / 10.0;
        }

        BooksBought.Add(title);
        NumberOfBooks++;

        RecentPrices.Add(price);

        if (RecentPrices.Count > 10)
            RecentPrices.RemoveAt(0);

        if (ID != 0)
        {
            AmountSpent += price;

            if (NumberOfBooks % 11 == 0)
                AmountSpent = 0;
        }

        return bonus;
    }

    public void Display()
    {
        Console.WriteLine("\nName: " + Name);
        Console.WriteLine("ID: " + ID);
        Console.WriteLine("Books bought: " + NumberOfBooks);
        Console.WriteLine("Amount spent since last bonus: $" + AmountSpent);
        Console.WriteLine("Money in bank: $" + MoneyInBank);

        Console.WriteLine("Purchase history:");

        foreach (string b in BooksBought)
            Console.WriteLine("- " + b);
    }
}

class Program
{
    static List<Book> books = new List<Book>();
    static List<Member> members = new List<Member>();

    static double totalSales = 0;
    static double membershipFees = 0;
    static int totalMembers = 0;
    static int occasionalBuyers = 0;

    static Book? FindBookByISBN(string isbn)
    {
        return books.Find(b => b.ISBN == isbn);
    }

    static Member? FindMemberByID(int id)
    {
        return members.Find(m => m.ID == id);
    }

    static void AddBook()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Publisher: ");
        string publisher = Console.ReadLine() ?? "";

        Console.Write("ISBN: ");
        string isbn = Console.ReadLine() ?? "";

        if (FindBookByISBN(isbn) != null)
        {
            Console.WriteLine("This ISBN already exists.");
            return;
        }

        Console.Write("Price: ");
        double price = Convert.ToDouble(Console.ReadLine());

        Console.Write("Stock: ");
        int stock = Convert.ToInt32(Console.ReadLine());

        Console.Write("Number of authors (0-4): ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (price < 0 || stock < 0 || n < 0 || n > 4)
        {
            Console.WriteLine("Invalid information.");
            return;
        }

        Book b = new Book(title, publisher, isbn, price, stock);

        for (int i = 0; i < n; i++)
        {
            Console.Write("Author " + (i + 1) + ": ");
            b.AddAuthor(Console.ReadLine() ?? "");
        }

        books.Add(b);
        Console.WriteLine("Book added.");
    }

    static void SearchByTitle()
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine() ?? "";

        bool found = false;

        foreach (Book b in books)
        {
            if (b.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                b.Display();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("Book not found.");
    }

    static void SearchByISBN()
    {
        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine() ?? "";

        Book? b = FindBookByISBN(isbn);

        if (b == null)
            Console.WriteLine("Book not found.");
        else
            b.Display();
    }

    static void UpdateStock()
    {
        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine() ?? "";

        Book? b = FindBookByISBN(isbn);

        if (b == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.Write("1. Increase stock\n2. Decrease stock\nChoice: ");
        int option = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        if (quantity < 0)
        {
            Console.WriteLine("Quantity must be positive.");
            return;
        }

        if (option == 1)
        {
            b.Stock += quantity;
        }
        else if (option == 2)
        {
            if (quantity > b.Stock)
            {
                Console.WriteLine("Not enough stock.");
                return;
            }

            b.Stock -= quantity;
        }
        else
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        Console.WriteLine("Stock updated. Current stock: " + b.Stock);
    }

    static void AddMember()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter unique member ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter money in bank: ");
        double money = Convert.ToDouble(Console.ReadLine());

        if (id <= 0 || money < 10 || FindMemberByID(id) != null)
        {
            Console.WriteLine("Invalid ID, duplicate ID, or insufficient funds.");
            return;
        }

        Member m = new Member(name, id, money - 10);
        members.Add(m);

        membershipFees += 10;
        totalSales += 10;
        totalMembers++;

        Console.WriteLine("Member added. $10 membership fee collected.");
    }

    static void SearchMember()
    {
        Console.Write("1. Search by ID\n2. Search by Name\nChoice: ");
        int option = Convert.ToInt32(Console.ReadLine());

        Member? found = null;

        if (option == 1)
        {
            Console.Write("Enter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            found = FindMemberByID(id);
        }
        else if (option == 2)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";

            found = members.Find(m =>
                m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        if (found == null)
            Console.WriteLine("Member not found.");
        else
            found.Display();
    }

    static void UpdateMember()
    {
        Console.Write("1. Find by ID\n2. Find by Name\nChoice: ");
        int option = Convert.ToInt32(Console.ReadLine());

        Member? m = null;

        if (option == 1)
        {
            Console.Write("Enter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            m = FindMemberByID(id);
        }
        else if (option == 2)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";

            m = members.Find(x =>
                x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        if (m == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        Console.Write("1. Update name\n2. Update ID\n3. Update both\nChoice: ");
        int change = Convert.ToInt32(Console.ReadLine());

        string newName = m.Name;
        int newID = m.ID;

        if (change == 1 || change == 3)
        {
            Console.Write("New name: ");
            newName = Console.ReadLine() ?? "";
        }

        if (change == 2 || change == 3)
        {
            Console.Write("New ID: ");
            newID = Convert.ToInt32(Console.ReadLine());
        }

        if (newID <= 0)
        {
            Console.WriteLine("ID must be positive.");
            return;
        }

        Member? duplicate = FindMemberByID(newID);

        if (duplicate != null && duplicate != m)
        {
            Console.WriteLine("That ID is already in use.");
            return;
        }

        m.Name = newName;
        m.ID = newID;

        Console.WriteLine("Member information updated.");
    }

    static void PurchaseBook()
    {
        Console.Write("Enter book ISBN: ");
        string isbn = Console.ReadLine() ?? "";

        Book? b = FindBookByISBN(isbn);

        if (b == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.Write("Quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        if (quantity <= 0 || quantity > b.Stock)
        {
            Console.WriteLine("Invalid quantity or insufficient stock.");
            return;
        }

        Console.Write("Enter member ID (0 for occasional buyer): ");
        int id = Convert.ToInt32(Console.ReadLine());

        Member? m = null;

        if (id != 0)
        {
            m = FindMemberByID(id);

            if (m == null)
            {
                Console.WriteLine("Member not found. Register first.");
                return;
            }
        }
        else
        {
            occasionalBuyers++;
        }

        double transactionTotal = 0;

        for (int i = 0; i < quantity; i++)
        {
            double price = b.Price;
            double discount = 0;

            if (m != null)
            {
                // Regular 5% member discount.
                discount = price * 0.05;

                // Extra discount on every 11th book.
                double bonus = m.RecordPurchase(b.Title, price);

                // The extra discount cannot exceed the remaining price.
                bonus = Math.Min(bonus, price - discount);

                discount += bonus;
            }

            double finalPrice = price - discount;
            transactionTotal += finalPrice;

            if (m != null)
            {
                m.MoneyInBank -= finalPrice;
            }
        }

        if (m == null)
        {
            // Keep occasional buyer purchases in a separate history.
            // They are not eligible for member discounts.
            // Their names and IDs are not stored as member records.
        }

        b.Stock -= quantity;
        totalSales += transactionTotal;

        Console.WriteLine("Book: " + b.Title);
        Console.WriteLine("Quantity: " + quantity);
        Console.WriteLine("Total discount: $" +
                          (b.Price * quantity - transactionTotal));
        Console.WriteLine("Amount payable: $" + transactionTotal);
        Console.WriteLine("Remaining stock: " + b.Stock);
    }

    static void DisplaySales()
    {
        Console.WriteLine("\n===== SALES REPORT =====");
        Console.WriteLine("Total sales including membership fees: $" + totalSales);
        Console.WriteLine("Membership fees collected: $" + membershipFees);
        Console.WriteLine("Registered members: " + totalMembers);
        Console.WriteLine("Occasional-buyer transactions: " + occasionalBuyers);
    }

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== BOOKSTORE MENU =====");
            Console.WriteLine("1. Add a Book");
            Console.WriteLine("2. Search by Title");
            Console.WriteLine("3. Search by ISBN");
            Console.WriteLine("4. Update Stock");
            Console.WriteLine("5. Add a Member");
            Console.WriteLine("6. Search Member");
            Console.WriteLine("7. Update Member Information");
            Console.WriteLine("8. Purchase a Book");
            Console.WriteLine("9. Display Sales");
            Console.WriteLine("10. Exit");
            Console.Write("Enter choice: ");

            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    SearchByTitle();
                    break;
                case 3:
                    SearchByISBN();
                    break;
                case 4:
                    UpdateStock();
                    break;
                case 5:
                    AddMember();
                    break;
                case 6:
                    SearchMember();
                    break;
                case 7:
                    UpdateMember();
                    break;
                case 8:
                    PurchaseBook();
                    break;
                case 9:
                    DisplaySales();
                    break;
                case 10:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 10);
    }
}
