using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Gare : CasePropriete
    {
        public static int[] Loyers
        {
            get;
            private set;
        }

        public static int PrixAchatGare { get; set; }

        public Gare(String n, int num, int[] loyers) : base(n, num)
        {
            Loyers = loyers;
            PrixAchat = PrixAchatGare;
        }

        public override void serialiser(XElement racine)
        {
            XElement gare = new XElement("case");
            XElement numero = new XElement("numero", Numero);
            XElement nom = new XElement("nom", Nom);
            XElement param = new XElement("param");
            param.SetAttributeValue("type", "propriete");
            param.SetAttributeValue("spec", "gare");

            gare.Add(numero, nom);
            racine.Add(gare);
        }

        public static void setLoyers(int[] loy)
        {
            Loyers = loy;
        }

        public new static Object deserialiser(XElement racine)
        {
            int numCase = (int)racine.Element("numero");
            String nomCase = (String)racine.Element("nom");

            //int prix = (int)racine.Element("achat");

            return new Gare(nomCase, numCase, Loyers);
        }

        public override int calculeLoyer()
        {
            return 200;
        }
    }
}
