
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// ======================== PRODUCT BL ========================
class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public int MinimumStock { get; set; }

    public Product() { }

    public Product(string name, string category, double price,
                   int stock, int minimumStock)
    {
        Name = name;
        Category = category;
        Price = price;
        Stock = stock;
        MinimumStock = minimumStock;
    }

    public double GetTaxRate()
    {
        if (Category.Equals("Grocery",
            StringComparison.OrdinalIgnoreCase))
            return 0.10;

        if (Category.Equals("Fruit",
            StringComparison.OrdinalIgnoreCase))
            return 0.05;

        return 0.15;
    }

    public double GetPriceWithTax(int quantity)
    {
        return Price * quantity * (1 + GetTaxRate());
    }
}

// ======================== CUSTOMER BL ========================
class Customer
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }

    public Customer() { }

    public Customer(string username, string password, string email,
                    string address, string contactNumber)
    {
        Username = username;
        Password = password;
        Email = email;
        Address = address;
        ContactNumber = contactNumber;
    }
}

// ======================== ADMIN BL ========================
class MUser
{
    public string Username { get; set; }
    public string Password { get; set; }

    public MUser() { }

    public MUser(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

// ======================== PRODUCT DL ========================
class ProductDL
{
    private static List<Product> products = new List<Product>();
    private const string FileName = "Products.json";

    public static void Load()
    {
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);
            products = JsonSerializer.Deserialize<List<Product>>(json)
                       ?? new List<Product>();
        }
    }

    public static void Save()
    {
        string json = JsonSerializer.Serialize(products);
        File.WriteAllText(FileName, json);
    }

    public static void Add(Product product)
    {
        products.Add(product);
        Save();
    }

    public static List<Product> GetAll()
    {
        return products;
    }

    public static Product FindByName(string name)
    {
        return products.FirstOrDefault(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}

// ======================== CUSTOMER DL ========================
class CustomerDL
{
    private static List<Customer> customers = new List<Customer>();
    private const string FileName = "Customers.json";

    public static void Load()
    {
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);
            customers = JsonSerializer.Deserialize<List<Customer>>(json)
                        ?? new List<Customer>();
        }
    }

    public static void Save()
    {
        string json = JsonSerializer.Serialize(customers);
        File.WriteAllText(FileName, json);
    }

    public static void Add(Customer customer)
    {
        customers.Add(customer);
        Save();
    }

    public static Customer Find(string username, string password)
    {
        return customers.FirstOrDefault(c =>
            c.Username == username && c.Password == password);
    }

    public static bool UsernameExists(string username)
    {
        return customers.Any(c => c.Username == username);
    }
}

// ======================== ADMIN DL ========================
class AdminDL
{
    // Demo credentials; replace with the admin system
    // provided by your teacher if one already exists.
    public static MUser Admin =
        new MUser("AAA", "111");

    public static bool IsValid(string username, string password)
    {
        return Admin.Username == username &&
               Admin.Password == password;
    }
}

// ======================== CONSOLE UTILITY ========================
class ConsoleUtility
{
    public static int ReadInt(string message)
    {
        int value;
        Console.Write(message);

        while (!int.TryParse(Console.ReadLine(), out value))
            Console.Write("Enter a valid whole number: ");

        return value;
    }

    public static double ReadDouble(string message)
    {
        double value;
        Console.Write(message);

        while (!double.TryParse(Console.ReadLine(), out value))
            Console.Write("Enter a valid number: ");

        return value;
    }

    public static string ReadText(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? "";
    }

    public static void ShowProducts()
    {
        List<Product> products = ProductDL.GetAll();

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (Product p in products)
        {
            Console.WriteLine(
                $"{p.Name} | {p.Category} | Price: {p.Price:F2}" +
                $" | Stock: {p.Stock} | Minimum: {p.MinimumStock}");
        }
    }
}

