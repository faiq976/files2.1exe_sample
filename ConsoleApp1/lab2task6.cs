using System;
using System.IO;

class MUser
{
    public string Username;
    public string Password;
    public string Role;

    public MUser(string Username, string Password, string Role)
    {
        this.Username = Username;
        this.Password = Password;
        this.Role = Role;
    }

    public void Display()
    {
        Console.WriteLine("Username: " + Username);
        Console.WriteLine("Role: " + Role);
    }
}

class Program
{
    static void SignUp()
    {
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        Console.Write("Enter Role: ");
        string role = Console.ReadLine();

        StreamWriter sw = new StreamWriter("users.txt", true);

        sw.WriteLine(username + "," + password + "," + role);

        sw.Close();

        Console.WriteLine("Sign Up Successful!");
    }

    static void SignIn()
    {
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        if (!File.Exists("users.txt"))
        {
            Console.WriteLine("No users found!");
            return;
        }

        string[] lines = File.ReadAllLines("users.txt");

        bool found = false;

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            MUser user = new MUser(data[0], data[1], data[2]);

            if (user.Username == username && user.Password == password)
            {
                Console.WriteLine("Sign In Successful!");
                user.Display();
                found = true;
                break;
            }
        }

        if (found == false)
        {
            Console.WriteLine("Invalid Username or Password!");
        }
    }

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== Sign Up / Sign In =====");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Sign In");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                SignUp();
            }
            else if (choice == 2)
            {
                SignIn();
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

