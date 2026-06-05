using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;



// Console.WriteLine("Hello, World!");

// EXERCISE 1
 Console.WriteLine("\n ============================================================================");
 Console.WriteLine(" EXERCISE 1: NULL HANDLING & STRING INTERPOLATION");
 Console.WriteLine(" ============================================================================");

// This is how the legacy system declared region — no indication it could be empty string region = null; // ⚠Compiler warning CS8600 
// Console.WriteLine(region.ToUpper()); // ⚠Compiler warning CS8602
// Declare the variable as nullable with '?'
// This tells the compiler: "I know this might be null. I accept responsibility."
string? region = null;

// Null-conditional operator '?.' — skip the call if null
// If region is null, ToUpper() never executes. No crash. string? upperRegion = region?.ToUpper(); Console.WriteLine($"Region (conditional): {upperRegion}");

// Null-coalescing operator '??' — provide a fallback value
// If region is null, use "Unassigned" instead.
string displayRegion = region ?? "Unassigned"; Console.WriteLine($"Region (coalesced): {displayRegion}");

// Null-coalescing assignment '??=' — assign only if currently null
// Useful for lazy initialization.
region ??= "Addis Ababa"; 
Console.WriteLine($"Region (assigned): {region}");
string studentName = "Abeba";
string studentId = "STU-001"; int enrollmentCount = 3;
decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
DateTime enrolledAt = DateTime.UtcNow; string? campusRegion = null;
Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Courses: {enrollmentCount}");
 Console.WriteLine($"Grant: {grantAmount:F2}");
 Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}"); 
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");

// EXERCISE 2
Console.WriteLine("\n ============================================================================");
 Console.WriteLine(" EXERCISE 2: FINANCIAL PRECISION");
Console.WriteLine(" ============================================================================");

// Legacy implementation — the bug that caused the audit failure
double grantPerStudent = 1999.99;
double totalAllocation = grantPerStudent * 100_000;

 Console.WriteLine($"Total allocated (double): {totalAllocation}");
 
// Fixed implementation — exact financial math
//decimal grantPerStudent = 1999.99m;
//decimal totalAllocation =  1999.99m * 100_000m;

Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

// EXERCISE 3
 Console.WriteLine("\n ============================================================================");
 Console.WriteLine(" EXERCISE 3: Pipeline & Encapsulation Testing");
 Console.WriteLine(" ============================================================================");

Console.WriteLine("--- Exercise 3 - Part 1: Record Immutability & Value Equality ---");

var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow); 
Console.WriteLine(enrollment);

var corrected = enrollment with { CourseCode = "CS-402" }; 
Console.WriteLine(corrected);

var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt); 
Console.WriteLine($"Same data? {enrollment == duplicate}"); // True


// EXERCISE 3 Part 2
Console.WriteLine("\n--- Exercise 3 - Part 2: Course Capacity & Title Field Validation ---");

var course = new CourseCode {Code = "CS-401", Title = "Advanced C#", Capacity = 30};
Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

try
{
    course.Capacity = -5;
}
catch(ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

try
{
    course.Title = "";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// EXERCISE 3 Part 3
Console.WriteLine("\n--- Exercise 3 - Part 3: Student Property Range Rules ---");

var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; 
Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

// These should throw — try each one:
// new Student { Id = "S2", Name = "", Age = 20, GPA = 3.0m };

// new Student { Id = "S3", Name = "Test", Age = 12, GPA = 3.0m };

// new Student { Id = "S4", Name = "Test", Age = 20, GPA = 5.0m };

// EXERCISE 3B: POLYMORPHIC INTERFACE TESTING
 Console.WriteLine("\n ============================================================================");
 Console.WriteLine(" Exercise 3B: Polymorphic Grade Report");
 Console.WriteLine(" ============================================================================");

void PrintGradeReport(IEnumerable<IGradable> assessments)
{
Console.WriteLine("--- Grade Report ---");
foreach (var item in assessments)
{
Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
}
}
IGradable[] cohortAssessments = [
new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 },
new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m }
];

PrintGradeReport(cohortAssessments);
