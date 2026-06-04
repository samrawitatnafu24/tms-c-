// Legacy implementation — what the logging service did to the data
public class Enrollment
{
public string StudentId { get; set; } = string.Empty; 
public string CourseCode { get; set; } = string.Empty; 
public DateTime ProcessedAt { get; set; }
}
// Immutable by design — the logging pipeline cannot corrupt this
public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);




