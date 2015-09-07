using EF_Mappings;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Import_Users_and_Games_from_XML
{
    class UsersAndGamesFromXML
    {
        static void Main()
        {
            var context = new DiabloEntities();

            XmlDocument doc = new XmlDocument();
            doc.Load("../../users-and-games.xml");

            var root = doc.DocumentElement;

            foreach (XmlNode user in root.ChildNodes)
            {
                XmlNode userUserNameNode = user.Attributes["username"];
                string userName = userUserNameNode.InnerText;

                if (context.Users.Any(u => u.Username == userName))
                {
                    Console.WriteLine("User {0} already exists", user.Attributes["username"].InnerText);
                }

                else
                {
                    XmlNode userFirstNameNode = user.Attributes["first-name"];
                    XmlNode userLastNameNode = user.Attributes["last-name"];
                    XmlNode userEmailNode = user.Attributes["email"];

                    string userFirstName = null;
                    string userLastName = null;
                    string userEmail = null;

                    if (userFirstNameNode != null)
                    {
                        userFirstName = userFirstNameNode.InnerText;
                    }

                    if (userLastNameNode != null)
                    {
                        userLastName = userLastNameNode.InnerText;
                    }

                    if (userEmailNode != null)
                    {
                        userEmail = userEmailNode.InnerText;
                    }
                    

                    XmlNode userIsDeletedNode = user.Attributes["is-deleted"];
                    bool isDeleted = Convert.ToBoolean(Convert.ToInt32(userIsDeletedNode.InnerText));

                    XmlNode userIpAddressNode = user.Attributes["ip-address"];
                    string userIpAddress = userIpAddressNode.InnerText;

                    XmlNode userRegistrationDateNode = user.Attributes["registration-date"];
                    DateTime userRegistrationDate = DateTime.ParseExact(userRegistrationDateNode.InnerText, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    var newUser = new User()
                    {
                        FirstName = userFirstName,
                        LastName = userLastName,
                        Email = userEmail,
                        Username = userName,
                        IsDeleted = isDeleted,
                        IpAddress = userIpAddress,
                        RegistrationDate = userRegistrationDate
                    };

                    context.Users.Add(newUser);
                    Console.WriteLine("Successfully added user {0}", userName);

                    context.SaveChanges();

                    XmlNode gamesNode = user.SelectSingleNode("games");
                    foreach (XmlNode game in gamesNode.ChildNodes)
                    {
                        string gameName = game["game-name"].InnerText;
                        string characterName = game["character"].Attributes["name"].InnerText;
                        decimal cash = decimal.Parse(game["character"].Attributes["cash"].Value);
                        int level = int.Parse(game["character"].Attributes["level"].Value);
                        DateTime joinedOn = DateTime.ParseExact(game["joined-on"].InnerText, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        var newUserGame = new UsersGame()
                        {
                            Game = context.Games.FirstOrDefault(g => g.Name == gameName),
                            User = newUser,
                            Character = context.Characters.FirstOrDefault(ch => ch.Name == characterName),
                            Cash = cash,
                            Level = level,
                            JoinedOn = joinedOn
                        };

                        context.UsersGames.Add(newUserGame);
                        Console.WriteLine("User {0} successfully added to game {1}", newUser.Username, gameName);

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
