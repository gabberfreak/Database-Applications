namespace _6.CallStoredProcedure
{
    using DbContext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    using System.Threading;

    class StoredProcedure
    {
        static void Main()
        {
            var context = new SoftUniEntities();
 
            var projects = context.GetProjectsByEmployee("Ruth", "Ellerbrock");
            foreach (var project in projects)
            {
                Console.WriteLine(project.Name + " - "
                                        + project.Description + ", " 
                                                + project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                                                    + "\n");
            }
        }
    }
}
