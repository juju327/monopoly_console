using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Compagnie : CasePropriete
    {
        public Compagnie(String nom, int num)
            : base(nom, num)
        {

        }

        public override void serialiser(XElement racine)
        {
            XElement c = new XElement("case");

            XElement numero = new XElement("numero", Numero);
            XElement param = new XElement("param");
            param.SetAttributeValue("type", "propriete");
            param.SetAttributeValue("spec", "compagnie");
            XElement nom = new XElement("nom", Nom);

            c.Add(numero, param, nom);
            Console.WriteLine(c);
            //racine.Add(c);
        }

        public new static object deserialiser(XElement racine)
        {
            return null;
        }

    }
}
