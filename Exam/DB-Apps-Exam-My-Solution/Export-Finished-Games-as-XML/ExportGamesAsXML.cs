using EF_Mappings;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Export_Finished_Games_as_XML
{
    class ExportGamesAsXML
    {
        static void Main()
        {
            var context = new DiabloEntities();
            var gamesQuery = context.Games
                .Where(g => g.IsFinished)
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Select(g => new
                {
                    GameName = g.Name,
                    Duration = g.Duration,
                    Users = g.UsersGames
                        .Select(u => new
                        {
                            UserName = u.User.Username,
                            IpAdress = u.User.IpAddress
                        })
                })
                .ToList();


            var xmlGames = new XElement("games");

            foreach (var game in gamesQuery)
            {
                var xmlGame = new XElement("game");
                xmlGame.Add(new XAttribute("name", game.GameName));
                if (game.Duration != null)
                {
                    xmlGame.Add(new XAttribute("duration", game.Duration));
                }

                var xmlUsers = new XElement("users");

                foreach (var user in game.Users)
                {
                    xmlUsers.Add(new XElement("user", 
                        new XAttribute("username", user.UserName),
                        new XAttribute("ip-address", user.IpAdress)));

                }
                xmlGame.Add(xmlUsers);
                xmlGames.Add(xmlGame);
            }

            var xmlDoc = new XDocument(xmlGames);
            xmlDoc.Save("../../finished-games.xml");
        }
    }
}
