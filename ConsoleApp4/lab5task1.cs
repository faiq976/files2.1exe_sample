
using System;
using System.Collections.Generic;

// ================= SUBJECT BL =================
class SubjectBL
{
    public string Name { get; set; }
    public int CreditHours { get; set; }

    public SubjectBL(string name, int creditHours)
    {
        Name = name;
        CreditHours = creditHours;
    }
}

// ================= SUBJECT DL =================
class SubjectDL
{
    private static List<SubjectBL> subjects = new List<SubjectBL>();

    public static void AddSubject(SubjectBL subject)
    {
        subjects.Add(subject);
    }

    public static List<SubjectBL> GetAllSubjects()
    {
        return subjects;
    }
}

// ================= SUBJECT UI =================
class SubjectUI
{
    public static void AddSubject()
    {
        Console.Write("Enter subject name: ");
        string name = Console.ReadLine();

        Console.Write("Enter credit hours: ");
        int hours = int.Parse(Console.ReadLine());

        SubjectBL subject = new SubjectBL(name, hours);
        SubjectDL.AddSubject(subject);

        Console.WriteLine("Subject added successfully.");
    }

    public static void ViewSubjects()
    {
        List<SubjectBL> subjects = SubjectDL.GetAllSubjects();

        foreach (SubjectBL subject in subjects)
        {
            Console.WriteLine(
                "Name: " + subject.Name +
                ", Credit Hours: " + subject.CreditHours);
        }
    }
}

// ============== DEGREE PROGRAM BL ==============
class DegreeProgramBL
{
    public string Name { get; set; }

    public DegreeProgramBL(string name)
    {
        Name = name;
    }
}

// ============== DEGREE PROGRAM DL ==============
class DegreeProgramDL
{
    private static List<DegreeProgramBL> programs =
        new List<DegreeProgramBL>();

    public static void AddProgram(DegreeProgramBL program)
    {
        programs.Add(program);
    }

    public static List<DegreeProgramBL> GetAllPrograms()
    {
        return programs;
    }
}

// ============== DEGREE PROGRAM UI ==============
class DegreeProgramUI
{
    public static void AddProgram()
    {
        Console.Write("Enter degree program name: ");
        string name = Console.ReadLine();

        DegreeProgramBL program = new DegreeProgramBL(name);
        DegreeProgramDL.AddProgram(program);

        Console.WriteLine("Degree program added successfully.");
    }

    public static void ViewPrograms()
    {
        List<DegreeProgramBL> programs =
            DegreeProgramDL.GetAllPrograms();

        foreach (DegreeProgramBL program in programs)
        {
            Console.WriteLine("Program: " + program.Name);
        }
    }
}

// ================== STUDENT BL ==================
class StudentBL
{
    public string Name { get; set; }
    public int RollNumber { get; set; }

    public StudentBL(string name, int rollNumber)
    {
        Name = name;
        RollNumber = rollNumber;
    }
}

// ================== STUDENT DL ==================
class StudentDL
{
    private static List<StudentBL> students =
        new List<StudentBL>();

    public static void AddStudent(StudentBL student)
    {
        students.Add(student);
    }

    public static List<StudentBL> GetAllStudents()
    {
        return students;
    }
}

// ================== STUDENT UI ==================
class StudentUI
{
    public static void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();

        Console.Write("Enter roll number: ");
        int rollNumber = int.Parse(Console.ReadLine());

        StudentBL student = new StudentBL(name, rollNumber);
        StudentDL.AddStudent(student);

        Console.WriteLine("Student added successfully.");
    }

    public static void ViewStudents()
    {
        List<StudentBL> students = StudentDL.GetAllStudents();

        foreach (StudentBL student in students)
        {
            Console.WriteLine(
                "Name: " + student.Name +
                ", Roll Number: " + student.RollNumber);
        }
    }
}

// ================ CONSOLE UTILITY ================
class ConsoleUtility
{
    public static int ReadChoice()
    {
        Console.Write("Enter your choice: ");
        return int.Parse(Console.ReadLine());
    }

    public static void ShowMainMenu()
    {
        Console.WriteLine("\n===== MAIN MENU =====");
        Console.WriteLine("1. Manage Subjects");
        Console.WriteLine("2. Manage Degree Programs");
        Console.WriteLine("3. Manage Students");
        Console.WriteLine("4. Exit");
    }
}

// ================= DRIVER / MAIN =================
class Program
{
    static void Main()
    {
        int choice;

        do
        {
            ConsoleUtility.ShowMainMenu();
            choice = ConsoleUtility.ReadChoice();

            switch (choice)
            {
                case 1:
                    SubjectMenu();
                    break;

                case 2:
                    DegreeProgramMenu();
                    break;

                case 3:
                    StudentMenu();
                    break;

                case 4:
                    Console.WriteLine("Program ended.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 4);
    }

    static void SubjectMenu()
    {
        Console.WriteLine("\n1. Add Subject");
        Console.WriteLine("2. View Subjects");

        int choice = ConsoleUtility.ReadChoice();

        if (choice == 1)
            SubjectUI.AddSubject();
        else if (choice == 2)
            SubjectUI.ViewSubjects();
        else
            Console.WriteLine("Invalid choice.");
    }

    static void DegreeProgramMenu()
    {
        Console.WriteLine("\n1. Add Degree Program");
        Console.WriteLine("2. View Degree Programs");

        int choice = ConsoleUtility.ReadChoice();

        if (choice == 1)
            DegreeProgramUI.AddProgram();
        else if (choice == 2)
            DegreeProgramUI.ViewPrograms();
        else
            Console.WriteLine("Invalid choice.");
    }

    static void StudentMenu()
    {
        Console.WriteLine("\n1. Add Student");
        Console.WriteLine("2. View Students");

        int choice = ConsoleUtility.ReadChoice();

        if (choice == 1)
            StudentUI.AddStudent();
        else if (choice == 2)
            StudentUI.ViewStudents();
        else
            Console.WriteLine("Invalid choice.");
    }
}
