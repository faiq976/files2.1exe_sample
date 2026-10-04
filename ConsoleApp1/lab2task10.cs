using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
class Car
{
    int Carid;
    string Model;
    double Price;
    public Car(int Carid, string Model, double Price)
    {
        this.Carid = Carid;
        this.Model = Model;
        this.Price = Price;
    }
    public void Display()
    {
        Console.WriteLine("Carid:" + Carid);
        Console.WriteLine("Model:" + Model);
        Console.WriteLine("Price" + Price);
    }
    public double GetPrice()
    {
        return Price;
    }
    static void Main(string[] args)
    {
        List<Car> cars = new List<Car>();
        cars.Add(new Car(1, "Toyota Corolla", 5000000));
        cars.Add(new Car(2, "Honda Civic", 8000000));
        cars.Add(new Car(3, "Suzuki Swift", 3500000));

        cars.Add(new Car(4, "Toyota Camry", 12000000));

        Console.WriteLine("All cars:");
        foreach (Car car in cars)
        {
            car.Display();
        }
        Car expensiveCar = cars[0];
        foreach (Car car in cars)
        {
            if (car.GetPrice() > expensiveCar.GetPrice())
            {
                expensiveCar = car;
            }
        }
        Console.WriteLine("Most expensive car:");
        expensiveCar.Display();
    }
}