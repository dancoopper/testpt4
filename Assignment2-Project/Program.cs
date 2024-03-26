using System;
using System.Collections.Generic;
//test3
public class Student
{
    public int StudentID { get; }
    public string Name { get; }
    public List<string> EnrolledClasses { get; }

    public Student(int id, string name)
    {
        StudentID = id;
        Name = name;
        EnrolledClasses = new List<string>();
    }

    public void Enroll(string className)
    {
        EnrolledClasses.Add(className);
    }
}

public class Professor
{
    public int ProfessorID { get; }
    public string Name { get; }
    public List<string> TeachesClasses { get; }

    public Professor(int id, string name)
    {
        ProfessorID = id;
        Name = name;
        TeachesClasses = new List<string>();
    }

    public void AddTeachesClass(string className)
    {
        TeachesClasses.Add(className);
    }
}

public class CollegeManagementSystem
{
    private List<Student> students;
    private List<Professor> professors;

    public CollegeManagementSystem()
    {
        students = new List<Student>();
        professors = new List<Professor>();
    }

    public void AddStudent(int id, string name)
    {
        students.Add(new Student(id, name));
    }

    public void AddProfessor(int id, string name)
    {
        professors.Add(new Professor(id, name));
    }

    public void EnrollStudentInClass(int studentID, string className)
    {
        Student student = students.Find(s => s.StudentID == studentID);
        if (student != null)
            student.Enroll(className);
        else
            Console.WriteLine("Student not found.");
    }

    public void ViewStudentsInClass(string className)
    {
        Console.WriteLine($"Students enrolled in {className}:");
        foreach (var student in students)
        {
            if (student.EnrolledClasses.Contains(className))
                Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}");
        }
    }

    public void ViewAllStudents()
    {
        Console.WriteLine("***** Students *****");
        foreach (var student in students)
            Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}");
    }

    public void ViewAllProfessors()
    {
        Console.WriteLine("***** Professors *****");
        foreach (var professor in professors)
            Console.WriteLine($"ID: {professor.ProfessorID}, Name: {professor.Name}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        CollegeManagementSystem cms = new CollegeManagementSystem();
        int choice;

        do
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. Add a new professor");
            Console.WriteLine("3. View all students");
            Console.WriteLine("4. View all professors");
            Console.WriteLine("5. Enroll a student in a class");
            Console.WriteLine("6. View students in a class");
            Console.WriteLine("7. Exit the program");
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter student ID: ");
                    int studentID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter student name: ");
                    string studentName = Console.ReadLine();
                    cms.AddStudent(studentID, studentName);
                    break;
                case 2:
                    Console.Write("Enter professor ID: ");
                    int professorID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter professor name: ");
                    string professorName = Console.ReadLine();
                    cms.AddProfessor(professorID, professorName);
                    break;
                case 3:
                    cms.ViewAllStudents();
                    break;
                case 4:
                    cms.ViewAllProfessors();
                    break;
                case 5:
                    Console.Write("Enter student ID: ");
                    int enrollStudentID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter class name to enroll: ");
                    string className = Console.ReadLine();
                    cms.EnrollStudentInClass(enrollStudentID, className);
                    break;
                case 6:
                    Console.Write("Enter class name to view students: ");
                    string classToView = Console.ReadLine();
                    cms.ViewStudentsInClass(classToView);
                    break;
                case 7:
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (choice != 7);
    }
}
