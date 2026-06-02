// // Console.WriteLine("Hello, World!");
// // This is how the legacy system declared region — no indication it could be empty
//  string region = null; // ⚠Compiler warning CS8600 
// Console.WriteLine(region.ToUpper()); // ⚠Compiler warning CS8602

// // Declare the variable as nullable with '?'
// // This tells the compiler: "I know this might be null. I accept responsibility."
// string? region = null;

// // Null-conditional operator '?.' — skip the call if null
// // If region is null, ToUpper() never executes. No crash. string? upperRegion = region?.ToUpper(); Console.WriteLine($"Region (conditional): {upperRegion}");
// string? upperRegion = region?.ToUpper(); Console.WriteLine($"Region (conditional): {upperRegion}");

// // Null-coalescing operator '??' — provide a fallback value
// // If region is null, use "Unassigned" instead.
// string displayRegion = region ?? "Unassigned"; Console.WriteLine($"Region (coalesced): {displayRegion}");

// // Null-coalescing assignment '??=' — assign only if currently null
// // Useful for lazy initialization.
// region ??= "Addis Ababa"; Console.WriteLine($"Region (assigned): {region}");
// string studentName = "Abeba";
// string studentId = "STU-001"; int enrollmentCount = 3;
// decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
// DateTime enrolledAt = DateTime.UtcNow; 
// string? campusRegion = null;

// Console.WriteLine($"Student: {studentName} ({studentId})");

// Console.WriteLine($"Courses: {enrollmentCount}"); 
// Console.WriteLine($"Grant: {grantAmount:F2}"); 
// Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}"); 
// Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");
// // Legacy implementation — the bug that caused the audit failure
// double grantPerStudent = 1999.99;
// double totalAllocation = grantPerStudent * 100_000; 
// Console.WriteLine($"Total allocated (double): {totalAllocation}");

// Fixed implementation — exact financial math
// decimal grantPerStudent = 1999.99m;
// decimal totalAllocation = grantPerStudent * 100_000m;

// Console.WriteLine($"Total allocated (decimal): {totalAllocation}"); 
// Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

using System;
using System.Linq;
using System.Collections.Generic;

internal static partial class Program
{
    private static void Main()
    {
        var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
        Console.WriteLine(enrollment);

        // Try to mutate it — uncomment this line and see the compiler error:
        // enrollment.CourseCode = "HACKED"; // ERROR: init-only property

        // Non-destructive copy — creates a NEW record with one field changed
        // var corrected = enrollment with { CourseCode = "CS-402" }; Console.WriteLine(corrected);

        // Value equality — two records with the same data are equal
        // var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt); 
        // Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

        var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
        Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

        // Invalid capacity — should throw
        try
        {
            course.Capacity = -5;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");
        }

        // Invalid title — should throw
        try
        {
            course.Title = "";
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Caught: {ex.Message}");
        }

        // C# 12+ Collection Expressions the modern way to initialize lists
        List<Student> students = [
        new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m }, 
        new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m }, 
        new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
         new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
          new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
           new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
            new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
             new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
        ];
        // Build leaderboard: honors students (GPA >= 3.5), ordered by GPA desc, projected to names
        var leaderboard = students
            .Where(s => s.GPA >= 3.5m)
            .OrderByDescending(s => s.GPA)
            .Select(s => s.Name)
            .ToList();

        Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
        foreach (var name in leaderboard)
        {
            Console.WriteLine($"- {name}");
        }

        // Average GPA
        decimal averageGpa = students.Average(s => s.GPA);
        Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

        // Group by academic standing
        var standingGroups = students.GroupBy(s => s.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "Good Standing",
            >= 2.0m => "Probation",
            _ => "Academic Warning"
        });

        Console.WriteLine("\n--- Academic Standing Report ---");
        foreach (var group in standingGroups)
        {
            Console.WriteLine($"\n{group.Key} ({group.Count()}):");
            foreach (var s in group)
            {
                Console.WriteLine($" {s.Name} GPA: {s.GPA}");
            }
        }

        // Collection expressions with spread (C# 12+)
        string[] backendCourses = ["C#", "ASP.NET Core"]; 
        string[] frontendCourses = ["TypeScript", "Angular"]; 
        string[] allCourses = [..backendCourses, ..frontendCourses];
        Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}");
    }
}



