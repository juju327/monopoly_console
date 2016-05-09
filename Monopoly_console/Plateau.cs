using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

namespace monopoly
{
    class Plateau : Serialisable
    {
        public List<CasePlateau> Cases
        {
            get;
            private set;
        }

        public void initPlateau()
        {
            // trouve les cases à créer (fichier XML)
            // remplit Cases avec les infos nécéssaires

            // reading
            /*
            StringBuilder output = new StringBuilder();

            String xmlString =
                    @"<?xml version='1.0'?>
                    <!-- This is a sample XML document -->
                    <Items>
                        <Item>test with a child element <more/> stuff</Item>
                    </Items>";

            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {

                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }

                }
            }
            Console.WriteLine(output.ToString());
            */

            XDocument xdoc = XDocument.Load("..\\..\\plateau.xml");

            //Console.WriteLine(xdoc);

            /*IEnumerable<CasePlateau> result = from c in xdoc.Descendants("plateau")
                                              select new CasePlateau((string)c.Element("nom"), (int)c.Element("numero"))
                                              {
                                                  
                                              };
*/


            //Run query
            // on récupère tous les éléments case du document
            var result = from e in xdoc.Descendants("plateau").Elements()
                         select e;

            //Chaque case va se désérialiser elle-même
            foreach (XElement x in result)
            {
                CasePlateau c;
                String typeCase = (String)x.Element("param").Attribute("type");
                String spec = (String)x.Element("param").Attribute("spec");
                switch (typeCase)
                {
                    case "propriete":
                        switch (spec)
                        {
                            case "gare":
                                c = new Gare("", 0);
                                break;
                            case "compagnie":
                                c = new Compagnie("", 0);
                                break;
                            case "terrain":
                                c = new Terrain("", 0);
                                break;
                        }
                        break;
                    case "pioche":
                        c = new CaseSpeciale("", 0, new Action(null));
                        break;
                    case "gare":
                        c = new CasePropriete("", 0);
                        break;
                    default:
                        c = null;
                        break;

                }
                Console.WriteLine(typeCase);
                //Console.WriteLine(c);
                //Result += String.Format("{0}\r\n", user);
            }


        }

        public void serialiser(XElement racine)
        {

        }

        public object deserialiser(XElement racine)
        {
            return null;
        }
    }
}
