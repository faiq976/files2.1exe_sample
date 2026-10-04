using System;

class BankAccount
{
    int AccountNumber;
    string Holdername;
    double Balance;

    public BankAccount(int AccountNumber, string Holdername, double Balance)
    {
        this.AccountNumber = AccountNumber;
        this.Holdername = Holdername;
        this.Balance = Balance;
    }

    public void Deposit(double amount)
    {
        Balance = Balance + amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= Balance)
        {
            Balance = Balance - amount;
        }
        else
        {
            Console.WriteLine("Insufficient Balance!");
        }
    }

    public void DisplayBalance()
    {
        Console.WriteLine("Balance: " + Balance);
    }

    public void DisplayDetails()
    {
        Console.WriteLine("Account Number: " + AccountNumber);
        Console.WriteLine("Holder Name: " + Holdername);
        Console.WriteLine("Balance: " + Balance);
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        List<BankAccount> accounts = new List<BankAccount>();

        accounts.Add(new BankAccount(101, "Faiq", 50000));
        accounts.Add(new BankAccount(102, "Ali", 30000));

        accounts[0].Deposit(10000);
        accounts[0].Withdraw(5000);

        Console.WriteLine("Account Balance:");
        accounts[0].DisplayBalance();

        accounts.Add(new BankAccount(103, "Ahmed", 40000));

        Console.WriteLine();
        Console.WriteLine("All Account Details:");

        foreach (BankAccount account in accounts)
        {
            account.DisplayDetails();
        }
    }
}