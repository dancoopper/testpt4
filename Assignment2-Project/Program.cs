using System;
using System.Collections.Generic;
using System.IO;

public class Student
{
    public int StudentID { get; }
    public string Name { get; }
    public string[] EnrolledClasses { get; }

    public Student(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("Student ID must be greater than zero.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Student name cannot be null or whitespace.", nameof(name));

        StudentID = id;
        Name = name;
        EnrolledClasses = new string[3]; // Assuming each student can enroll in at most 3 classes
    }

    public void Enroll(string className)
    {
        if (string.IsNullOrWhiteSpace(className.ToLower()))
            throw new ArgumentException("Class name cannot be null or whitespace.", nameof(className));

        for (int i = 0; i < EnrolledClasses.Length; i++)
        {
            if (EnrolledClasses[i] == null)
            {
                EnrolledClasses[i] = className.ToLower();
                return;
            }
        }
        throw new InvalidOperationException("Student cannot enroll in more classes.");
    }
    public override string ToString()
    {
        return $"ID: {StudentID}, Name: {Name}";
    }
}

public class Professor
{
    public int ProfessorID { get; }
    public string Name { get; }
    public string[] TeachesClasses { get; }

    public Professor(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("Professor ID must be greater than zero.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Professor name cannot be null or whitespace.", nameof(name));

        ProfessorID = id;
        Name = name;
        TeachesClasses = new string[3]; // Assuming each professor can teach at most 3 classes
    }

    public void AddTeachesClass(string className)
    {
        if (string.IsNullOrWhiteSpace(className))
            throw new ArgumentException("Class name cannot be null or whitespace.", nameof(className));

        for (int i = 0; i < TeachesClasses.Length; i++)
        {
            if (TeachesClasses[i] == null)
            {
                TeachesClasses[i] = className.ToLower();
                return;
            }
        }
        throw new InvalidOperationException("Professor cannot teach more classes.");
    }

    public override string ToString()
    {
        return $"ID: {ProfessorID}, Name: {Name}";
    }
}

public class CollegeManagementSystem
{
    private  Student[,] students;
    private Professor[,] professors;
    private int studentCount;
    private int professorCount;

    public CollegeManagementSystem(int maxStudents, int maxProfessors)
    {
        students = new Student[maxStudents, 3]; // Assuming each student can enroll in at most 3 classes
        professors = new Professor[maxProfessors, 3]; // Assuming each professor can teach at most 3 classes
        studentCount = 0;
        professorCount = 0;
    }

    public void AddStudent(int id, string name)
    {
        Console.Clear();
        try
        {
            if (studentCount < students.GetLength(0))
            {
                students[studentCount, 0] = new Student(id, name);
                Console.WriteLine("Student added successfully.");
                studentCount++;
            }
            else
            {
                Console.WriteLine("Maximum student limit reached.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error adding student: {ex.Message}");
        }
    }

    public void AddProfessor(int id, string name)
    {
        Console.Clear();
        try
        {
            if (professorCount < professors.GetLength(0))
            {
                professors[professorCount, 0] = new Professor(id, name);
                Console.WriteLine("Professor added successfully.");
                professorCount++;
            }
            else
            {
                Console.WriteLine("Maximum professor limit reached.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error adding professor: {ex.Message}");
        }
    }

    public void EnrollProfessorInClass(int professorID, string className)
    {
        Console.Clear();
        for (int i = 0; i < professorCount; i++)
        {
            if (professors[i, 0].ProfessorID == professorID)
            {
                try
                {
                    professors[i, 0].AddTeachesClass(className);
                    Console.WriteLine("Professor enrolled successfully.");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error enrolling professor: {ex.Message}");
                    return;
                }
            }
        }
        Console.WriteLine("Professor not found.");
    }

    public void EnrollStudentInClass(int studentID, string className)
    {
        Console.Clear();
        for (int i = 0; i < studentCount; i++)
        {
            if (students[i, 0].StudentID == studentID)
            {
                try
                {
                    students[i, 0].Enroll(className);
                    Console.WriteLine("Student enrolled successfully.");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error enrolling student: {ex.Message}");
                    return;
                }
            }
        }
        Console.WriteLine("Student not found.");
    }

    public void ViewAllClasses()
    {
        Console.Clear();
        Console.WriteLine("***** Classes *****");
        List<string> classes = new List<string>();

        for (int i = 0; i < studentCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (students[i, 0].EnrolledClasses[j] != null)
                {
                    if (!classes.Contains(students[i, 0].EnrolledClasses[j]))
                    {
                        classes.Add(students[i, 0].EnrolledClasses[j]);
                    }
                }
            }
        }

        for (int i = 0; i < professorCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (professors[i, 0].TeachesClasses[j] != null)
                {
                    if (!classes.Contains(professors[i, 0].TeachesClasses[j]))
                    {
                        classes.Add(professors[i, 0].TeachesClasses[j]);
                    }
                }
            }
        }

        if (classes.Count == 0)
        {
            Console.WriteLine("No classes found.");
            return;
        }
        else
        {
            foreach (var className in classes)
            {
                Console.WriteLine(className);
            }
        }
    }
    

