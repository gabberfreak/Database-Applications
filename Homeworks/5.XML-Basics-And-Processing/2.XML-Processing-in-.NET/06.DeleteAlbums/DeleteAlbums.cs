using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace DeleteAlbums
{
    class DeleteAlbums
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");

            var root = doc.DocumentElement;
            string culture = root.Attributes["culture"].Value;
            CultureInfo numberFormat = new CultureInfo(culture);

            while (true)
            {
                bool removed = false;
                foreach (XmlNode node in root)
                {
                    string currPriceStr = node["price"].InnerText;
                    decimal currPrice = Decimal.Parse(currPriceStr, numberFormat);
                    if (currPrice > 20m)
                    {
                        root.RemoveChild(node);
                        removed = true;
                        break;
                    }
                }

                if (!removed)
                {
                    break;
                }
            }
            

            doc.Save(@"../../cheap-albums-catalog.xml");
        }
    }
}
