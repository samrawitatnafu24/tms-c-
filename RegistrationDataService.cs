using System;
using System.Threading.Tasks;

namespace TmsCore;

public class RegistrationDataService
{
    public async Task<Student> FetchStudentAsync(string id)
    {
        Console.WriteLine($"  Fetching {id}...");
        await Task.Delay(300); // Simulate database network latency [cite: 58]
        
        return new Student
        {
            Id = id,
            Name = $"Student-{id}",
            Age = 20,
            GPA = id switch
            {
                "S1" => 3.8m,
                "S2" => 2.4m,
                "S3" => 3.5m,
                "S4" => 1.9m,
                "S5" => 3.2m,
                _    => 2.5m
            }
        };
    }

    public async Task<CourseCode> FetchCourseAsync(string code)
    {
        Console.WriteLine($"  Fetching course {code}...");
        await Task.Delay(200); 
        
        return new CourseCode
        {
            Code = code,
            Title = $"Course-{code}", 
            Capacity = code switch 
            {
                "CRS-101" => 2,
                "CRS-201" => 30,
                "CRS-301" => 15,
                _         => 25
            }
        };
    }
}