    public void ViewStudentsInClass(string className)
    {
        Console.Clear();
        Console.WriteLine($"Students enrolled in {className}:");
        bool found = false;

        for (int i = 0; i < studentCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (students[i, 0].EnrolledClasses[j] == className)
                {
                    Console.WriteLine($"ID: {students[i, 0].StudentID}, Name: {students[i, 0].Name}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("No students enrolled in this class.");
        }
    }

    public void ViewAllStudents()
    {
        Console.Clear();
        Console.WriteLine("***** Students *****");

        if (studentCount == 0)
        {
            Console.WriteLine("No students found.");
        }
        else
        {
            for (int i = 0; i < studentCount; i++)
            {
                //Console.WriteLine($"ID: {students[i, 0].StudentID}, Name: {students[i, 0].Name}");
                Console.WriteLine($"{students[i, 0].ToString()}");
            }
        }
    }

    public void ViewAllProfessors()
    {
        Console.Clear();
        Console.WriteLine("***** Professors *****");

        if (professorCount == 0)
        {
            Console.WriteLine("No professors found.");
        }
        else
        {
            for (int i = 0; i < professorCount; i++)
            {
                Console.WriteLine($"{professors[i, 0].ToString()}");
            }
        }
    }



    public void RemoveStudent(int studentID)
    {
        for (int i = 0; i < studentCount; i++)
        {
            if (students[i, 0].StudentID == studentID)
            {
                for (int j = 0; j < 3; j++)
                {
                    students[i, 0].EnrolledClasses[j] = null;
                }
                for (int k = i; k < studentCount - 1; k++)
                {
                    students[k, 0] = students[k + 1, 0];
                }
                students[studentCount - 1, 0] = null;
                studentCount--;
                Console.WriteLine("Student removed successfully.");
                return;
            }
        }
        Console.WriteLine("Student not found.");
    }

    public void RemoveProfessor(int professorID)
    {
        for (int i = 0; i < professorCount; i++)
        {
            if (professors[i, 0].ProfessorID == professorID)
            {
                for (int j = 0; j < 3; j++)
                {
                    professors[i, 0].TeachesClasses[j] = null;
                }
                for (int k = i; k < professorCount - 1; k++)
                {
                    professors[k, 0] = professors[k + 1, 0];
                }
                professors[professorCount - 1, 0] = null;
                professorCount--;
                Console.WriteLine("Professor removed successfully.");
                return;
            }
        }
        Console.WriteLine("Professor not found.");
    }

    public void CheckStudent(int studentID, string studentName)
    {
        
        for (int i = 0; i < studentCount; i++)
        {
            if (students[i, 0].StudentID == studentID)
            {
                Console.Clear();
                Console.WriteLine("Student found.");
                return;
            }
            
        }
        this.AddStudent(studentID, studentName);
    }

    public void CheckProfessor(int professorID, string professorName)
    {
        
        for (int i = 0; i < professorCount; i++)
        {
            if (professors[i, 0].ProfessorID == professorID)
            {
                Console.Clear();
                Console.WriteLine("Professor found.");
                return;
            }
            
        }
        this.AddProfessor(professorID, professorName);
    }

    public bool ConfirmExit()
    {
        Console.WriteLine("Are you sure you want to exit? (Y/N)");
        try { 
            string confirmExit = Console.ReadLine();
            confirmExit = confirmExit.ToUpper();
            if (confirmExit == "Y")
            {
                Console.WriteLine("Exiting program...");
                
                Environment.Exit(0);
            }
            else if (confirmExit == "N")
            {
                Console.Clear();
                Console.WriteLine("Continuing...");
                
            }
            
        }
        catch (IOException ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return false;
    }
  
}

class Program
{
    static void Main(string[] args)
    {
        CollegeManagementSystem cms = new CollegeManagementSystem(100, 100); // Adjust as needed
        int choice = 0;

        do
        {
            Console.WriteLine("\n***** College Management System *****");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. Add a new professor");
            Console.WriteLine("3. Remove a student");
            Console.WriteLine("4. Remove a professor");
            Console.WriteLine("5. View all students");
            Console.WriteLine("6. View all professors");
            Console.WriteLine("7. Enroll a student in a class");
            Console.WriteLine("8. Enroll a professor in a class");
            Console.WriteLine("9. View students in a class");
            Console.WriteLine("10. View all classes");
            Console.WriteLine("0. Exit the program");
            Console.Write("Enter your choice: ");
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                choice =-2;
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }catch (IOException ex)
            {
                choice = -2;
                Console.Clear();
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                choice =-2;
                Console.Clear();
                Console.WriteLine("An error occurred: " + ex.Message);
            }


            try
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter student ID: ");
                        int studentID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter student name: ");
                        string studentName = Console.ReadLine();
                        cms.CheckStudent(studentID, studentName.ToLower());
                        
                        break;
                    case 2:
                        Console.Write("Enter professor ID: ");
                        int professorID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter professor name: ");
                        string professorName = Console.ReadLine();
                        cms.CheckProfessor(professorID, professorName.ToLower());
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
                        cms.ViewAllStudents();
                        Console.Write("Enter student ID: ");
                        int enrollStudentID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter class name to enroll: ");
                        string className = Console.ReadLine();
                        cms.EnrollStudentInClass(enrollStudentID, className.ToLower());
                        break;
                    case 8:
                        cms.ViewAllProfessors();
                        Console.Write("Enter professor ID: ");
                        int enrollProfessorID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter class name to enroll: ");
                        string classToEnroll = Console.ReadLine();
                        cms.EnrollProfessorInClass(enrollProfessorID, classToEnroll.ToLower());
                        break;
                    case 9:
                        cms.ViewAllClasses();
                        Console.Write("Enter class name to view students: ");
                        string classToView = Console.ReadLine();
                        cms.ViewStudentsInClass(classToView.ToLower());
                        break;
                    case 10:
                        cms.ViewAllClasses();
                        break;
                    case 0:
                        bool YN = cms.ConfirmExit();
                        if (YN == false)
                        {
                            continue;
                        }
                       
                        break;
                    default:
                        //Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (IOException ex)
            {
                Console.Clear();
                Console.WriteLine("1An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("2An error occurred: " + ex.Message);
            }

        } while (choice != -1);

    }
}
