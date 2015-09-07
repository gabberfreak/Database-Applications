namespace _3.DatabaseSearchQueries
{
    using DbContext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SearchQuery
    {
        static void Main()
        {
            //Task 1
            var context = new SoftUniEntities();
            var employeeProjects = context.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Projects = e.Projects
                                        .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
                                        .Select(p => new
                                        {
                                            p.Name,
                                            p.StartDate,
                                            p.EndDate
                                        }).DefaultIfEmpty(),
                    ManagerName = e.Manager.FirstName + " " + e.Manager.LastName
                });

            foreach (var employee in employeeProjects)
            {
                Console.WriteLine("Employee: {0} {1} ----- {2}"
                    , employee.FirstName, employee.LastName, employee.ManagerName);
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine("Project Name: {0}\nStartDate: {1}\nEndDate: {2}\n"
                        , project.Name, project.StartDate, project.EndDate);
                }
            }

            Console.WriteLine();

            //Task 2
            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .Select(a => new
                {
                    Address = a.AddressText,
                    Town = a.Town.Name,
                    EmployeeCount = a.Employees.Count
                })
                .ToList();

            foreach (var address in addresses)
            {
                Console.WriteLine("{0}, {1} - {2} employees",
                    address.Address,
                    address.Town,
                    address.EmployeeCount);
            }

            Console.WriteLine();

            //Task 3
            var employeeById = context.Employees
                .Where(e => e.EmployeeID == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.Projects
                        .Select(p => new
                        {
                            ProjectName = p.Name
                        })
                        .OrderBy(p => p.ProjectName)
                })
                .FirstOrDefault();
            Console.WriteLine("{0} {1} --- Job Title: {2}", 
                employeeById.FirstName,employeeById.LastName,employeeById.JobTitle);
            foreach (var project in employeeById.Projects)
            {
                Console.WriteLine("Project name:{0}", project.ProjectName);
            }
            Console.WriteLine();

            //Task 4
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerName = d.Manager.LastName,
                    Employees = d.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.HireDate,
                        e.JobTitle
                    })
                })
                .ToList();
            Console.WriteLine(departments.Count);

            foreach (var department in departments)
            {
                Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
                    department.DepartmentName,
                    department.ManagerName,
                    department.Employees.Count());
            }
        }
    }
}
