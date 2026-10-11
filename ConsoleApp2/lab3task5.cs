
using System;

class Calculator
{
    public double Num1;
    public double Num2;

    public Calculator()
    {
        Num1 = 10;
        Num2 = 10;
    }

    public Calculator(double num1, double num2)
    {
        Num1 = num1;
        Num2 = num2;
    }

    public double Add()
    {

        Num1 + Num2;
    }
    public double Subtract()
    {
        Num1 - Num2;
    }
    public double Multiply()
    {
        Num1* Num2;
    }
    public double Divide()
    {
        if (Number2 == 0)
            throw new DivideByZeroException("Cannot divide by zero.");

        return Num1 / Num2;
    }

    public double Modulo()
    {
        if (Numb2 == 0)
            throw new DivideByZeroException("Cannot calculate modulo by zero.");

        return Num1 % Num2;
    }

    public void ChangeValues(double num1, double num2)
    {
        Num1 = num1;
        Num2 = num2;
    }

    public double SquareRoot(int number)
    {
        if (number < 0)
            throw new ArgumentException("Square root of a negative number is not real.");

        return Math.Sqrt(number);
    }

    public double Exponential(int exponent)
    {
        return Math.Exp(exponent);
    }

    public double Logarithm(int number)
    {
        if (number <= 0)
            throw new ArgumentException("Natural logarithm requires a positive number.");

        return Math.Log(number);
    }

    public double Sine(int angle)
    {
        double radians = angle * Math.PI / 180.0;
        return Math.Sin(radians);
    }

    public double Cosine(int angle)
    {
        double radians = angle * Math.PI / 180.0;
        return Math.Cos(radians);
    }

    public double Tangent(int angle)
    {
        double radians = angle * Math.PI / 180.0;

        if (Math.Abs(Math.Cos(radians)) < 1e-10)
            throw new ArgumentException("Tangent is undefined for this angle.");

        return Math.Tan(radians);
    }

    public void DisplayValues()
    {
        Console.WriteLine("Number 1: " + Number1);
        Console.WriteLine("Number 2: " + Number2);
    }
}

class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();
        int choice = 0;

        do
        {
            Console.WriteLine("\n===== CALCULATOR MENU =====");
            Console.WriteLine("1. Create/change calculator object");
            Console.WriteLine("2. Change values");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Subtract");
            Console.WriteLine("5. Multiply");
            Console.WriteLine("6. Divide");
            Console.WriteLine("7. Modulo");
            Console.WriteLine("8. Advanced mathematical functions");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Enter a valid menu number.");
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter first number: ");
                        double n1 = double.Parse(Console.ReadLine());

                        Console.Write("Enter second number: ");
                        double n2 = double.Parse(Console.ReadLine());

                        calc = new Calculator(n1, n2);
                        Console.WriteLine("New calculator object created.");
                        calc.DisplayValues();
                        break;

                    case 2:
                        Console.Write("Enter new first number: ");
                        double newN1 = double.Parse(Console.ReadLine());

                        Console.Write("Enter new second number: ");
                        double newN2 = double.Parse(Console.ReadLine());

                        calc.ChangeValues(newN1, newN2);
                        Console.WriteLine("Values changed.");
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
                        Console.WriteLine("Division = " + calc.Divide());
                        break;

                    case 7:
                        Console.WriteLine("Modulo = " + calc.Modulo());
                        break;

                    case 8:
                        Console.WriteLine("\n--- ADVANCED FUNCTIONS ---");
                        Console.WriteLine("1. Square root");
                        Console.WriteLine("2. Exponential (e^x)");
                        Console.WriteLine("3. Natural logarithm (ln x)");
                        Console.WriteLine("4. Sine");
                        Console.WriteLine("5. Cosine");
                        Console.WriteLine("6. Tangent");
                        Console.Write("Choose a function: ");

                        if (!int.TryParse(Console.ReadLine(), out int functionChoice))
                        {
                            Console.WriteLine("Invalid function choice.");
                            break;
                        }

                        Console.Write("Enter an integer value or angle in degrees: ");

                        if (!int.TryParse(Console.ReadLine(), out int value))
                        {
                            Console.WriteLine("Please enter a valid integer.");
                            break;
                        }

                        switch (functionChoice)
                        {
                            case 1:
                                Console.WriteLine("Square root = " +
                                    calc.SquareRoot(value));
                                break;

                            case 2:
                                Console.WriteLine("Exponential = " +
                                    calc.Exponential(value));
                                break;

                            case 3:
                                Console.WriteLine("Natural logarithm = " +
                                    calc.Logarithm(value));
                                break;

                            case 4:
                                Console.WriteLine("Sine = " + calc.Sine(value));
                                break;

                            case 5:
                                Console.WriteLine("Cosine = " + calc.Cosine(value));
                                break;

                            case 6:
                                Console.WriteLine("Tangent = " + calc.Tangent(value));
                                break;

                            default:
                                Console.WriteLine("Invalid function choice.");
                                break;
                        }
                        break;

                    case 9:
                        Console.WriteLine("Calculator closed.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Select 1 to 9.");
                        break;
                }
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The value is too large.");
            }

        } while (choice != 9);
    }
}
