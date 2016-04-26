using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.IsolatedStorage;

namespace monopoly
{
    class Plateau
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




        }
    }
}
