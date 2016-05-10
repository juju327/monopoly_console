using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class CaseSpeciale : CasePlateau
    {
        public Action ActionAEffectuer
        {
            get;
            private set;
        }

        public CaseSpeciale(String n, int num, Action action) : base(n, num)
        {

        }

        public override void serialiser(XElement racine)
        {
            XElement c = new XElement("case");

            //c.Add();
            Console.WriteLine(c);
            //racine.Add(c);
        }

        public static new object deserialiser(XElement racine)
        {
            return null;
        }


    }
}
