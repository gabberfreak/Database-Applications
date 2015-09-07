using System;
using System.Linq;
using System.Xml.Linq;

namespace LINQToXMLOldAlbums
{
    class LinqToXml
    {
        static void Main()
        {
            XDocument albums = XDocument.Load("../../../catalog.xml");
            var catalog = albums.Descendants("album")
                .Where(a => int.Parse(a.Element("year").Value) >= DateTime.Now.Year - 5)
                .Select(a => new
                {
                    Title = a.Element("name").Value,
                    Price = decimal.Parse(a.Element("price").Value)
                });

            foreach (var album in catalog)
            {
                Console.WriteLine(album.Title + " " + album.Price);
            }
        }
    }
}