// ======================== PRODUCT UI ========================
class ProductUI
{
    public static void AddProduct()
    {
        string name = ConsoleUtility.ReadText("Product name: ");
        string category = ConsoleUtility.ReadText(
            "Category (Grocery/Fruit/Other): ");
        double price = ConsoleUtility.ReadDouble("Unit price: ");
        int stock = ConsoleUtility.ReadInt("Available stock: ");
        int minimum = ConsoleUtility.ReadInt("Minimum stock threshold: ");

        if (price < 0 || stock < 0 || minimum < 0)
        {
            Console.WriteLine("Price and quantities cannot be negative.");
            return;
        }

        Product product = new Product(name, category, price, stock, minimum);
        ProductDL.Add(product);

        Console.WriteLine("Product added.");
    }

    public static void ViewProducts()
    {
        ConsoleUtility.ShowProducts();
    }

    public static void HighestPrice()
    {
        List<Product> products = ProductDL.GetAll();

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        Product highest = products.OrderByDescending(p => p.Price).First();

        Console.WriteLine(
            $"Highest unit price: {highest.Name}, {highest.Price:F2}");
    }

    public static void ViewSalesTax()
    {
        foreach (Product p in ProductDL.GetAll())
        {
            double tax = p.Price * p.GetTaxRate();

            Console.WriteLine(
                $"{p.Name}: tax rate {p.GetTaxRate() * 100:F0}%, " +
                $"tax per unit {tax:F2}");
        }
    }

    public static void ProductsToOrder()
    {
        List<Product> lowStock = ProductDL.GetAll()
            .Where(p => p.Stock < p.MinimumStock)
            .ToList();

        if (lowStock.Count == 0)
        {
            Console.WriteLine("No products need ordering.");
            return;
        }

        foreach (Product p in lowStock)
        {
            Console.WriteLine(
                $"{p.Name}: stock {p.Stock}, threshold {p.MinimumStock}");
        }
    }
}

// ======================== CUSTOMER UI ========================
class CustomerUI
{
    // Cart maps a product name to the quantity selected.
    private static Dictionary<string, int> cart =
        new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    public static void SignUp()
    {
        string username = ConsoleUtility.ReadText("Username: ");

        if (CustomerDL.UsernameExists(username))
        {
            Console.WriteLine("That username already exists.");
            return;
        }

        string password = ConsoleUtility.ReadText("Password: ");
        string email = ConsoleUtility.ReadText("Email: ");
        string address = ConsoleUtility.ReadText("Address: ");
        string contact = ConsoleUtility.ReadText("Contact number: ");

        Customer customer = new Customer(
            username, password, email, address, contact);

        CustomerDL.Add(customer);
        Console.WriteLine("Registration successful.");
    }

    public static Customer SignIn()
    {
        string username = ConsoleUtility.ReadText("Username: ");
        string password = ConsoleUtility.ReadText("Password: ");

        Customer customer = CustomerDL.Find(username, password);

        if (customer == null)
            Console.WriteLine("Invalid customer credentials.");

        return customer;
    }

    public static void ViewProfile(Customer customer)
    {
        Console.WriteLine($"Username: {customer.Username}");
        Console.WriteLine($"Password: {customer.Password}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Address: {customer.Address}");
        Console.WriteLine($"Contact: {customer.ContactNumber}");
    }

    public static void BuyProducts()
    {
        ConsoleUtility.ShowProducts();

        string name = ConsoleUtility.ReadText("Product to buy: ");
        Product product = ProductDL.FindByName(name);

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        int quantity = ConsoleUtility.ReadInt("Quantity: ");

        if (quantity <= 0 || quantity > product.Stock)
        {
            Console.WriteLine("Invalid quantity or insufficient stock.");
            return;
        }

        // Deduct stock immediately and persist the updated product.
        product.Stock -= quantity;
        ProductDL.Save();

        if (cart.ContainsKey(product.Name))
            cart[product.Name] += quantity;
        else
            cart[product.Name] = quantity;

        Console.WriteLine("Purchase recorded. Stock updated.");
    }

