using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TmsCore;



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

// EXERCISE 4: GUARD CLAUSES & PATTERN MATCHING VALIDATION
 Console.WriteLine("\n ============================================================================");
 Console.WriteLine(" EXERCISE 4: ENROLLMENT VALIDATION & GUARDS");
 Console.WriteLine(" ============================================================================");

 var service = new EnrollmentService();


// Test 1: Valid registration
var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; 
var validCourse = new CourseCode { Code = "CS-401", Title = "Advanced C#", Capacity = 30 }; 
var result = service.ProcessRegistration(validStudent, validCourse);
 Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

// Test 2: Null student should throw
try
{
service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
 
{
Console.WriteLine($"Guard caught: {ex.ParamName}");
}

// Test 3: Full course should throw
var fullCourse = new CourseCode { Code = "CS-402", Title = "Full Course", Capacity = 1 }; 
fullCourse.EnrolledCount = 1;
try
{
service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
Console.WriteLine($"Business rule: {ex.Message}");
}


// EXERCISE 5: COLLECTIONS & LINQ PIPELINES
Console.WriteLine("\n ============================================================================");
Console.WriteLine(" EXERCISE 5: FACULTY ANALYTICS DASHBOARD");
Console.WriteLine(" ============================================================================");

// Step 1: Initialize list using C# 12+ Collection Expressions [cite: 405, 407]
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

// Step 2: Build the Honors Leaderboard using chained LINQ extensions [cite: 410, 411]
List<string> leaderboard = students
.Where(s => s.GPA >= 3.5m)
.OrderByDescending(s => s.GPA)
.Select(s => s.Name)
.ToList();
    
Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
foreach (var name in leaderboard)
{
Console.WriteLine($"- {name}");
}

// Step 3: Class Average [cite: 426]
decimal averageGpa = students.Average(s => s.GPA);
Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

// Step 4: Group by Academic Standing using Switch Logic [cite: 432, 433]
var standingGroups = students.GroupBy(s => s.GPA switch
{
    >= 3.5m => "Honors", 
    >= 2.5m => "Good Standing", 
    >= 2.0m => "Probation",
_   => "Academic Warning"
});

Console.WriteLine("\n--- Academic Standing Report ---");
foreach (var group in standingGroups)
{
Console.WriteLine($"\n{group.Key} ({group.Count()}):");
foreach (var student in group)
    {
    Console.WriteLine($"  {student.Name} GPA: {student.GPA}");
    }
}

// Step 5: Collection Expressions with Spread Operator (..)
string[] backendCourses = ["C#", "ASP.NET Core"];
string[] frontendCourses = ["TypeScript", "Angular"];
string[] allCourses = [.. backendCourses, .. frontendCourses, "Capstone"]; 

Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}");


// EXERCISE 6 - Async and Resilience
Console.WriteLine("\n=====================================================================");
Console.WriteLine("Exercise 6: Async Programming & Resilience");
Console.WriteLine("=====================================================================");

// EXERCISE 6 - STEP 1: SEE THREAD STARVATION IN NUMBERS
Console.WriteLine("--- Exercise 6 Step 1: Thread Pool Latency Numbers ---");

var enrollService = new EnrollmentService();
var sw = Stopwatch.StartNew();

// 1. The Wrong Way: Blocking Sequential
for (int i = 0; i < 5; i++)
{
    Thread.Sleep(300); 
}
Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms");

// 2. Async But Still Sequential
sw.Restart();
for (int i = 0; i < 5; i++)
{
    await Task.Delay(300);
}
Console.WriteLine($"Async sequential:    {sw.ElapsedMilliseconds}ms");

// 3. The Right Way: Async Parallel
sw.Restart();
var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300));
await Task.WhenAll(tasks);
Console.WriteLine($"Async parallel:      {sw.ElapsedMilliseconds}ms");
