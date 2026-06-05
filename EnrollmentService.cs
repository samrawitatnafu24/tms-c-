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





