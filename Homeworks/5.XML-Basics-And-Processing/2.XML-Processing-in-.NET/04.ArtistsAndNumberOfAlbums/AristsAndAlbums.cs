using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ArtistsAndNumberOfAlbums
{
    class AristsAndAlbums
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");

            XmlNode root = doc.DocumentElement;
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (XmlNode node in root.ChildNodes)
            {
                string artist = node["artist"].InnerText;
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
                    pair.Key,pair.Value);
                Console.WriteLine();
            }
        }
    }
}
