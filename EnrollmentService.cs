using System;
using System.Threading.Tasks;
namespace TmsCore;

//EXERCISE 3

public class Enrollment
{
public string StudentId { get; set; } = string.Empty; 
public string CourseCode { get; set; } = string.Empty; 
public DateTime ProcessedAt { get; set; }
}
public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);

public class EnrollmentService
{
   public EnrollmentRecord ProcessRegistration(Student? student, CourseCode? course)
    {
        // TODO 1: Guard clauses - Fail fast [cite: 324]
        if (student is null) 
            throw new ArgumentNullException(nameof(student));

        if (course is null) 
            throw new ArgumentNullException(nameof(course));

        // Check if the course capacity is full, zero, or negative [cite: 324, 326]
        if (course.Capacity <= 0 || course.EnrolledCount >= course.Capacity)
            throw new InvalidOperationException("Business rule violation: The selected course is full or unavailable.");

        // TODO 2: Switch expression pattern to classify academic standing [cite: 330]
        string standing = student.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "Good Standing",
            _ => "Academic Warning" // Fallback arm [cite: 336, 395]
        };

        Console.WriteLine($"{student.Name} is in {standing} standing.");

        // TODO 3: Return immutable checkpoint tracking instance [cite: 342]
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow); 
    }


public async Task SendConfirmationAsync(Student student)
{
try
{
await Task.Delay(100); // Simulate sending email
Console.WriteLine($" Email sent to {student.Name}");
}
catch (Exception ex)
{
// Log the failure do NOT re-throw.
// This is intentional fire-and-forget.
Console.WriteLine($" Email failed for {student.Name}: {ex.Message}");
}
}
}