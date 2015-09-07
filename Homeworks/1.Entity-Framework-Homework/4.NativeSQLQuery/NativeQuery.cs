namespace _4.NativeSQLQuery
{
    using DbContext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    class NativeQuery
    {
        public static void PrintNamesWithNativeQuery()
        {
            var context = new SoftUniEntities();
            string query =
                "SELECT e.FirstName FROM Employees e " +
                "WHERE EXISTS(SELECT p.ProjectID FROM Projects p " +
                "LEFT JOIN EmployeesProjects ep " +
                "ON p.ProjectID = ep.ProjectID " +
                "LEFT JOIN Employees em " +
                "ON ep.EmployeeID = em.EmployeeID " +
                "WHERE em.EmployeeID = e.EmployeeID " +
                "AND YEAR(p.StartDate) = 2002)";

            var queryResult = context.Database.SqlQuery<string>(query);
            List<string> employees = queryResult.ToList();

            Console.WriteLine(string.Join(" | ", employees));
        }

        public static void PrintNamesWithLinqQuery()
        {
            var context = new SoftUniEntities();

            var query = context.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                .Select(e => e.FirstName);

            var employees = query.ToList();

            Console.WriteLine(string.Join(" | ", employees));
        }
        
        static void Main()
        {
            var context = new SoftUniEntities();

            var totalCount = context.Employees.Count();

            var sw = new Stopwatch();
            sw.Start();

            PrintNamesWithNativeQuery();
            Console.WriteLine("Native: {0}",sw.Elapsed);

            sw.Reset();

            PrintNamesWithLinqQuery();
            Console.WriteLine("Linq: {0}",sw.Elapsed);
            var emp = context.Employees.Where(e => e.LastName == "Gilbert").First();
            emp.FirstName = "Guy";
            context.SaveChanges();
        }
    }
}
