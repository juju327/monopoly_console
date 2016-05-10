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

        public CaseSpeciale(String n, int num, Action action)
            : base(n, num)
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
            int numCase = (int)racine.Element("numero");
            String nomCase = (String)racine.Element("nom");
            String spec = (String)racine.Element("param").Attribute("spec"); //

            Action action = null;
            int somme = 0;
            bool gagnerArgent = false;
            switch (spec)
            {
                // case pour faire gagner ou perdre de l'argent (ex : taxe de luxe)
                case "argent":
                    somme = (int)racine.Element("somme");
                    if (somme > 0)
                        gagnerArgent = true;
                    else
                    {
                        gagnerArgent = false;
                        somme = -somme;
                    }
                    action = new ActionArgent(gagnerArgent, somme);
                    break;

                // case départ : on gagne 200€
                case "depart":
                    somme = 200;
                    gagnerArgent = true;
                    action = new ActionArgent(gagnerArgent, somme);
                    break;
                // case prise : on ne se déplace pas, on ne gagne pas d'argent
                case "prison":
                    somme = 0;
                    gagnerArgent = true;
                    action = new ActionArgent(gagnerArgent, somme);

                    // TODO voir si on a pas une meilleure façon de faire ça
                    break;
                case "allezprison":
                    // TODO gérer les déplacements sur une case spécifique
                    action = new ActionAllerA(null, false);
                    break;
            }

            return new CaseSpeciale(nomCase, numCase, action);
        }


    }
}
