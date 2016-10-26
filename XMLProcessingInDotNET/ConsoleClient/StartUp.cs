using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ConsoleClient
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var separator = new string[50];
            string url = @"..\..\Data\catalog.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            var docElement = doc.DocumentElement;
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            // Last minute job, please ignore my totally anti-HQC

            Console.WriteLine(string.Join("-", separator));

            ExtractingArtistsUsingDOMParser(docElement);

            Console.WriteLine(string.Join("-", separator));

            ExtractingArtistsUsingXPath(doc);

            Console.WriteLine(string.Join("-", separator));

            DeleteUsingDOMParser(doc, docElement);

            Console.WriteLine(string.Join("-", separator));

            ExtractSongTitlesWithXmlReader(url);

            Console.WriteLine(string.Join("-", separator));

            ExtractSongTitlesWithLINQ(url);


            string personInfoUrl = @"..\..\Data\person.txt";
            string personXMLUrl = @"..\..\Data\person.xml";
            ParsePersonFromTextFileToXml(personInfoUrl, personXMLUrl, encoding);

            Console.WriteLine(string.Join("-", separator));

            string albumXMLUrl = @"..\..\Data\album.xml";
            ExtractAlbumsInfoInSeparateXML(url, albumXMLUrl, encoding);

            string xsdUrl = @"..\..\Data\catalog.xsd";
            ValidateXMLWithXSLSchema(url, xsdUrl);

            string xsltUrl = @"..\..\Data\catalog.xslt";
            XPathDocument myXPathDoc = new XPathDocument(url);
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(xsltUrl);
            XmlTextWriter myWriter = new XmlTextWriter(@"..\..\Data\catalog.html", null);
            myXslTrans.Transform(myXPathDoc, null, myWriter);
        }

        public static void ValidateXMLWithXSLSchema(string url, string xsdUrl)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdUrl);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += Handler;

            var reader = XmlReader.Create(url, settings);
        }

        private static void Handler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
                System.Diagnostics.Trace.WriteLine(
                  String.Format("Line: {0}, Position: {1} \"{2}\"",
                      e.Exception.LineNumber, e.Exception.LinePosition, e.Exception.Message));
        }

        public static void ExtractAlbumsInfoInSeparateXML(string url, string albumXMLUrl, Encoding encoding)
        {
            Hashtable albumInfo = new Hashtable();
            using (var reader = XmlReader.Create(url))
            {
                string name = string.Empty;
                string author = string.Empty;
                while (reader.Read())
                {
                    if (reader.Name == "name")
                    {
                        name = reader.ReadInnerXml();
                    }
                    if (reader.Name == "artist")
                    {
                        author = reader.ReadInnerXml();
                    }
                    if (name != string.Empty && author != string.Empty)
                    {
                        albumInfo.Add(name, author);
                        name = string.Empty;
                        author = string.Empty;
                    }
                }
            }
            using (XmlTextWriter writer = new XmlTextWriter(albumXMLUrl, encoding))
            {

                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("albums");
                foreach (DictionaryEntry item in albumInfo)
                {
                    WriteAlbum(writer, item.Key.ToString(), item.Value.ToString());
                }
                writer.WriteEndDocument();
            }

        }

        private static void WriteAlbum(XmlWriter writer, string name, string author)
        {
            // not entirely sure if as attributes or as elements is better.
            writer.WriteStartElement("album");
            writer.WriteAttributeString("author", author);
            writer.WriteElementString("name", name);
            writer.WriteEndElement();
        }

        public static void ParsePersonFromTextFileToXml(string personInfoUrl, string personXMLUrl, Encoding encoding)
        {
            string name = string.Empty;
            string address = string.Empty;
            string phone = string.Empty;

            System.IO.StreamReader file = new System.IO.StreamReader(personInfoUrl);
            name = file.ReadLine();
            address = file.ReadLine();
            phone = file.ReadLine();
            file.Close();

            using (XmlTextWriter writer = new XmlTextWriter(personXMLUrl, encoding))
            {

                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("people");
                WritePerson(writer, name, address, phone);
                writer.WriteEndDocument();
            }

        }

        private static void WritePerson(XmlWriter writer, string name, string address, string phone)
        {
            writer.WriteStartElement("person");
            writer.WriteElementString("name", name);
            writer.WriteElementString("address", address);
            writer.WriteElementString("phone", phone);
            writer.WriteEndElement();
        }

        public static void ExtractSongTitlesWithLINQ(string url)
        {
            var document = XDocument.Load(url);

            var titles =
                from song in document.Root
                                    .Elements("album")
                                    .Elements("songs")
                                    .Elements("song")
                select song.Element("title").Value;

            foreach (var item in titles)
            {
                Console.WriteLine(item);
            };
        }

        public static void ExtractSongTitlesWithXmlReader(string url)
        {
            using (var reader = XmlReader.Create(url))
            {
                while (reader.Read())
                {
                    if (reader.Name == "title")
                    {
                        Console.WriteLine(reader.ReadInnerXml());
                    }
                }
            }
        }

        public static void DeleteUsingDOMParser(XmlDocument doc, XmlElement docElement)
        {
            foreach (XmlElement item in docElement.ChildNodes)
            {
                if (double.Parse(item.GetElementsByTagName("price")[0].InnerText) > 20)
                {
                    docElement.RemoveChild(item);
                }
            }

            foreach (XmlElement item in docElement.ChildNodes)
            {
                Console.WriteLine(item.GetElementsByTagName("name")[0].InnerText);
            }

            doc.Save(@"..\..\Data\catalog-new.xml");
        }

        public static void ExtractingArtistsUsingXPath(XmlDocument doc)
        {

            string xPath = "/catalog/album/artist";
            Hashtable xPathHashTable = new Hashtable();

            var titleNodes = doc.SelectNodes(xPath);
            foreach (XmlElement titleNode in titleNodes)
            {
                var currentArtist = titleNode.InnerText;
                if (!xPathHashTable.Contains(currentArtist))
                {
                    xPathHashTable.Add(currentArtist, 0);
                }
                xPathHashTable[currentArtist] = (int)xPathHashTable[currentArtist] + 1;
            }

            foreach (DictionaryEntry item in xPathHashTable)
            {
                Console.WriteLine($"{item.Key} - {item.Value} album(s).");
            }
        }

        public static void ExtractingArtistsUsingDOMParser(XmlElement docElement)
        {
            var hashTable = new Hashtable();

            foreach (XmlElement item in docElement.ChildNodes)
            {
                var currentArtist = item.GetElementsByTagName("artist")[0].InnerText;
                if (!hashTable.Contains(currentArtist))
                {
                    hashTable.Add(currentArtist, 0);
                }
                hashTable[currentArtist] = (int)hashTable[currentArtist] + 1;
            }

            foreach (DictionaryEntry item in hashTable)
            {
                Console.WriteLine($"{item.Key} - {item.Value} album(s).");
            }
        }
    }
}
