using System;
using System.IO;

class User
{
    public string Username;
    public string Password;
    public string Role;

    public User(string Username, string Password, string Role)
    {
        this.Username = Username;
        this.Password = Password;
        this.Role = Role;
    }
}

class Book
{
    public string BookID;
    public string BookName;
    public double Price;

    public Book(string BookID, string BookName, double Price)
    {
        this.BookID = BookID;
        this.BookName = BookName;
        this.Price = Price;
    }

    public void Display()
    {
        Console.WriteLine("Book ID: " + BookID);
        Console.WriteLine("Book Name: " + BookName);
        Console.WriteLine("Price: " + Price);
        Console.WriteLine();
    }
}

class Program
{
    // ================= SIGN UP =================

    static void SignUp()
    {
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        Console.Write("Enter Role (Admin/User): ");
        string role = Console.ReadLine();

        User user = new User(username, password, role);

        StreamWriter sw = new StreamWriter("users.txt", true);

        sw.WriteLine(user.Username + "," + user.Password + "," + user.Role);

        sw.Close();

        Console.WriteLine("Sign Up Successful!");
    }

    // ================= SIGN IN =================

    static User SignIn()
    {
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        if (!File.Exists("users.txt"))
        {
            Console.WriteLine("No users found!");
            return null;
        }

        string[] lines = File.ReadAllLines("users.txt");

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            User user = new User(data[0], data[1], data[2]);

            if (user.Username == username && user.Password == password)
            {
                Console.WriteLine("Sign In Successful!");
                return user;
            }
        }

        Console.WriteLine("Invalid Username or Password!");
        return null;
    }

    // ================= ADD BOOK =================

    static void AddBook()
    {
        Console.Write("Enter Book ID: ");
        string id = Console.ReadLine();

        Console.Write("Enter Book Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Price: ");
        double price = double.Parse(Console.ReadLine());

        Book book = new Book(id, name, price);

        StreamWriter sw = new StreamWriter("books.txt", true);

        sw.WriteLine(book.BookID + "," + book.BookName + "," + book.Price);

        sw.Close();

        Console.WriteLine("Book Added Successfully!");
    }

    // ================= SHOW BOOKS =================

    static void ShowBooks()
    {
        if (!File.Exists("books.txt"))
        {
            Console.WriteLine("No Books Found!");
            return;
        }

        string[] lines = File.ReadAllLines("books.txt");

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            Book book = new Book(
                data[0],
                data[1],
                double.Parse(data[2])
            );

            book.Display();
        }
    }

    // ================= UPDATE BOOK =================

    static void UpdateBook()
    {
        Console.Write("Enter Book ID to Update: ");
        string id = Console.ReadLine();

        if (!File.Exists("books.txt"))
        {
            Console.WriteLine("No Books Found!");
            return;
        }

        string[] lines = File.ReadAllLines("books.txt");

        bool found = false;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            if (data[0] == id)
            {
                Console.Write("Enter New Book Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter New Price: ");
                double price = double.Parse(Console.ReadLine());

                Book book = new Book(id, name, price);

                lines[i] = book.BookID + "," +
                           book.BookName + "," +
                           book.Price;

                found = true;
                break;
            }
        }

        File.WriteAllLines("books.txt", lines);

        if (found)
            Console.WriteLine("Book Updated Successfully!");
        else
            Console.WriteLine("Book Not Found!");
    }

    // ================= DELETE BOOK =================

    static void DeleteBook()
    {
        Console.Write("Enter Book ID to Delete: ");
        string id = Console.ReadLine();

        if (!File.Exists("books.txt"))
        {
            Console.WriteLine("No Books Found!");
            return;
        }

        string[] lines = File.ReadAllLines("books.txt");

        bool found = false;

        StreamWriter sw = new StreamWriter("books_temp.txt");

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            if (data[0] == id)
            {
                found = true;
                continue;
            }

            sw.WriteLine(line);
        }

        sw.Close();

        File.Delete("books.txt");
        File.Move("books_temp.txt", "books.txt");

        if (found)
            Console.WriteLine("Book Deleted Successfully!");
        else
            Console.WriteLine("Book Not Found!");
    }

    // ================= ADMIN MENU =================

    static void AdminMenu()
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== ADMIN MENU =====");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Show Books");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Logout");

            Console.Write("Enter Choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
                AddBook();

            else if (choice == 2)
                ShowBooks();

            else if (choice == 3)
                UpdateBook();

            else if (choice == 4)
                DeleteBook();

            else if (choice == 5)
                Console.WriteLine("Logging Out...");

            else
                Console.WriteLine("Invalid Choice!");

        } while (choice != 5);
    }

    // ================= USER MENU =================

    static void UserMenu()
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== USER MENU =====");
            Console.WriteLine("1. Show Books");
            Console.WriteLine("2. Logout");

            Console.Write("Enter Choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
                ShowBooks();

            else if (choice == 2)
                Console.WriteLine("Logging Out...");

            else
                Console.WriteLine("Invalid Choice!");

        } while (choice != 2);
    }

    // ================= MAIN =================

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== BOOK MANAGEMENT SYSTEM =====");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Sign In");
            Console.WriteLine("3. Exit");

            Console.Write("Enter Choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                SignUp();
            }

            else if (choice == 2)
            {
                User user = SignIn();

                if (user != null)
                {
                    if (user.Role == "Admin" || user.Role == "admin")
                    {
                        AdminMenu();
                    }
                    else
                    {
                        UserMenu();
                    }
                }
            }

            else if (choice == 3)
            {
                Console.WriteLine("Program Ended.");
            }

            else
            {
                Console.WriteLine("Invalid Choice!");
            }

        } while (choice != 3);
    }
}