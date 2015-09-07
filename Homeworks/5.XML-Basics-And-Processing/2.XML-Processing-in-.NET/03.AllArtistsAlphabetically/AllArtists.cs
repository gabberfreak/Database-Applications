using System;
using System.Collections.Generic;
using System.Xml;

namespace AllArtistsAlphabetically
{
    class AllArtists
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");

            XmlNode root = doc.DocumentElement;
            SortedSet<string> set = new SortedSet<string>();

            foreach (XmlNode node in root.ChildNodes)
            {
                string artist = node["artist"].InnerText;
                set.Add(artist);
            }

            foreach (string val in set)
            {
                Console.WriteLine(val);
            }
        }
    }
}
