using System;

class ClockType
{
    public int Hours;
    public int Minutes;
    public int Seconds;

    public ClockType(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }
    public int ElapsedTime()
    {
        return Hours * 3600 + Minutes * 60 + Seconds;
    }
    public int RemainingTime()
    {
        return 86400 - ElapsedTime();
    }
    public int TimeDifference(ClockType other)
    {
        int difference = ElapsedTime() - other.ElapsedTime();

        if (difference < 0)
            difference = -difference;

        return difference;
    }
    public void DisplayTime()
    {
        Console.WriteLine(
            Hours.ToString("D2") + ":" +
            Minutes.ToString("D2") + ":" +
            Seconds.ToString("D2"));
    }
}

class Program
{
    static void Main()
    {
        ClockType clock1 = new ClockType(10, 30, 45);
        ClockType clock2 = new ClockType(8, 15, 20);
        Console.Write("Clock 1: ");
        clock1.DisplayTime();
        Console.Write("Clock 2: ");
        clock2.DisplayTime();

        Console.WriteLine("Elapsed seconds: " + clock1.ElapsedTime());
        Console.WriteLine("Remaining seconds: " + clock1.RemainingTime());
        Console.WriteLine("Time difference in seconds: " + clock1.TimeDifference(clock2));
    }
}