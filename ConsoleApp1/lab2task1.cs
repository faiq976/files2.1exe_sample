using System;

class Transaction
{
    public int TransactionID;
    public string ProductName;
    public double Amount;
    public string Date;
    public string Time;

    public Transaction(int TransactionID, string ProductName, double Amount, string Date, string Time)
    {
        this.TransactionID = TransactionID;
        this.ProductName = ProductName;
        this.Amount = Amount;
        this.Date = Date;
        this.Time = Time;
    }

    public Transaction(Transaction t)
    {
        this.TransactionID = t.TransactionID;
        this.ProductName = t.ProductName;
        this.Amount = t.Amount;
        this.Date = t.Date;
        this.Time = t.Time;
    }

    public void Display()
    {
        Console.WriteLine("Transaction ID: " + TransactionID);
        Console.WriteLine("Product Name: " + ProductName);
        Console.WriteLine("Amount: " + Amount);
        Console.WriteLine("Date: " + Date);
        Console.WriteLine("Time: " + Time);
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Transaction t1 = new Transaction(101, "Laptop", 85000, "04-10-2026", "03:00 PM");

        Transaction t2 = new Transaction(t1);

        Console.WriteLine("Before Changes:");
        Console.WriteLine("Transaction 1:");
        t1.Display();

        Console.WriteLine("Transaction 2:");
        t2.Display();

        t1.ProductName = "Mobile";
        t1.Amount = 50000;

        t2.ProductName = "Tablet";
        t2.Amount = 35000;

        Console.WriteLine("After Changes:");
        Console.WriteLine("Transaction 1:");
        t1.Display();

        Console.WriteLine("Transaction 2:");
        t2.Display();
    }
}     