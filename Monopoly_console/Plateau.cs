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
            Cases = new List<CasePlateau>();
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

            // on récupère les paramètres de la partie, comme le nom des cases
            // qui sont toujours les mêmes, ou les loyers des gares qui sont
            // constants
            var result = from e in xdoc.Descendants("parametres").Elements()
                         select e;
            
            XElement parametres = xdoc.Element("plateau").Element("parametres");

            
            
            // prix d'achat d'une gare
            int achatGare = (int)parametres.Element("gare").Element("achat");

            // loyers des gares
            int[] loyersGare = new int[4];
            var loyersXMLGare = from e in parametres.Element("gare").Descendants("loyer").Elements()
                                select e;

            int i = 0;
            foreach (XElement loyer in loyersXMLGare)
            {
                loyersGare[i] = (int)loyer;
                i++;
            }

            // nom des cases spéciales
            String nomCommunaute = (String)parametres.Element("communaute").Element("nom");
            String nomChance = (String)parametres.Element("chance").Element("nom");

            //Run query
            // on récupère tous les éléments case du document
            result = from e in xdoc.Descendants("cases").Elements()
                     select e;

            //Chaque case va se désérialiser elle-même
            foreach (XElement x in result)
            {
                CasePlateau c = null;

                int numCase = (int)x.Element("numero");
                String nomCase = (String)x.Element("nom");
                String typeCase = (String)x.Element("param").Attribute("type"); // pioche
                String spec = (String)x.Element("param").Attribute("spec"); // communaute


                switch (typeCase)
                {
                    case "propriete":
                        switch (spec)
                        {
                            case "gare":
                                c = new Gare(nomCase, numCase, loyersGare);
                                break;
                            case "compagnie":
                                c = new Compagnie("", 0);
                                break;
                            case "terrain":
                                int couleur = (int)x.Element("couleur");
                                int[] loyers = new int[6];
                                var loyersXML = from e in x.Descendants("loyer").Elements()
                                                select e;
                                int j = 0;
                                foreach (XElement loyer in loyersXML)
                                {
                                    loyers[j] = (int)loyer;
                                    j++;
                                }
                                c = new Terrain(nomCase, numCase, loyers, couleur);
                                break;
                        }
                        break;
                    case "pioche":

                        switch (spec)
                        {
                            case "communaute":
                                nomCase = nomCommunaute;
                                break;
                            case "chance":
                                nomCase = nomChance;
                                break;

                        }
                        c = new CaseSpeciale(nomCase, numCase, new Action(null));
                        break;

                    case "speciale":
                        c = new CaseSpeciale(nomCase, numCase, new Action(null));
                        break;
                    default:
                        c = null;
                        break;


                }

                Cases.Add(c);
                Console.WriteLine(typeCase);
                //Console.WriteLine(c);
                //Result += String.Format("{0}\r\n", user);
            }

            Console.WriteLine("toto");


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
