# College Management System

This program is a simple College Management System implemented in C#. It allows users to add and remove students and professors, enroll students and professors in classes, view all students, professors, and classes, and view students enrolled in a specific class.

## Prerequisites

To compile and run this program, you need:

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

## Compilation

1. Open a terminal or command prompt.
2. Navigate to the directory containing the `Program.cs` file.
3. Run the following command to compile the program:

   ```
   dotnet build
   ```

## Running the Program

Once the program is compiled, follow these steps to run it:

1. In the terminal or command prompt, navigate to the directory containing the compiled program.
2. Run the following command:

   ```
   dotnet run
   ```

3. The program will start running, and you'll see a menu with various options to interact with the College Management System.

## Usage

1. Choose options from the menu by entering the corresponding number and pressing Enter.
2. Follow the prompts to perform various actions such as adding students or professors, enrolling them in classes, viewing information, or exiting the program.

## Additional Notes

- You can adjust the maximum number of students and professors allowed in the system by modifying the arguments passed to the `CollegeManagementSystem` constructor in the `Main` method of `Program.cs`.
- The program makes use of exception handling to gracefully handle errors that may occur during runtime.
