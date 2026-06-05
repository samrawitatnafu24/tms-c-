// EXERCISE 3
public record  EnrollmentRecord(string StudentID, string CourseCode, DateTime EnrolledAt);

public class  CourseCode
{
public required string Code { get; init; }
public required string Title
{
   get;
   set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException("Title cannot be empty or whitespace.", nameof(value));
}

public int Capacity
{
         get;
         set => field = value > 0
             ? value
             : throw new ArgumentOutOfRangeException(nameof(value), "Capacity must be greater than zero.");
}

public int EnrolledCount { get; set; }
}

public class Student
{
public required string Id { get; init; }
public required string Name
{
   get;
   set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));

}

public int Age
{
get;
set => field = value is >= 16 and <= 100
? value
: throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 a nd 100.");
}

public decimal GPA
{
get;
set => field = value is >= 0.0m and <= 4.0m
? value
: throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0 and 4.0.");
}
}
