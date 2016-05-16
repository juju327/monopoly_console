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
            ActionAEffectuer = action;
        }

        public override void serialiser(XElement racine)
        {
            XElement c = new XElement("case");

            //c.Add();
            //MaConsole.ecrireLigne(c);
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
                case "prison":
                    action = new ActionAllerA(30, false);
                    break;
            }

            CaseSpeciale c = new CaseSpeciale(nomCase, numCase, action);
            return c;
        }

        public override void estTombeSur(Partie partie)
        {
            Joueur j = partie.JoueurEnCours;
            if (Numero == 20)
            {
                j.gagner(Pioche.Plateau.ParcGratuit);
                MaConsole.ecrireLigne("Vous récupérez le solde du parc gratuit de " + Pioche.Plateau.ParcGratuit + "€ !");
            }


            MaConsole.ecrireLigne("Vous êtes tombé sur la case {0}", Nom);
            MaConsole.hauteurLigne += 2;
            if (Numero == 20)
            {
                j.gagner(Pioche.Plateau.ParcGratuit);
                Pioche.Plateau.ParcGratuit = 0;
                MaConsole.ecrireLigne("Vous récupérez le solde du parc gratuit de " + Pioche.Plateau.ParcGratuit + "€ !");
            }
            else if (Numero == 30)
            {
                j.EstEnPrison = true;
                MaConsole.ecrireLigne("Vous allez en prison ! Vous ne pourrez plus jouer pendant 3 tours...");
                MaConsole.ecrireLigne("Pour sortir de prison, vous pouvez tenter de faire un double aux dés");
                MaConsole.ecrireLigne("ou utiliser une de vos éventuelles cartes \"Libéré de Prison\" !");
                MaConsole.ecrireLigne("Au bout de 3 tours, vous sortez de prison et pouvez jouer normalement.");
            }
            else
                this.ActionAEffectuer.executer(partie);
        }
    }
}
