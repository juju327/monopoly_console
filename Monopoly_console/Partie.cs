using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Partie : Serialisable
    {
        public Plateau Plateau
        {
            get;
            private set;
        }

        public List<Joueur> Joueurs
        {
            get;
            private set;
        }

        public De De
        {
            get;
            private set;
        }

        public Partie()
        {
            Plateau = new Plateau();
            Plateau.initPlateau();

            initJoueurs();
        }

        private void initJoueurs()
        {
            Joueurs = new List<Joueur>();
            Console.Write("Entrez le nombre de joueurs s'il vous plaît : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.Write("\nEntrez le nom du joueur {0} : ", i + 1);
                String s = Console.ReadLine();
            }

            Dictionary<Joueur, int> resultats = new Dictionary<Joueur, int>();

            for (int i = 0; i < nbJoueurs; i++)
            {
                De.Lancerde();
                int res = De.de1 + De.de2;

                // ajouter un couple "joueur", "résultats aux dés"
                resultats.Add(Joueurs[i], res);

            }

            // trier la liste d'items par valeur décroissante
            // et récupérer les clés (type Joueur)
            var items = from pair in resultats
                        orderby pair.Value descending
                        select pair.Key;

            List<Joueur> finaleTriee = items.ToList();

            Joueurs = finaleTriee;
        }

        public override void serialiser(XElement racine)
        {
            /*XElement nom = new XElement("Nom", Nom);
            XElement vole = new XElement("Vole", Vole);
            XElement tailleOeufs = new XElement("TailleOeufs", TailleOeufs);
            XElement pays = new XElement("Pays", Pays);

            partie.Add(nom, vole, tailleOeufs, pays);*/




            XElement partie = new XElement("Partie");
            // <Partie></Partie>

            XElement partie2 = new XElement("Partie", "toto");
            // <Partie>toto</Partie>

            racine.Add(partie);
            // <root>
            // <partie></partie>
            // </root>
        }

        public static new object deserialiser(XElement racine)
        {
            IEnumerable<Partie> result = from c in racine.Descendants("Oiseau")
                                         select new Partie()
                                         {
                                             /*Nom = (string)c.Element("Nom"),
                                             Pays = (string)c.Element("Pays"),
                                             Vole = (bool)c.Element("Vole"),
                                             TailleOeufs = (string)c.Element("TailleOeufs")*/
                                         };

            return result.First();
        }
    }
}
