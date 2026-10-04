using System;

class Student
{
    string Name;
    int Studentid;
    double GPA;

    public Student()
    {
        Name = "Unknown";
        Studentid = 0;
        GPA = 0;
    }

    public Student(string Name, int Studentid, double GPA)
    {
        this.Name = Name;
        this.Studentid = Studentid;
        this.GPA = GPA;
    }

    public void Display()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Student ID: " + Studentid);
        Console.WriteLine("GPA: " + GPA);

        if (GPA >= 3.5)
        {
            Console.WriteLine("Honor Roll: Yes");
        }
        else
        {
            Console.WriteLine("Honor Roll: No");
        }

        Console.WriteLine();
    }

    public void UpdateGPA(double GPA)
    {
        this.GPA = GPA;
    }

    public int GetStudentid()
    {
        return Studentid;
    }

    public bool HonorRoll()
    {
        if (GPA >= 3.5)
        {
            return true;
        }

        return false;
    }

    static void Main(string[] args)
    {
        List<Student> students = new List<Student>();

        Student student1 = new Student();
        Student student2 = new Student("Faiq", 101, 3.7);
        Student student3 = new Student("Ali", 102, 3.2);

        students.Add(student1);
        students.Add(student2);
        students.Add(student3);

        Console.WriteLine("All Students:");

        foreach (Student student in students)
        {
            student.Display();
        }

        Console.Write("Enter Student ID to update GPA: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter New GPA: ");
        double gpa = Convert.ToDouble(Console.ReadLine());

        UpdateStudentGPA(students, id, gpa);

        Console.WriteLine("\nStudents after GPA Update:");

        foreach (Student student in students)
        {
            student.Display();
        }
    }

    static void UpdateStudentGPA(List<Student> students, int id, double gpa)
    {
        foreach (Student student in students)
        {
            if (student.GetStudentid() == id)
            {
                student.UpdateGPA(gpa);
                Console.WriteLine("GPA Updated Successfully!");
                return;
            }
        }

        Console.WriteLine("Student not found!");
    }
}