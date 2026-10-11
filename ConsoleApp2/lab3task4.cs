
using System;

class Calculator
{
    public double Num1;
    public double Num2;

    // Default constructor
    public Calculator()
    {
        Num1 = 10;
        Num2 = 10;
    }

    // Parameterized constructor
    public Calculator(double num1, double num2)
    {
        Num1 = num1;
        Num2 = num2;
    }

    public double Add()
    {
        return Num1 + Num2;
    }

    public double Subtract()
    {
        return Num1 - Num2;
    }

    public double Multiply()
    {
        return Num1 * Num2;
    }

    public double Divide()
    {
        if (Num2 == 0)
        {
            Console.WriteLine("Error: Cannot divide by zero.");
            return double.NaN;
        }

        return Num1 / Num2;
    }

    public double Modulo()
    {
        if (Num2 == 0)
        {
            Console.WriteLine("Error: Cannot calculate modulo by zero.");
            return double.NaN;
        }

        return Num1 % Num2;
    }

    public void ChangeValues(double num1, double num2)
    {
        Num1 = num1;
        Num2 = num2;
    }

    public void DisplayValues()
    {
        Console.WriteLine("Number 1: " + Num1);
        Console.WriteLine("Number 2: " + Num2);
    }
}

class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();
        int choice;

        do
        {
            Console.WriteLine("\n===== CALCULATOR MENU =====");
            Console.WriteLine("1. Create/change calculator object");
            Console.WriteLine("2. Change values or attributes");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Subtract");
            Console.WriteLine("5. Multiply");
            Console.WriteLine("6. Divide");
            Console.WriteLine("7. Modulo");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Please enter a valid menu number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter first number: ");
                    double n1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter second number: ");
                    double n2 = double.Parse(Console.ReadLine());

                    calc = new Calculator(n1, n2);

                    Console.WriteLine("Calculator object created/changed.");
                    calc.DisplayValues();
                    break;

                case 2:
                    Console.Write("Enter new first number: ");
                    double newN1 = double.Parse(Console.ReadLine());

                    Console.Write("Enter new second number: ");
                    double newN2 = double.Parse(Console.ReadLine());

                    calc.ChangeValues(newN1, newN2);

                    Console.WriteLine("Values updated.");
                    calc.DisplayValues();
                    break;

                case 3:
                    Console.WriteLine("Addition = " + calc.Add());
                    break;

                case 4:
                    Console.WriteLine("Subtraction = " + calc.Subtract());
                    break;

                case 5:
                    Console.WriteLine("Multiplication = " + calc.Multiply());
                    break;

                case 6:
                    if (calc.Number2 == 0)
                        Console.WriteLine("Error: Cannot divide by zero.");
                    else
                        Console.WriteLine("Division = " + calc.Divide());
                    break;

                case 7:
                    if (calc.Number2 == 0)
                        Console.WriteLine("Error: Cannot calculate modulo by zero.");
                    else
                        Console.WriteLine("Modulo = " + calc.Modulo());
                    break;

                case 8:
                    Console.WriteLine("Calculator closed.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Select 1 to 8.");
                    break;
            }

        } while (choice != 8);
    }
}
