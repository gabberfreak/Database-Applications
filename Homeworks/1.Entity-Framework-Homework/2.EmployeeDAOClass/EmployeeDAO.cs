namespace _2.EmployeeDAOClass
{
    using DbContext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Text;
    using System.Threading.Tasks;

    class EmployeeDAO
    {
        
        public static void Add(Employee employee)
        {
            var context = new SoftUniEntities();
            context.Employees.Add(employee);
            context.SaveChanges();
            Console.WriteLine(employee.FirstName + " " + employee.LastName + " --> " + employee.JobTitle);
        }

        public static Employee FindByKey(object key)
        {
            var context = new SoftUniEntities();
            var key1 = context.Employees.Find(key);
            return key1;
        }

        public static void Modify(Employee employee)
        {
            var context = new SoftUniEntities();
            employee.FirstName = "Gosho";
        }

        public static void Delete(Employee employee)
        {
            var context = new SoftUniEntities();
            context.Entry(employee).State = EntityState.Deleted;
            context.SaveChanges();
        }

        static void Main()
        {
            var context = new SoftUniEntities();

            var insertEmployee = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Peshov",
                JobTitle = "DbAdmin",
                DepartmentID = 1,
                HireDate = DateTime.Now,
                Salary = 2450.20m
            };

            Add(insertEmployee);

            Console.WriteLine();

            var employeeByKey = FindByKey(3);
            Console.WriteLine("Id:{0}, FirstName:{1} LastName:{2}", employeeByKey.EmployeeID, employeeByKey.FirstName, employeeByKey.LastName);

            Console.WriteLine();

            var modifyEmployee = context.Employees.First(e => e.LastName == "Gilbert");
            Modify(modifyEmployee);
            context.SaveChanges();
            Console.WriteLine(modifyEmployee.FirstName + " " + modifyEmployee.LastName);

            var deleteEmployee = context.Employees
                .OrderByDescending(e => e.EmployeeID)
                .First();
            context.Entry(deleteEmployee).State = EntityState.Detached;
            Delete(deleteEmployee);

        }
    }
}
