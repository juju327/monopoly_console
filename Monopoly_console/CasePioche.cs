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

        public override void estTombeSur(Partie partie)
        {
            CartePioche c = Pioche.piocher();

            MaConsole.ecrireLigne("Vous êtes tombé sur la case");
            MaConsole.ecrireLigne(" > {0}.", Nom);
            MaConsole.ecrireLigne("Vous piochez la carte :");
            MaConsole.hauteurLigne++;
            MaConsole.ecrireLigne(c.Description);
            MaConsole.hauteurLigne++;

            c.Action.executer(partie);
        }

        public static new Object deserialiser(XElement racine)
        {
            CasePioche c;
            // nom des cases spéciales

            int numCase = (int)racine.Element("numero");
            String nomCase = "";
            String spec = (String)racine.Element("param").Attribute("spec");

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
