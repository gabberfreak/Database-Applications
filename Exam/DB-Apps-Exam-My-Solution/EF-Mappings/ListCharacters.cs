using System;
using System.Linq;

namespace EF_Mappings
{
    class ListCharacters
    {
        static void Main()
        {
            var context = new DiabloEntities();
            var testQuery = context.Characters.Select(ch => ch.Name);
            foreach (var character in testQuery)
            {
                Console.WriteLine(character);
            }
        }
    }
}
