using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Gare : CasePropriete, Serialisable
    {
        public int[] Loyers
        {
            get;
            private set;
        }

        public Gare(String n, int num, int[] loyers) : base(n, num)
        {
            Loyers = loyers;
        }

        public new void serialiser(XElement racine)
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

        public new Object deserialiser(XElement racine)
        {
            return new Gare("", 0, new int[4]);
        }
    }
}
