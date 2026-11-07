using System;
using System.Collections.Generic;
using System.Linq;

// Record Type - Positional Syntax, Immutable Data, Value-Based Equality
record Student(string Name, string Major, double Gpa);

// Interface Abstraction - A Contract With No Implementation
interface IReportable
{
    string GetReport();
}

// Pattern Matching - Switch Expression With Property and Relational Patterns
static class HonorsRules
{
    public static string For(Student s) => s switch
    {
        { Gpa: >= 3.8 } => "Summa Cum Laude",
        { Gpa: >= 3.5 } => "Magna Cum Laude",
        { Gpa: >= 3.2 } => "Cum Laude",
        _ => "No honors"   // _ means everything else
    };
}

// Encapsulation - private readonly Field, Public Method
class AverageReport : IReportable
{
    private readonly List<Student> _students;

    public AverageReport(List<Student> students)
    {
        _students = students;
    }

    public string GetReport()
    {
        // LINQ - Average() With a Lambda
        double avg = _students.Average(s => s.Gpa);
        return $"Average report: {_students.Count} students, average GPA {avg:F2}";
    }
}

// A Second Implementation of the Same Interface (Polymorphism)
class HonorsReport : IReportable
{
    private readonly List<Student> _students;

    public HonorsReport(List<Student> students)
    {
        _students = students;
    }

    public string GetReport()
    {
        int honorsCount = _students.Count(s => HonorsRules.For(s) != "No honors");
        return $"Honors report: {honorsCount} of {_students.Count} students earned honors";
    }
}

class Program
{
    static void Main()
    {
        // Strong Static Typing - List<Student> Is a Reference Type on the Heap
        List<Student> students = new List<Student>
        {
            new Student("Ana Torres",   "Computer Science", 3.9),
            new Student("Luis Garza",   "Computer Science", 3.4),
            new Student("Maria Lopez",  "Math",             3.7),
            new Student("Carlos Ruiz",  "Math",             2.8),
            new Student("Sofia Reyes",  "Biology",          3.2),
            new Student("Diego Cruz",   "Biology",          3.0)
        };

        Console.WriteLine("=============================================");
        Console.WriteLine("          C# Student Report Program          ");
        Console.WriteLine("=============================================");

        // 1. Records - Printing the Roster
        Console.WriteLine("\n--- 1. Roster (record type) ---");
        foreach (Student s in students)
        {
            Console.WriteLine($"{s.Name,-14} {s.Major,-18} {s.Gpa:F1}");
        }

        // Records - Value-Based Equality (Classes Would Print False Here)
        var a = new Student("Ana Torres", "Computer Science", 3.9);
        var b = new Student("Ana Torres", "Computer Science", 3.9);
        Console.WriteLine($"\na == b ? {a == b}");

        // Records - Immutability: Make a Copy With One Property Changed
        var updated = a with { Gpa = 4.0 };
        Console.WriteLine($"Original: {a.Gpa}  Copy: {updated.Gpa}");

        // 2. LINQ - Where and OrderByDescending
        Console.WriteLine("\n--- 2. LINQ: GPA of 3.5 or higher, ranked ---");
        var topStudents = students
            .Where(s => s.Gpa >= 3.5)
            .OrderByDescending(s => s.Gpa);

        foreach (Student s in topStudents)
        {
            Console.WriteLine($"{s.Name,-14} {s.Gpa:F1}");
        }

        // 3. LINQ - GroupBy, Select, Average
        Console.WriteLine("\n--- 3. LINQ: Average GPA by major ---");
        var byMajor = students
            .GroupBy(s => s.Major)
            .Select(g => new { Major = g.Key, Count = g.Count(), Avg = g.Average(s => s.Gpa) });

        foreach (var g in byMajor)
        {
            Console.WriteLine($"{g.Major,-18} {g.Count} students, average {g.Avg:F2}");
        }

        // 4. Pattern Matching - Honors Label for Each Student
        Console.WriteLine("\n--- 4. Pattern matching: honors ---");
        foreach (Student s in students)
        {
            Console.WriteLine($"{s.Name,-14} {s.Gpa:F1}  ->  {HonorsRules.For(s)}");
        }

        // 5. Interface and Polymorphism - One List, Two Implementations
        Console.WriteLine("\n--- 5. Interface: IReportable ---");
        List<IReportable> reports = new List<IReportable>
        {
            new AverageReport(students),
            new HonorsReport(students)
        };

        foreach (IReportable report in reports)
        {
            // Same Call, Different Behavior Depending on the Object
            Console.WriteLine(report.GetReport());
        }
    }
}
