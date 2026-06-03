public class EnrollmentService 
{
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course) 
    {
        // TODO 1: Guard clauses (fail fast)
        if (student is null) 
        {
            throw new ArgumentNullException(nameof(student));
        }

        if (course is null) 
        {
            throw new ArgumentNullException(nameof(course));
        }

        if (course.Capacity <= 0) 
        {
            throw new InvalidOperationException("Course is full or invalid capacity.");
        }

        // TODO 2: Switch expression for academic standing
        string standing = student.GPA switch 
        {
          //  >= 3.5f => "Honors", // Assumes GPA is a float or double
          //  >= 2.5f => "Good Standing",
            _       => "Academic Warning"
        };

        Console.WriteLine($"{student.Name} is in {standing}.");

        // TODO 3: Return a new EnrollmentRecord
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}

