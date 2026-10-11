using System;

class RacingGameCar
{
    public string CarName;
    public double Speed;
    public double Acceleration;
    public double FuelLevel;
    public double DamageLevel;
    public int Position;
    public int LapCount;

    public RacingGameCar(string carName, double speed, double acceleration,
                         double fuelLevel, double damageLevel, int position, int lapCount)
    {
        CarName = carName;
        Speed = speed;
        Acceleration = acceleration;
        FuelLevel = fuelLevel;
        DamageLevel = damageLevel;
        Position = position;
        LapCount = lapCount;
    }
    public void Accelerate()
    {
        if (FuelLevel <= 0)
        {
            Console.WriteLine("The car has no fuel!");
            return;
        }

        Speed += Acceleration;
        FuelLevel -= 2;

        if (FuelLevel < 0)
            FuelLevel = 0;

        Console.WriteLine("Car accelerated.");
    }
    public void Collide(double damage)
    {
        DamageLevel += damage;

        if (DamageLevel > 100)
            DamageLevel = 100;

        Speed *= 0.5;
        Console.WriteLine("The car collided with an obstacle!");
    }
    public void Refuel(double amount)
    {
        if (amount <= 0)
            return;

        FuelLevel += amount;

        if (FuelLevel > 100)
            FuelLevel = 100;

        Console.WriteLine("Car refuelled.");
    }

    public void Repair(double amount)
    {
        if (amount <= 0)
            return;

        DamageLevel -= amount;

        if (DamageLevel < 0)
            DamageLevel = 0;

        Console.WriteLine("Car repaired.");
    }

    public void CompleteLap()
    {
        LapCount++;
    }

    public void DisplayStatus()
    {
        Console.WriteLine("\n--- Car Status ---");
        Console.WriteLine("Name: " + CarName);
        Console.WriteLine("Speed: " + Speed);
        Console.WriteLine("Acceleration: " + Acceleration);
        Console.WriteLine("Fuel: " + FuelLevel);
        Console.WriteLine("Damage: " + DamageLevel);
        Console.WriteLine("Position: " + Position);
        Console.WriteLine("Laps Completed: " + LapCount);
    }
}

class Program
{
    static void Main()
    {
        RacingGameCar car1 =
            new RacingGameCar("Thunder", 100, 15, 80, 0, 1, 0);

        car1.DisplayStatus();
        car1.Accelerate();
        car1.Collide(20);
        car1.Refuel(15);
        car1.Repair(10);
        car1.CompleteLap();
        car1.DisplayStatus();
    }
}