    public static void GenerateInvoice()
    {
        if (cart.Count == 0)
        {
            Console.WriteLine("You have no recorded purchases.");
            return;
        }

        double subtotal = 0;
        double totalTax = 0;

        Console.WriteLine("\n========== INVOICE ==========");

        foreach (var item in cart)
        {
            Product product = ProductDL.FindByName(item.Key);

            if (product == null)
                continue;

            int quantity = item.Value;
            double lineSubtotal = product.Price * quantity;
            double lineTax = lineSubtotal * product.GetTaxRate();

            subtotal += lineSubtotal;
            totalTax += lineTax;

            Console.WriteLine(
                $"{product.Name} x {quantity} | " +
                $"Subtotal: {lineSubtotal:F2} | Tax: {lineTax:F2}");
        }

        Console.WriteLine($"Subtotal: {subtotal:F2}");
        Console.WriteLine($"Total tax: {totalTax:F2}");
        Console.WriteLine($"Total payable: {subtotal + totalTax:F2}");
        Console.WriteLine("=============================");
    }
}

// ======================== ADMIN UI ========================
class AdminUI
{
    public static void ViewProfile()
    {
        Console.WriteLine($"Username: {AdminDL.Admin.Username}");
        Console.WriteLine($"Password: {AdminDL.Admin.Password}");
    }

    public static void ShowMenu()
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== ADMIN MENU =====");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Find Highest Unit Price");
            Console.WriteLine("4. View Sales Tax");
            Console.WriteLine("5. Products to be Ordered");
            Console.WriteLine("6. View Profile");
            Console.WriteLine("7. Exit");

            choice = ConsoleUtility.ReadInt("Choice: ");

            switch (choice)
            {
                case 1: ProductUI.AddProduct(); break;
                case 2: ProductUI.ViewProducts(); break;
                case 3: ProductUI.HighestPrice(); break;
                case 4: ProductUI.ViewSalesTax(); break;
                case 5: ProductUI.ProductsToOrder(); break;
                case 6: ViewProfile(); break;
                case 7: break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        } while (choice != 7);
    }
}

// ======================== CUSTOMER MENU ========================
class CustomerMenuUI
{
    public static void ShowMenu(Customer customer)
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== CUSTOMER MENU =====");
            Console.WriteLine("1. View All Products");
            Console.WriteLine("2. Buy Products");
            Console.WriteLine("3. Generate Invoice");
            Console.WriteLine("4. View Profile");
            Console.WriteLine("5. Exit");

            choice = ConsoleUtility.ReadInt("Choice: ");

            switch (choice)
            {
                case 1: ProductUI.ViewProducts(); break;
                case 2: CustomerUI.BuyProducts(); break;
                case 3: CustomerUI.GenerateInvoice(); break;
                case 4: CustomerUI.ViewProfile(customer); break;
                case 5: break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        } while (choice != 5);
    }
}

// ======================== DRIVER CODE ========================
class Program
{
    static void Main()
    {
        ProductDL.Load();
        CustomerDL.Load();

        int choice;

        do
        {
            Console.WriteLine("\n===== DEPARTMENTAL STORE =====");
            Console.WriteLine("1. Sign In");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("3. Exit");

            choice = ConsoleUtility.ReadInt("Choice: ");

            switch (choice)
            {
                case 1:
                    string username =
                        ConsoleUtility.ReadText("Username: ");
                    string password =
                        ConsoleUtility.ReadText("Password: ");

                    if (AdminDL.IsValid(username, password))
                    {
                        AdminUI.ShowMenu();
                    }
                    else
                    {
                        Customer customer =
                            CustomerDL.Find(username, password);

                        if (customer == null)
                            Console.WriteLine("Invalid credentials.");
                        else
                            CustomerMenuUI.ShowMenu(customer);
                    }
                    break;

                case 2:
                    CustomerUI.SignUp();
                    break;

                case 3:
                    Console.WriteLine("Goodbye.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        } while (choice != 3);
    }
}
