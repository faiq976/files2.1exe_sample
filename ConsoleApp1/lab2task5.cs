using System;
using System.Xml.Serialization;

class Product
{
    int Productid;
    string ProductName;
    double Price;
    string Category;
    string BrandName;
    string Country;
    public AddProduct(int Productid, string ProductName, double Price, string Category, string BrandName, string Country)
    {
        this.Productid = Productid;
        this.ProductName = ProductName;
        this.Price = Price;
        this.Category = Category;
        this.BrandName = BrandName;
        this.Country = Country;
    }
    public void Display()
    {
        Console.WriteLine("1.Add P:roducts");
        Console.WriteLine("2.Show Products");
        Console.WriteLine("3.Total Store Worth");
        Console.WriteLine("Choose any option(1-3):");
        int Choice = int.Parse(Console.ReadLine());
    }
    static void Main(string[] args)
    {
        List<Product> products = new List<Product>();
        if (Choice == 1)
        {
            foreach (Product product in products)
            {
                AddProduct();
            }
        }
        if (Choice == 2)
        {
            foreach (Product product in products)
            {
                Product.Display();
            }
        }
        int TotalWorth = 0;
        if (Choice == 3)
        {
            foreach (Product product in products)
            {
                TotalWorth = TotalWorth + Price;
            }
        }
        Console.WriteLine("Total Store Worth:" + Totalworth);
    }
}