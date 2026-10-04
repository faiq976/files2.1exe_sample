using System;

class ATM
{
    double balance;
    List<string> transaction_history;

    public ATM(double balance)
    {
        this.balance = balance;
        transaction_history = new List<string>();
    }
    public void deposit(double amount)
    {
        balance = balance + amount;
    }
    public void withdrawal(double amount)
    {
        if (amount <= balance)
        {
            balance = balance - amount;
            transaction_history.Add("Withdrawal:" + amount);
        }
        else
        {
            Console.WriteLine("Insufficient balance!");
        }
    }
    public double check_balance()
    {
        return balance;
    }
    public void transaction_history()
    {
        Console.WriteLine("Transaction history:");
        foreach (string transaction in transaction_history)
        {
            Console.WriteLine(transaction);
        }
    }
    static void Main (string[] args)
    {
        ATM atm = new ATM(10000);
        
        atm.deposit(50000);
        atm.withdrawal(20000);
        atm.withdrawal(15000);

        Console.WriteLine("Current balance:" + atm.check_balance());
        atm.transaction_history();
    }
}