using System;

class Student
{
    public string Name;
    public double MatricMarks;
    public double FscMarks;
    public double EcatMarks;

    public double CalculateAggregate()
    {
        return (MatricMarks * 30 / 100) +
               (FscMarks * 40 / 100) +
               (EcatMarks * 30 / 100);
    }

    public void Display()
    {
        Console.WriteLine("Student Name: " + Name);
        Console.WriteLine("Matric Marks: " + MatricMarks);
        Console.WriteLine("FSc Marks: " + FscMarks);
        Console.WriteLine("ECAT Marks: " + EcatMarks);
        Console.WriteLine("Aggregate: " + CalculateAggregate());
        Console.WriteLine();
    }
}

class Program
{
    static Student[] students = new Student[100];
    static int count = 0;

    static void AddStudent()
    {
        Student s = new Student();

        Console.Write("Enter Student Name: ");
        s.Name = Console.ReadLine();

        Console.Write("Enter Matric Marks: ");
        s.MatricMarks = double.Parse(Console.ReadLine());

        Console.Write("Enter FSc Marks: ");
        s.FscMarks = double.Parse(Console.ReadLine());

        Console.Write("Enter ECAT Marks: ");
        s.EcatMarks = double.Parse(Console.ReadLine());

        students[count] = s;
        count++;

        Console.WriteLine("Student Added Successfully!");
    }

    static void ShowStudents()
    {
        if (count == 0)
        {
            Console.WriteLine("No students found!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            students[i].Display();
        }
    }

    static void CalculateAggregate()
    {
        if (count == 0)
        {
            Console.WriteLine("No students found!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(students[i].Name + " Aggregate: " +
                              students[i].CalculateAggregate());
        }
    }

    static void TopStudents()
    {
        if (count == 0)
        {
            Console.WriteLine("No students found!");
            return;
        }

        Student[] temp = new Student[count];

        for (int i = 0; i < count; i++)
        {
            temp[i] = students[i];
        }

        for (int i = 0; i < count - 1; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                if (temp[j].CalculateAggregate() > temp[i].CalculateAggregate())
                {
                    Student x = temp[i];
                    temp[i] = temp[j];
                    temp[j] = x;
                }
            }
        }

        int top = count < 3 ? count : 3;

        Console.WriteLine("Top Students:");

        for (int i = 0; i < top; i++)
        {
            temp[i].Display();
        }
    }

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== Student Management System =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Show Students");
            Console.WriteLine("3. Calculate Aggregate");
            Console.WriteLine("4. Top Students");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                AddStudent();
            }
            else if (choice == 2)
            {
                ShowStudents();
            }
            else if (choice == 3)
            {
                CalculateAggregate();
            }
            else if (choice == 4)
            {
                TopStudents();
            }
            else if (choice == 5)
            {
                Console.WriteLine("Program Ended.");
            }
            else
            {
                Console.WriteLine("Invalid Choice!");
            }

        } while (choice != 5);
    }
}