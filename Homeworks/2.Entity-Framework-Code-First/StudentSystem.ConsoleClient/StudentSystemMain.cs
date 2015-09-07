using System;
using System.Linq;
using StudentSystem.Data;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace StudentSystem.ConsoleClient
{
    public class StudentSystemMain
    {
        static void Main()
        {
            var context = new StudentSystemContext();

            var testQuery = context.Students.Count();

            //1.ListAllStudentsAndTheirHomeworks
            var allStudents = context.Students
                .Select(s => new
                {
                    s.Name,
                    HomeworkSubmissions = s.Homeworks
                        .Select(h => new
                        {
                            h.Content,
                            h.ContentType
                        })
                }).ToList();

            foreach (var student in allStudents)
            {
                Console.WriteLine("Student name: " + student.Name);
                foreach (var homework in student.HomeworkSubmissions)
                {
                    Console.WriteLine("Homework: " + homework.Content);
                    Console.WriteLine("Homework Type: " + homework.ContentType);
                }
                Console.WriteLine();
            }

            //2.AllCoursesAndTheirResources
            var allCourses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    c.Resources
                })
                .ToList();

            foreach (var course in allCourses)
            {
                Console.WriteLine("Course name: " + course.Name);
                string descript = course.Description ?? "no description";
                Console.WriteLine("Course Description: " + descript);
                if (course.Resources.Count() == 0)
                {
                    Console.WriteLine("No Resources for this course !");
                    Console.WriteLine();
                }
                else
                {
                    foreach (var resource in course.Resources)
                    {
                        Console.WriteLine("Resource name: " + resource.Name);
                        Console.WriteLine("Resource type: " + resource.ResourceType);
                        Console.WriteLine("Resource URL: " + resource.Url);
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }

            //3.AllCourseWithMoreThan2Resources
            var coursesWithResources = context.Courses
                .Where(c => c.Resources.Count > 2)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    ResourcesCount = c.Resources.Count
                })
                .ToList();

            foreach (var course in coursesWithResources)
            {
                Console.WriteLine("Course name: {0} -- Resources count {1}", course.Name, course.ResourcesCount);
            }

            //4.ActiveCoursesOnDate
            var givenDate = DateTime.ParseExact("20/10/2008", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var coursesActive = context.Courses
                .Where(c => c.StartDate <= givenDate && c.EndDate >= givenDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = EntityFunctions.DiffDays(c.StartDate, c.EndDate),
                    StudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsEnrolled)
                .ThenByDescending(c => c.Duration);

            foreach (var course in coursesActive)
            {
                Console.WriteLine("Course name: " + course.Name);
                Console.WriteLine("Start date: " + String.Format("{0:d/M/yyyy}", course.StartDate));
                Console.WriteLine("End date: " + String.Format("{0:d/M/yyyy}", course.EndDate));
                Console.WriteLine("Course duration: {0} days", course.Duration);
                Console.WriteLine("Students enrolled: " + course.StudentsEnrolled);
                Console.WriteLine();
            }

            //5.StudentAndCoursesEnrolled
            var studentsEnrolled = context.Students
                .Select(s => new
                {
                    s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Price),
                    AveragePrice = s.Courses.Average(c => c.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name);

            foreach (var student in studentsEnrolled)
            {
                Console.WriteLine(" Student name: {0}\n Number of courses: {1}\n Total price of courses: {2}\n Average price of courses: {3}",
                    student.Name, student.NumberOfCourses, student.TotalPrice, student.AveragePrice);
                Console.WriteLine();
            }
        }
    }
}
