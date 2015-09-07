namespace _5.ConcurrentDatabaseChangesWithEF
{
    using DbContext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DatabaseChanges
    {
        static void Main()
        {
            var context1 = new SoftUniEntities();
            var employee1 = context1.Employees.Find(1);
            employee1.FirstName = "Gegata";

            var context2 = new SoftUniEntities();
            var employee2 = context2.Employees.Find(2);
            employee2.FirstName = "Manaf";

            context1.SaveChanges();
            context2.SaveChanges();
        }
    }
}
