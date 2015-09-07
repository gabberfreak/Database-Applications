using System;
using System.Xml.Linq;

namespace DirectoryContentsAsXML
{
    class XElementDirectoryContents
    {
        static void Main()
        {

            string path = @"C:\Example";

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement("root-dir",
                    new XAttribute("path", path)
                ));

            doc.Element("root-dir").Add(
                new XElement("dir",
                    new XAttribute("name", "docs"),
                        new XElement("file", new XAttribute("name", "tutorial.pdf")),
                        new XElement("file", new XAttribute("name", "TODO.txt")),
                        new XElement("file", new XAttribute("name", "Presentation.pptx"))
                    ),
                new XElement("dir",
                    new XAttribute("name", "photos"),
                        new XElement("dir", new XAttribute("name", "birthday-4-march"),
                            new XElement("file", new XAttribute("name", "friends.jpg")),
                            new XElement("file", new XAttribute("name", "the_cake.jpg")),
                            new XElement("file", new XAttribute("name", "baloongs.jpg"))
                                    ),
                        new XElement("dir", new XAttribute("name", "travel"),
                            new XElement("file", new XAttribute("name", "IMG24152.jpg"))
                                    )
                            )
                    );

            Console.WriteLine(doc);
            doc.Save("../../directory.xml");
        }
    }
}
