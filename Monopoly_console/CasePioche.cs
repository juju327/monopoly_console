using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class CasePioche : CasePlateau
    {
        public CasePioche(String n, int num)
            : base(n, num)
        {
        }

        public Pioche Pioche
        {
            get;
            private set;
        }

        public void associerPioche(Pioche p)
        {
            Pioche = p;
        }

        public override void estTombeSur(Joueur j)
        {
            CartePioche c = Pioche.piocher();
            Console.WriteLine("Vous êtes tombé sur la case {0}. Vous piochez la carte :\n {1}", Nom, c.Description);

            c.Action.executer(j);
        }

        public static new Object deserialiser(XElement racine)
        {
            CasePioche c;
            int numCase = (int)racine.Element("numero");
            String nomCase = "";
            String spec = (String)racine.Element("param").Attribute("spec"); //

            ///Action action = null;

            switch (spec)
            {
                case "communaute":
                    nomCase = "Caisse de communauté";
                    break;
                case "chance":
                    nomCase = "Chance";
                    break;
            }

            c = new CasePioche(nomCase, numCase);
            return c;
        }
    }
}
