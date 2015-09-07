using System;
using System.Xml;

namespace ExtractAlbumNames
{
    class AlbumNames
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");

            XmlNode root = doc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                Console.WriteLine("Album name: {0}", node["name"].InnerText);
            }
        }
    }
}
