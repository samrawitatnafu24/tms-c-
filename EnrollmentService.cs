// Legacy implementation — what the logging service did to the data
public class Enrollment
{
public string StudentId { get; set; } = string.Empty; 
public string CourseCode { get; set; } = string.Empty; 
public DateTime ProcessedAt { get; set; }
}
// Immutable by design — the logging pipeline cannot corrupt this
public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);

// Legacy Pre-C# 14 Implementation (Verbose)
public class Course
{
private int _capacity; // Manual backing field

public int Capacity
{
get => _capacity; set
{
if (value <= 0)
throw new ArgumentOutOfRangeException("Capacity must be positive.");
_capacity = value;
}
}
}



