
using System;
using System.Collections.Generic;

class Angle
{
    private int degrees;
    private float minutes;
    private char direction;

    public Angle(int degrees, float minutes, char direction)
    {
        this.degrees = degrees;
        this.minutes = minutes;
        this.direction = char.ToUpper(direction);
    }

    public void SetAngle()
    {
        Console.Write("Enter degrees: ");
        degrees = int.Parse(Console.ReadLine()!);

        Console.Write("Enter minutes: ");
        minutes = float.Parse(Console.ReadLine()!);

        Console.Write("Enter direction (N/S/E/W): ");
        direction = char.ToUpper(Console.ReadLine()![0]);
    }

    public void DisplayAngle()
    {
        Console.Write($"{degrees}\u00b0{minutes}' {direction}");
    }
}

class Ship
{
    private string shipNumber;
    private Angle latitude;
    private Angle longitude;

    public Ship(string shipNumber, Angle latitude, Angle longitude)
    {
        this.shipNumber = shipNumber;
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public string GetShipNumber()
    {
        return shipNumber;
    }

    public void DisplayPosition()
    {
        Console.Write("Ship is at ");
        latitude.DisplayAngle();
        Console.Write(" and ");
        longitude.DisplayAngle();
        Console.WriteLine();
    }

    public void DisplaySerialNumber()
    {
        Console.WriteLine("Ship's serial number is " + shipNumber);
    }

    public void ChangePosition()
    {
        Console.WriteLine("Enter Ship Latitude:");
        latitude.SetAngle();

        Console.WriteLine("Enter Ship Longitude:");
        longitude.SetAngle();

        Console.WriteLine("Ship position updated successfully!");
    }
}

class Program
{
    static void Main()
    {
        List<Ship> ships = new List<Ship>();
        int choice;

        do
        {
            Console.WriteLine("\n--- SHIP NAVIGATION MENU ---");
            Console.WriteLine("1. Add Ship");
            Console.WriteLine("2. View Ship Position");
            Console.WriteLine("3. View Ship Serial Number");
            Console.WriteLine("4. Change Ship Position");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    {
                        Console.Write("Enter Ship Number: ");
                        string number = Console.ReadLine()!;

                        Console.WriteLine("Enter Ship Latitude:");
                        Console.Write("Enter Latitude's Degree: ");
                        int latDegree = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter Latitude's Minute: ");
                        float latMinute = float.Parse(Console.ReadLine()!);
                        Console.Write("Enter Latitude's Direction: ");
                        char latDirection =
                            char.ToUpper(Console.ReadLine()![0]);

                        Console.WriteLine("Enter Ship Longitude:");
                        Console.Write("Enter Longitude's Degree: ");
                        int lonDegree = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter Longitude's Minute: ");
                        float lonMinute = float.Parse(Console.ReadLine()!);
                        Console.Write("Enter Longitude's Direction: ");
                        char lonDirection =
                            char.ToUpper(Console.ReadLine()![0]);

                        Angle latitude = new Angle(
                            latDegree, latMinute, latDirection);

                        Angle longitude = new Angle(
                            lonDegree, lonMinute, lonDirection);

                        Ship ship = new Ship(number, latitude, longitude);
                        ships.Add(ship);

                        Console.WriteLine("Ship added successfully!");
                        break;
                    }

                case 2:
                    {
                        Console.Write("Enter Ship Serial Number: ");
                        string number = Console.ReadLine()!;

                        Ship? found = ships.Find(
                            s => s.GetShipNumber().Equals(
                                number, StringComparison.OrdinalIgnoreCase));

                        if (found != null)
                            found.DisplayPosition();
                        else
                            Console.WriteLine("Ship not found!");

                        break;
                    }

                case 3:
                    {
                        Console.Write("Enter Ship Latitude: ");
                        string latitudeInput = Console.ReadLine()!;

                        Console.Write("Enter Ship Longitude: ");
                        string longitudeInput = Console.ReadLine()!;

                        Ship? found = ships.Find(s =>
                        {
                            // Match the entered coordinates to the ship's
                            // displayed position.
                            return GetPositionPart(s, true)
                                       .Equals(latitudeInput,
                                           StringComparison.OrdinalIgnoreCase)
                                && GetPositionPart(s, false)
                                       .Equals(longitudeInput,
                                           StringComparison.OrdinalIgnoreCase);
                        });

                        if (found != null)
                            found.DisplaySerialNumber();
                        else
                            Console.WriteLine("Ship not found at that position!");

                        break;
                    }

                case 4:
                    {
                        Console.Write("Enter Ship's serial number: ");
                        string number = Console.ReadLine()!;

                        Ship? found = ships.Find(
                            s => s.GetShipNumber().Equals(
                                number, StringComparison.OrdinalIgnoreCase));

                        if (found != null)
                            found.ChangePosition();
                        else
                            Console.WriteLine("Ship not found!");

                        break;
                    }

                case 5:
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

        } while (choice != 5);
    }

    static string GetPositionPart(Ship ship, bool isLatitude)
    {
        // Use the ship's formatted position to obtain the requested
        // coordinate without exposing the Angle objects.
        return ship.GetFormattedAngle(isLatitude);
    }
}
