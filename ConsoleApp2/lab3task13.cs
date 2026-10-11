
using System;
using System.Collections.Generic;

class Product
{
    public string ProductName;
    public string ProductCategory;
    public double ProductPrice;
    public int StockQuantity;
    public int MinimumStockQuantity;

    public Product(string name, string category, double price,
                   int stock, int minimumStock)
    {
        ProductName = name;
        ProductCategory = category;
        ProductPrice = price;
        StockQuantity = stock;
        MinimumStockQuantity = minimumStock;
    }

    public double GetSalesTaxRate()
    {
        if (ProductCategory.Equals("Grocery",
            StringComparison.OrdinalIgnoreCase))
            return 0.10;

        if (ProductCategory.Equals("Fruit",
            StringComparison.OrdinalIgnoreCase))
            return 0.05;

        return 0.15;
    }

    public double CalculateSalesTax()
    {
        return ProductPrice * GetSalesTaxRate();
    }

    public void DisplayProduct()
    {
        Console.WriteLine("Name: " + ProductName);
        Console.WriteLine("Category: " + ProductCategory);
        Console.WriteLine("Unit Price: " + ProductPrice.ToString("F2"));
        Console.WriteLine("Stock Quantity: " + StockQuantity);
        Console.WriteLine("Minimum Stock: " + MinimumStockQuantity);
        Console.WriteLine("Sales Tax per Unit: " +
                          CalculateSalesTax().ToString("F2"));
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>();
        int choice;

        do
        {
            Console.WriteLine("\n=== Departmental Store ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Find Product with Highest Unit Price");
            Console.WriteLine("4. View Sales Tax of All Products");
            Console.WriteLine("5. Products to Be Ordered");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter product name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter category (Grocery/Fruit/Other): ");
                    string category = Console.ReadLine();

                    Console.Write("Enter unit price: ");
                    double price = double.Parse(Console.ReadLine());

                    Console.Write("Enter stock quantity: ");
                    int stock = int.Parse(Console.ReadLine());

                    Console.Write("Enter minimum stock quantity: ");
                    int minimumStock = int.Parse(Console.ReadLine());

                    Product newProduct = new Product(
                        name, category, price, stock, minimumStock);

                    products.Add(newProduct);

                    Console.WriteLine("Product added successfully.");
                    break;

                case 2:
                    if (products.Count == 0)
                    {
                        Console.WriteLine("No products available.");
                        break;
                    }

                    foreach (Product p in products)
                    {
                        p.DisplayProduct();
                    }
                    break;

                case 3:
                    if (products.Count == 0)
                    {
                        Console.WriteLine("No products available.");
                        break;
                    }

                    Product highest = products[0];

                    foreach (Product p in products)
                    {
                        if (p.ProductPrice > highest.ProductPrice)
                        {
                            highest = p;
                        }
                    }

                    Console.WriteLine("Product with Highest Unit Price:");
                    highest.DisplayProduct();
                    break;

                case 4:
                    if (products.Count == 0)
                    {
                        Console.WriteLine("No products available.");
                        break;
                    }

                    foreach (Product p in products)
                    {
                        Console.WriteLine("Product: " + p.ProductName);
                        Console.WriteLine("Unit Price: " +
                                          p.ProductPrice.ToString("F2"));
                        Console.WriteLine("Tax Rate: " +
                                          (p.GetSalesTaxRate() * 100) + "%");
                        Console.WriteLine("Sales Tax per Unit: " +
                                          p.CalculateSalesTax().ToString("F2"));
                        Console.WriteLine("Price Including Tax: " +
                            (p.ProductPrice + p.CalculateSalesTax()).ToString("F2"));
                        Console.WriteLine();
                    }
                    break;

                case 5:
                    bool found = false;

                    foreach (Product p in products)
                    {
                        if (p.StockQuantity < p.MinimumStockQuantity)
                        {
                            Console.WriteLine("Product to order:");
                            p.DisplayProduct();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No products need to be ordered.");
                    }
                    break;

                case 6:
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

        } while (choice != 6);
    }
}
