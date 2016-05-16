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

        public Partie PartieAssociee
        {
            get;
            private set;
        }


        //permet de gérer le parc gratuit
        public int ParcGratuit
        {
            get;
            set;
        }

        public void initPlateau()
        {
            XDocument xdoc = XDocument.Load("..\\..\\plateau.xml");

            Cases = (List<CasePlateau>)Plateau.deserialiser(xdoc.Root);
        }

        public Plateau(Partie p)
        {
            ParcGratuit = 0;
            PartieAssociee = p;
            Cases = new List<CasePlateau>();
        }

        public CasePlateau getCaseFromNum(int numero)
        {
            for (int i = 0; i < Cases.Count; i++)
            {
                if (i == numero) return Cases[i];
            }
            return null;
        }

        public bool appartientAuMemeProprio()
        {
            return false;
        }

        public void associerPioches(Pioche chance, Pioche communaute)
        {
            for (int i = 0; i < Cases.Count; i++)
            {
                if (Cases[i].Nom == "Chance")
                {
                    CasePioche c = Cases[i] as CasePioche;
                    c.associerPioche(chance);
                }
                else if (Cases[i].Nom == "Caisse de communauté")
                {
                    CasePioche c = Cases[i] as CasePioche;
                    c.associerPioche(communaute);
                }
            }
        }

        public override void serialiser(XElement racine)
        {
            // création d'une série de cases
            XElement cases = new XElement("cases");

            // chaque case se sérialise selon sa propre méthode
            foreach (CasePlateau c in Cases)
            {
                Serialisable ser = c as Serialisable;

                //if (ser != null)
                //ser.serialiser(cases);
            }

            // on ajoute la série de case à la racine
            racine.Add(cases);
        }

        public static new object deserialiser(XElement racine)
        {
            List<CasePlateau> cases = new List<CasePlateau>();

            // on récupère les paramètres de la partie, comme le nom des cases
            // qui sont toujours les mêmes, ou les loyers des gares qui sont
            // constants
            XElement parametres = racine.Element("parametres");

            // prix d'achat d'une gare
            int achatGare = (int)parametres.Element("gare").Element("achat");
            Gare.PrixAchatGare = achatGare;

            int achatCompagnie = (int)parametres.Element("compagnie").Element("achat");
            Compagnie.PrixAchatCompagnie = achatCompagnie;

            // couleurs à faire avant la création des terrains !
            var couleurs = from e in parametres.Element("couleurs").Elements()
                           select e;

            Couleur.Couleurs = new List<Couleur>();
            foreach (XElement x in couleurs)
            {

                Couleur c = (Couleur)Couleur.deserialiser(x);
                Couleur.Couleurs.Add(c);
            }


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

            Gare.setLoyers(loyersGare);


            //Run query
            // on récupère tous les éléments case du document
            var result = from e in racine.Descendants("cases").Elements()
                         select e;
            int couleur = 0;

            foreach (XElement x in result)
            {
                String spec = (String)x.Element("param").Attribute("spec");
                CasePlateau c = null;

                switch (spec)
                {
                    case "terrain":
                        couleur = (int)x.Element("couleur");
                        c = (Terrain)Terrain.deserialiser(x);
                        break;
                    case "communaute":
                        c = (CasePioche)CasePioche.deserialiser(x);  // case pioche
                        break;
                    case "chance":
                        c = (CasePioche)CasePioche.deserialiser(x); // case pioche
                        break;
                    case "gare":
                        c = (Gare)Gare.deserialiser(x);
                        break;
                    case "compagnie":
                        c = (Compagnie)Compagnie.deserialiser(x);
                        break;
                    case "argent":
                        c = (CaseSpeciale)CaseSpeciale.deserialiser(x);
                        break;
                    case "prison":
                        ActionPartie act = new ActionPartie("prison");
                        c = new CaseSpeciale((String)x.Element("nom"), (int)x.Element("numero"), act);
                        break;
                    case "allezprison":
                        ActionAllerA action = new ActionAllerA(cases[10], false);
                        c = new CaseSpeciale((String)x.Element("nom"), (int)x.Element("numero"), action);
                        break;

                }

                cases.Add(c);

                //Cases.Add(c);
                //Console.WriteLine(c.Numero);

            }
            return cases;
        }

    }
}
