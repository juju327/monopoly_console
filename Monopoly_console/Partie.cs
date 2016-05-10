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
            De = new De();
            initJoueurs();
        }



        private void initJoueurs()
        {
            int[] de = new int[100];
            Joueurs = new List<Joueur>();
            Console.Write("Entrez le nombre de joueurs s'il vous plaît : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.Write("\nEntrez le nom du joueur {0} : ", i + 1);
                String s = Console.ReadLine();
                Joueur J = new Joueur(s);
                Joueurs.Add(J);
            }

            Console.WriteLine("Veuillez lancer les dés pour commencer. \n Appuyez sur la touche Entree pour lancer les dés \r\n");
            Console.ReadKey();


            Dictionary<Joueur, int> resultats = new Dictionary<Joueur, int>();
           
            for (int i = 0; i < nbJoueurs; i++)
            {
                De.Lancerde();
                int res = De.de1 + De.de2;
                Console.WriteLine(Joueurs[i].Nom + ". Votre score est de " + res);
                // ajouter un couple "joueur", "résultats aux dés"

                resultats.Add(Joueurs[i], res);
            }

              /* Gerer les personnes qui font deu fois le même résultat !
                
                if (resultats.ContainsValue(res))    
                {
                    Console.WriteLine("égalité");

                    var egals = from entry in resultats
                                where entry.Value == res
                                select entry.Key;

                    foreach (var egal in egals)
                    {

                        De.Lancerde();
                        res = De.de1 + De.de2;
                        

                    }




                    
                }
            }*/

         
           
            
            // trier la liste d'items par valeur décroissante
            // et récupérer les clés (type Joueur)
            var items = from pair in resultats
                        orderby pair.Value descending
                        select pair.Key;

            List <Joueur>finaleTriee = items.ToList();
            Joueurs = finaleTriee;
            Console.WriteLine("\n" + finaleTriee[0].Nom + " \n Vous jouez en premier");
            for (int i = 1; i < nbJoueurs; i++)
            {
                Console.WriteLine("\n" + finaleTriee[i].Nom + "\n Vous jouez en " + (i + 1) + "ème.");
            }

            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.WriteLine("\r\n " + finaleTriee[i].Nom + " A votre tour. \n Veuillez appuyer sur Entrée pour lancer les dés.");
                Console.ReadKey();
                De.Lancerde();
                int res1 = De.de1 + De.de2;
                Console.WriteLine(Joueurs[i].Nom + ". Votre avez obtenu " + res1);
            }
       
        
        }


        public void serialiser(XElement racine)
        {
            XElement partie = new XElement("Partie");
            /*XElement nom = new XElement("Nom", Nom);
            XElement vole = new XElement("Vole", Vole);
            XElement tailleOeufs = new XElement("TailleOeufs", TailleOeufs);
            XElement pays = new XElement("Pays", Pays);

            partie.Add(nom, vole, tailleOeufs, pays);*/
            racine.Add(partie);
        }

        public object deserialiser(XElement racine)
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
