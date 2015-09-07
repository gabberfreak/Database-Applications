using EF_Mappings;
using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.IO;

namespace Export_Characters_and_Players_as_JSON
{
    class ExportCharactersAndPlayersJSON
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var charactersQuery = context.Characters
                .OrderBy(ch => ch.Name)
                .Select(ch => new
                {
                    name = ch.Name,
                    playedBy = ch.UsersGames.Select(u => u.User.Username)
                })
                .ToList();

            var jsSerializer = new JavaScriptSerializer();
            var charsJson = jsSerializer.Serialize(charactersQuery);
            File.WriteAllText("../../characters.json", charsJson);
        }
    }
}
