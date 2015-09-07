namespace StudentSystem.Data.Migrations
{
    using StudentSystem.Models;
    using StudentSystem.Models.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using System.Globalization;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystemContext context)
        {
            if (context.Courses.Count() == 0)
            {
                SeedCourses(context);
            }

            if (context.Resources.Count() == 0)
            {
                SeedResources(context);
            }

            if (context.Students.Count() == 0)
            {
                SeedStudents(context);
            }

            if (context.Homeworks.Count() == 0)
            {
                SeedHomeworks(context);
            }
        }

        private void SeedCourses(StudentSystemContext context)
        {
            var startDate1 = DateTime.ParseExact("15/03/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate1 = DateTime.ParseExact("16/04/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate2 = DateTime.ParseExact("01/10/2008", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate2 = DateTime.ParseExact("07/11/2008", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate3 = DateTime.ParseExact("22/09/2008", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate3 = DateTime.ParseExact("25/10/2008", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate4 = DateTime.ParseExact("13/01/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate4 = DateTime.ParseExact("13/03/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate5 = DateTime.ParseExact("29/05/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate5 = DateTime.ParseExact("30/06/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var startDate6 = DateTime.ParseExact("08/12/2011", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var endDate6 = DateTime.ParseExact("09/01/2012", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            context.Courses.Add(new Course()
            {
                Name = "JavaScript",
                Description = "Introduction to JavaScript",
                StartDate = startDate1,
                EndDate = endDate1,
                Price = 210.0m
            });

            context.SaveChanges();

            context.Courses.Add(new Course()
            {
                Name = "PHP Web",
                StartDate = startDate2,
                EndDate = endDate2,
                Price = 115.45m
            });

            context.SaveChanges();

            context.Courses.Add(new Course()
            {
                Name = "Web Services",
                Description = "Web Services And Cloud",
                StartDate = startDate3,
                EndDate = endDate3,
                Price = 89.0m
            });

            context.SaveChanges();

            context.Courses.Add(new Course()
            {
                Name = "C# Basics",
                StartDate = startDate4,
                EndDate = endDate4,
                Price = 150.0m
            });

            context.SaveChanges();

            context.Courses.Add(new Course()
            {
                Name = "ASP.NET",
                StartDate = startDate5,
                EndDate = endDate5,
                Price = 480.0m
            });

            context.SaveChanges();

            context.Courses.Add(new Course()
            {
                Name = "Web Development",
                Description = "First Steps in Creating Sites",
                StartDate = startDate6,
                EndDate = endDate6,
                Price = 70.67m
            });

            context.SaveChanges();

        }

        private void SeedResources(StudentSystemContext context)
        {
            var type1 = (ResourceType)int.Parse("1");
            var course1 = context.Courses.Find(1);

            var type2 = (ResourceType)int.Parse("2");
            var course2 = context.Courses.Find(2);

            var type3 = (ResourceType)int.Parse("3");
            var course3 = context.Courses.Find(3);

            var type4 = (ResourceType)int.Parse("4");
            var course4 = context.Courses.Find(4);

            var course5 = context.Courses.Find(5);
            var course6 = context.Courses.Find(6);

            var resource1 = new Resource()
            {
                Name = "Diagrams",
                ResourceType = type1,
                Url = "http://www.foo.com/bar.html",
                Course = course1
            };

            context.Resources.Add(resource1);
            context.SaveChanges();

            var resource2 = new Resource()
            {
                Name = "How to learn programming",
                ResourceType = type2,
                Url = "http://www.hack.bg",
                Course = course2
            };

            context.Resources.Add(resource2);
            context.SaveChanges();

            var resource3 = new Resource()
            {
                Name = "Solving Problems",
                ResourceType = type3,
                Url = "http://www.foo.com/bar.html",
                Course = course3
            };

            context.Resources.Add(resource3);
            context.SaveChanges();

            var resource4 = new Resource()
            {
                Name = "Detailed Lecture for OOP",
                ResourceType = type4,
                Url = "http://www.softuni.bg/profile.html",
                Course = course3
            };

            context.Resources.Add(resource4);
            context.SaveChanges();

            var resource5 = new Resource()
            {
                Name = "Conferences",
                ResourceType = type4,
                Url = "http://www.softuni.bg/conf.html",
                Course = course3
            };

            context.Resources.Add(resource5);
            context.SaveChanges();

            var resource6 = new Resource()
            {
                Name = "Pictures",
                ResourceType = type3,
                Url = "http://www.google.com/",
                Course = course5
            };

            context.Resources.Add(resource6);
            context.SaveChanges();

            var resource7 = new Resource()
            {
                Name = "Presentaions",
                ResourceType = type2,
                Url = "http://www.foo.com/",
                Course = course1
            };

            context.Resources.Add(resource7);
            context.SaveChanges();
        }

        private void SeedStudents(StudentSystemContext context)
        {
            var register1 = DateTime.ParseExact("15/03/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var birthday1 = DateTime.ParseExact("01/10/1992", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var courses1 = new HashSet<Course>();
            courses1.Add(context.Courses.Find(2));
            courses1.Add(context.Courses.Find(3));
            courses1.Add(context.Courses.Find(6));

            var register2 = DateTime.ParseExact("16/04/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var courses2 = new HashSet<Course>();
            courses2.Add(context.Courses.Find(3));
            courses2.Add(context.Courses.Find(1));
            courses2.Add(context.Courses.Find(4));
            courses2.Add(context.Courses.Find(5));
            courses2.Add(context.Courses.Find(6));

            var register3 = DateTime.ParseExact("22/07/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var courses3 = new HashSet<Course>();
            courses3.Add(context.Courses.Find(2));

            var register4 = DateTime.ParseExact("02/03/2015", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var student1 = new Student()
            {
                Name = "Gosho",
                PhoneNumber = "092-678123",
                RegisteredOn = register1,
                BirthDay = birthday1,
                Courses = courses1
            };
            context.Students.Add(student1);
            context.SaveChanges();

            var student2 = new Student()
            {
                Name = "Pesho",
                RegisteredOn = register2,
                Courses = courses2
            };

            context.Students.Add(student2);
            context.SaveChanges();

            var student3 = new Student()
            {
                Name = "Daniela",
                PhoneNumber = "0993412378",
                RegisteredOn = register3,
                Courses = courses3
            };

            context.Students.Add(student3);
            context.SaveChanges();

            var student4 = new Student()
            {
                Name = "Petrakiev",
                PhoneNumber = "0134953286",
                RegisteredOn = register4,
                Courses = courses3
            };

            context.Students.Add(student4);
            context.SaveChanges();
        }


        private void SeedHomeworks(StudentSystemContext context)
        {
            var type1 = (ContentType)int.Parse("0");
            var date1 = DateTime.ParseExact("29/12/2012", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var course1 = context.Courses.Find(2);
            var student1 = context.Students.Find(3);

            var type2 = (ContentType)int.Parse("1");
            var date2 = DateTime.ParseExact("25/01/2011", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var course2 = context.Courses.Find(3);
            var student2 = context.Students.Find(1);

            var date3 = DateTime.ParseExact("07/08/2015", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var course3 = context.Courses.Find(1);
            var student3 = context.Students.Find(2);

            var date4 = DateTime.ParseExact("12/05/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var student4 = context.Students.Find(4);

            var homework1 = new Homework()
            {
                Content = "IT Events",
                ContentType = type1,
                SubmissionDate = date1,
                Course = course1,
                Student = student1
            };
            context.Homeworks.Add(homework1);
            context.SaveChanges();

            var homework2 = new Homework()
            {
                Content = "SoftUni News",
                ContentType = type2,
                SubmissionDate = date2,
                Course = course2,
                Student = student2
            };
            context.Homeworks.Add(homework2);
            context.SaveChanges();

            var homework3 = new Homework()
            {
                Content = "C# Statistics",
                ContentType = type1,
                SubmissionDate = date3,
                Course = course3,
                Student = student3
            };

            context.Homeworks.Add(homework3);
            context.SaveChanges();

            var homework4 = new Homework()
            {
                Content = "XML Concepts",
                ContentType = type2,
                SubmissionDate = date4,
                Course = course1,
                Student = student4
            };

            context.Homeworks.Add(homework4);
            context.SaveChanges();

        }
    }
}
