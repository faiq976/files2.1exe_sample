using System;

class Student
{
    public string Studentid;
    public string Name;
    public string Department;
    public int Semester;
    public int MarksSubject1;
    public int MarksSubject2;
    public int MarksSubject3;
    public int MarksSubject4;
    public int MarksSubject5;
    public Student(string Studentid, string Name, string Department, int Semester, int MarksSubject1, int MarksSubject2, int MarksSubject3, int MarksSubject4, int MarksSubject5)
    {
        this.Studentid = Studentid;
        this.Name = Name;
        this.Department = Departmentment;
        this.Semester = Semester;
        this.MarksSubject1 = MarksSubject1;
        this.MarksSubject1 = MarksSubject2;
        this.MarksSubject1 = MarksSubject3;
        this.MarksSubject1 = MarksSubject4;
        this.MarksSubject1 = MarksSubject5;
    }
    public void TotalMarks(int MarksSubject1, int MarksSubject2, int MarksSubject3, int MarksSubject4, int MarksSubject5)
    {
        TotalMarks = MarksSubject1 + MarksSubject2 + MarksSubject3 + MarksSubject4 + MarksSubject5;
    }
    public void AverageMarks(int MarksSubject1, int MarksSubject2, int MarksSubject3, int MarksSubject4, int MarksSubject5)
    {
        AverageMarks = (MarksSubject1 + MarksSubject1 + MarksSubject1 + MarksSubject1 + MarksSubject1) / 5;
    }
    public void Grade()
    {
        return TotalMarks;
    }
    public void Display()
    {
        Console.WriteLine("Student ID:" + Studentid);
        Console.WriteLine("Name:" + Name);
        Console.WriteLine("Department:" + Department);
        Console.WriteLine("Semester:" + Semester);
        Console.WriteLine("Subject1 Marks:" + Markssubject1);
        Console.WriteLine("Subject2 Marks:" + Markssubject2);
        Console.WriteLine("Subject3 Marks:" + Markssubject3);
        Console.WriteLine("Subject4 Marks:" + Markssubject4);
        Console.WriteLine("Subject5 Marks:" + Markssubject5);
    }
    static void Main(string[] args)
    {
        Student student = new Student();
        student.TotalMarks(80, 85, 87, 92, 94);
        student.AverageMarks(80, 85, 87, 92, 94);
        student.Grade(A);
        student.Display();
    }

}