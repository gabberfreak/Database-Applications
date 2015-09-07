using System;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;

namespace XPathOldAlbums
{
    class OldAlbums
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            var root = doc.DocumentElement;

            string albumsQuery = "/albums/album";
            XmlNodeList albums = doc.SelectNodes(albumsQuery);

            string culture = root.Attributes["culture"].Value;
            CultureInfo numberFormat = new CultureInfo(culture);

            foreach (XmlNode albumNode in albums)
            {
                int year = int.Parse(albumNode["year"].InnerText, numberFormat);
                if (year >= DateTime.Now.AddYears(-5).Year)
                {
                    string title = albumNode.SelectSingleNode("name").InnerText;
                    string priceStr = albumNode.SelectSingleNode("price").InnerText;
                    decimal price = Decimal.Parse(priceStr, numberFormat);
                    Console.WriteLine("Album title: " + title);
                    Console.WriteLine("Album price: " + price);
                    Console.WriteLine("Album year: " + year);
                    Console.WriteLine();
                }

            }
        }
    }
}
