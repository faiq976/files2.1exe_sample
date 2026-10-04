using System;

class Calculator
{
    public Calculator()
    {
    }
    public double Add(double a, double b)
    {
        return a+b;
    }
    public double Subtract(double a, double b)
    {
        return a-b;
    }
    public double Multiply(double a, double b)
    {
        return a*b;
    }
    public double Divide(double a, double b)
    {
        return a/b;
    }
    static void Main(string [] args)
    {
        Calculator calulator = new Calculator();
        Console.WriteLine("Addition:" + calculator.Add(10, 5));
        Console.WriteLine("Subtraction:" + calculator.Subtract(10, 5));
        Console.WriteLine("Multiplication:" + calculator.Multiply(10, 5));
        Console.WriteLine("Division:" + calculator.Divide(10, 5));
    }
}