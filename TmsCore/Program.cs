// // Console.WriteLine("Hello, World!");
// // This is how the legacy system declared region — no indication it could be empty string region = null; // ⚠Compiler warning CS8600 Console.WriteLine(region.ToUpper()); // ⚠Compiler warning CS8602

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


var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow); 
Console.WriteLine(enrollment);

// Try to mutate it — uncomment this line and see the compiler error:
// enrollment.CourseCode = "HACKED"; // ERROR: init-only property

// Non-destructive copy — creates a NEW record with one field changed
 
// var corrected = enrollment with { CourseCode = "CS-402" }; Console.WriteLine(corrected);

// // Value equality — two records with the same data are equal
// var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt); 
// Console.WriteLine($"Same data? {enrollment == duplicate}"); // True



var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 }; Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

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

var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; 
Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

// These should throw — try each one:
// new Student { Id = "S2", Name = "", Age = 20, GPA = 3.0m };

// new Student { Id = "S3", Name = "Test", Age = 12, GPA = 3.0m };

// new Student { Id = "S4", Name = "Test", Age = 20, GPA = 5.0m };


void PrintGradeReport(IEnumerable<IGradable> assessments)
{
Console.WriteLine("--- Grade Report ---");
foreach (var item in assessments)
{
Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
}
}

// Test it — one array holds two completely different types
IGradable[] cohortAssessments = [
new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 },
new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m }
];

PrintGradeReport(cohortAssessments);

var service = new EnrollmentService();

// Test 1: Valid registration
var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 }; var result = service.ProcessRegistration(validStudent, validCourse); Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

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
var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 }; 
fullCourse.EnrolledCount = 1;
try
{
service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
Console.WriteLine($"Business rule: {ex.Message}");
}
