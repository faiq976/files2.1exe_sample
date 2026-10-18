
using System;
using System.Collections.Generic;

class Member
{
    private string name;
    private int memberID;
    private List<string> booksBought;
    private int numberOfBooks;
    private double moneyInBank;
    private double amountSpent;

    public Member()
    {
        name = "";
        memberID = 0;
        booksBought = new List<string>();
        numberOfBooks = 0;
        moneyInBank = 0;
        amountSpent = 0;
    }

    public Member(string n, int id, double money)
    {
        name = n;
        memberID = id;
        moneyInBank = money;
        booksBought = new List<string>();
        numberOfBooks = 0;
        amountSpent = 0;
    }

    public void SetName(string n) { name = n; }
    public string GetName() { return name; }
    public bool CheckName(string n) { return name == n; }

    public void SetID(int id) { memberID = id; }
    public int GetID() { return memberID; }

    public void AddBook(string book)
    {
        booksBought.Add(book);
        numberOfBooks++;
    }

    public void ShowBooks()
    {
        foreach (string book in booksBought)
            Console.WriteLine("- " + book);
    }

    public void SetNumberOfBooks(int n)
    {
        if (n >= 0) numberOfBooks = n;
    }

    public void ShowNumberOfBooks()
    {
        Console.WriteLine("Number of books: " + numberOfBooks);
    }

    public void SetMoneyInBank(double money)
    {
        if (money >= 0) moneyInBank = money;
    }

    public void ShowMoneyInBank()
    {
        Console.WriteLine("Money in bank: $" + moneyInBank);
    }

    public void SetAmountSpent(double amount)
    {
        if (amount >= 0) amountSpent = amount;
    }

    public void AddAmountSpent(double amount)
    {
        if (amount >= 0) amountSpent += amount;
    }

    public void ShowAmountSpent()
    {
        Console.WriteLine("Amount spent: $" + amountSpent);
    }

    public void Display()
    {
        Console.WriteLine("\nName: " + name);
        Console.WriteLine("Member ID: " + memberID);
        ShowBooks();
        ShowNumberOfBooks();
        ShowMoneyInBank();
        ShowAmountSpent();
    }
}

class Program
{
    static void Main()
    {
        List<Member> members = new List<Member>();
        int choice;

        do
        {
            Console.WriteLine("\n1. Add Member");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by Name");
            Console.WriteLine("4. Update Name");
            Console.WriteLine("5. Update Bank Balance");
            Console.WriteLine("6. Add Purchased Book");
            Console.WriteLine("7. Display All Members");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter bank balance: ");
                double money = Convert.ToDouble(Console.ReadLine());

                members.Add(new Member(name, id, money));
                Console.WriteLine("Member added.");
            }
            else if (choice == 2)
            {
                Console.Write("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Member? m = members.Find(x => x.GetID() == id);

                if (m == null) Console.WriteLine("Member not found.");
                else m.Display();
            }
            else if (choice == 3)
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine() ?? "";

                Member? m = members.Find(x => x.CheckName(name));

                if (m == null) Console.WriteLine("Member not found.");
                else m.Display();
            }
            else if (choice == 4)
            {
                Console.Write("Enter member ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Member? m = members.Find(x => x.GetID() == id);

                if (m == null)
                {
                    Console.WriteLine("Member not found.");
                }
                else
                {
                    Console.Write("Enter new name: ");
                    m.SetName(Console.ReadLine() ?? "");
                    Console.WriteLine("Name updated.");
                }
            }
            else if (choice == 5)
            {
                Console.Write("Enter member ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Member? m = members.Find(x => x.GetID() == id);

                if (m == null)
                {
                    Console.WriteLine("Member not found.");
                }
                else
                {
                    Console.Write("Enter new bank balance: ");
                    double money = Convert.ToDouble(Console.ReadLine());
                    m.SetMoneyInBank(money);
                }
            }
            else if (choice == 6)
            {
                Console.Write("Enter member ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Member? m = members.Find(x => x.GetID() == id);

                if (m == null)
                {
                    Console.WriteLine("Member not found.");
                }
                else
                {
                    Console.Write("Enter book title: ");
                    m.AddBook(Console.ReadLine() ?? "");

                    Console.Write("Enter amount spent: ");
                    double amount = Convert.ToDouble(Console.ReadLine());
                    m.AddAmountSpent(amount);
                }
            }
            else if (choice == 7)
            {
                foreach (Member m in members)
                    m.Display();
            }

        } while (choice != 0);
    }
}
