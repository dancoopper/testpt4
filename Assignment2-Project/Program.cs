using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; }
    public string Name { get; }
    public List<string> EnrolledClasses { get; }

    public Student(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("Student ID must be greater than zero.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Student name cannot be null or whitespace.", nameof(name));

        StudentID = id;
        Name = name;
        EnrolledClasses = new List<string>();
    }

    public void Enroll(string className)
    {
        if (string.IsNullOrWhiteSpace(className.ToLower()))
            throw new ArgumentException("Class name cannot be null or whitespace.", nameof(className));

        EnrolledClasses.Add(className.ToLower());
    }
}

public class Professor
{
    public int ProfessorID { get; }
    public string Name { get; }
    public List<string> TeachesClasses { get; }

    public Professor(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("Professor ID must be greater than zero.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Professor name cannot be null or whitespace.", nameof(name));

        ProfessorID = id;
        Name = name;
        TeachesClasses = new List<string>();
    }

    public void AddTeachesClass(string className)
    {
        if (string.IsNullOrWhiteSpace(className))
            throw new ArgumentException("Class name cannot be null or whitespace.", nameof(className));

        TeachesClasses.Add(className);
    }
}

public class CollegeManagementSystem
{
    private readonly List<Student> students;
    private readonly List<Professor> professors;

    public CollegeManagementSystem()
    {
        students = new List<Student>();
        professors = new List<Professor>();
    }

    public void AddStudent(int id, string name)
    {
        try
        {
            var student = new Student(id, name);
            students.Add(student);
            Console.WriteLine("Student added successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error adding student: {ex.Message}");
        }
    }

    public void AddProfessor(int id, string name)
    {
        try
        {
            var professor = new Professor(id, name);
            professors.Add(professor);
            Console.WriteLine("Professor added successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error adding professor: {ex.Message}");
        }
    }

    public void EnrollStudentInClass(int studentID, string className)
    {
        Student student = students.Find(s => s.StudentID == studentID);
        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        try
        {
            student.Enroll(className);
            Console.WriteLine("Student enrolled successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error enrolling student: {ex.Message}");
        }
    }

    public void ViewStudentsInClass(string className)
    {
        Console.WriteLine($"Students enrolled in {className}:");
        bool found = false;

        foreach (var student in students)
        {
            if (student.EnrolledClasses.Contains(className))
            {
                Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No students enrolled in this class.");
        }
    }

    public void ViewAllStudents()
    {
        Console.WriteLine("***** Students *****");

        if (students.Count == 0)
        {
            Console.WriteLine("No students found.");
        }
        else
        {
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}");
            }
        }
    }

    public void ViewAllProfessors()
    {
        Console.WriteLine("***** Professors *****");

        if (professors.Count == 0)
        {
            Console.WriteLine("No professors found.");
        }
        else
        {
            foreach (var professor in professors)
            {
                Console.WriteLine($"ID: {professor.ProfessorID}, Name: {professor.Name}");
            }
        }
    }

    public void RemoveStudent(int studentID)
    {
        var student = students.Find(s => s.StudentID == studentID);
        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine($"Student ID: {student.StudentID}, Name: {student.Name}");
        Console.Write("Are you sure you want to remove this student? (Y/N): ");
        char confirmRemove = char.ToUpper(Console.ReadKey().KeyChar);

        if (confirmRemove != 'Y')
        {
            Console.WriteLine("Student removal canceled.");
            return;
        }

        students.Remove(student);
        Console.WriteLine("Student removed successfully.");
    }

    public void RemoveProfessor(int professorID)
    {
        var professor = professors.Find(p => p.ProfessorID == professorID);
        if (professor == null)
        {
            Console.WriteLine("Professor not found.");
            return;
        }

        Console.WriteLine($"Professor ID: {professor.ProfessorID}, Name: {professor.Name}");
        Console.Write("Are you sure you want to remove this professor? (Y/N): ");
        char confirmRemove = char.ToUpper(Console.ReadKey().KeyChar);

        if (confirmRemove != 'Y')
        {
            Console.WriteLine("Professor removal canceled.");
            return;
        }

        professors.Remove(professor);
        Console.WriteLine("Professor removed successfully.");
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
            //Console.Clear();

            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. Add a new professor");
            Console.WriteLine("3. Remove a student");
            Console.WriteLine("4. Remove a professor");
            Console.WriteLine("5. View all students");
            Console.WriteLine("6. View all professors");
            Console.WriteLine("7. Enroll a student in a class");
            Console.WriteLine("8. View students in a class");
            Console.WriteLine("9. Exit the program");
            Console.Write("Enter your choice: ");

            choice = Convert.ToInt32(Console.ReadLine());

            try
            {
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
                        Console.Write("Enter student ID to remove: ");
                        int removeStudentID = Convert.ToInt32(Console.ReadLine());
                        cms.RemoveStudent(removeStudentID);
                        break;
                    case 4:
                        Console.Write("Enter professor ID to remove: ");
                        int removeProfessorID = Convert.ToInt32(Console.ReadLine());
                        cms.RemoveProfessor(removeProfessorID);
                        break;
                    case 5:
                        cms.ViewAllStudents();
                        break;
                    case 6:
                        cms.ViewAllProfessors();
                        break;
                    case 7:
                        Console.Write("Enter student ID: ");
                        int enrollStudentID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter class name to enroll: ");
                        string className = Console.ReadLine();
                        cms.EnrollStudentInClass(enrollStudentID, className.ToLower());
                        break;
                    case 8:
                        Console.Write("Enter class name to view students: ");
                        string classToView = Console.ReadLine();
                        cms.ViewStudentsInClass(classToView.ToLower());
                        break;
                    case 9:
                        Console.WriteLine("Are you sure you want to exit? (Y/N)");
                        char confirmExit = char.ToUpper(Console.ReadKey().KeyChar);
                        if (confirmExit == 'Y')
                        {
                            Console.WriteLine("Exiting program...");
                            choice = 9;
                        }
                        else
                        {
                            Console.WriteLine("Continuing...");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

        } while (choice != 9);

    }
}
