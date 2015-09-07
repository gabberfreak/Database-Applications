using System;
using System.Collections.Generic;
using System.Xml;

namespace XPathArtistsAndNumberOfAlbums
{
    class XPathArtistsAndAlbums
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");

            string artistsQuery = "/albums/album";
            XmlNodeList albums = doc.SelectNodes(artistsQuery);

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (XmlNode artistNode in albums)
            {
                string artist = artistNode.SelectSingleNode("artist").InnerText;
                if (dictionary.ContainsKey(artist))
                {
                    dictionary[artist]++;
                }
                else
                {
                    dictionary.Add(artist, 1);
                }
            }

            foreach (var pair in dictionary)
            {
                Console.WriteLine("Artist: {0}\nCount of albums: {1}",
                    pair.Key, pair.Value);
                Console.WriteLine();
            }
        }
    }
}
