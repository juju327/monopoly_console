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

        public Des Des
        {
            get;
            private set;
        }

        public Pioche CartesCommunaute
        {
            get;
            private set;
        }

        public Pioche CartesChance
        {
            get;
            private set;
        }
        public static Joueur JoueurEnCours { get; private set; }

        public Partie()
        {
            Plateau = new Plateau(this);
            initCartes();
            initCouleurs();
            Plateau.associerPioches(CartesChance, CartesCommunaute);
            

            Plateau.associerPioches(CartesChance, CartesCommunaute);
            Des = new Des();
            initJoueurs();
            jouer();
        }

        private void initCartes()
        {
            XDocument xdoc = XDocument.Load("..\\..\\chance.xml");

            CartesChance = new Pioche(Plateau, xdoc.Root);
             xdoc = XDocument.Load("..\\..\\communaute.xml");

            CartesCommunaute = new Pioche(Plateau, xdoc.Root);

           
        }

        private void jouer()
        {
            int compteurTour = 1;
            while (true)  // A gérer ensuite
            {
                for (int i = 0; i < Joueurs.Count; i++)
                {
                    JoueurEnCours = Joueurs[i];

                    afficherInfos(JoueurEnCours, compteurTour);
                    int position = JoueurEnCours.CaseActuelle;
                    
                    
                    
                    if (compteurTour >= 2 && JoueurEnCours.ListeProprietes.Count>0)
                    {

                        Console.WriteLine("Souhaitez-vous acheter des maisons/hotels pour vos propriétés? (o/n)");
                        string answer = Console.ReadLine();

                        if (answer == "o")
                        {
                           Joueurs[i].construire();
                        }
                        else
                        {
                            if (answer == "n")
                            {

                                Console.WriteLine("Vous n'avez pas souhaitez acheter");
                            }




                        }

                    }
                    
                        Console.WriteLine("Veuillez appuyer sur Entrée pour lancer les dés.");
                        Console.ReadKey();
                        Des.lancerDes();
                        int res1 = Des.de1 + Des.de2;
                        Console.WriteLine("Votre avez obtenu {0} aux dés.", res1);

                        if (position + res1 < 40)
                        {
                            // à voir qui gère le déplacement : la partie ou le joueur ?
                            CasePlateau destination = Plateau.getCaseFromNum(res1 + position);
                            JoueurEnCours.deplacerA(destination, true);

                            //Joueurs[i].CaseActuelle = Joueurs[i].CaseActuelle + res1;
                        }

                        else
                        {
                            //int temp = 40 - Joueurs[i].CaseActuelle; // le joueur finit son tour sur le plateau
                            //int res2 = res1 - temp; // calcule le nombre de cases à avancer pour le nouveau tour de plateau
                            //Joueurs[i].CaseActuelle = 0 + res2;

                            CasePlateau destination = Plateau.getCaseFromNum(res1 + position - 40);
                            JoueurEnCours.deplacerA(destination, true);

                        }


                        CasePlateau caseTombe = Plateau.getCaseFromNum(JoueurEnCours.CaseActuelle);

                        caseTombe.estTombeSur(JoueurEnCours);
                        position = Joueurs[i].CaseActuelle;
                        int argent = Joueurs[i].Argent;
                        if (position == 20)
                        {
                            argent += Pioche.Plateau.parc;
                            Console.WriteLine("Postion : Parc gratuit .Vous récupérez le solde du parc gratuit de" + Pioche.Plateau.parc + " \n Solde :" + argent);
                            Console.ReadLine();

                        }

                        else
                        {
                            Console.WriteLine("Position :  {0} --- Solde : {1} ", caseTombe, argent);

                            Console.WriteLine("Appuyez sur Entrée pour finir votre tour.");
                            Console.ReadLine();
                        }

                    } compteurTour++;

                }
            }
        
        

        private void afficherTitre()
        {
            Console.WriteLine(@"
************               MONOPOLY           ************");
        }

        private void afficherInfos(Joueur j,int nbTours)
        {
            int position = j.CaseActuelle;
            String nomCase = Plateau.getCaseFromNum(position).Nom;
           Console.Clear();
            afficherTitre();
            Console.WriteLine(@"
------------    Tour{0} - {1} ------------",nbTours, j.Nom);
            Console.WriteLine("Position :  {0} --- Solde : {1} ", Plateau.getCaseFromNum(position).Nom, j.Argent);
        }


        public void anniversaire()
        {
            int somme = 0;
            for (int i = 0; i < Joueurs.Count; i++)
            {
                if (Joueurs[i] != JoueurEnCours)
                    Joueurs[i].perdre(10);
                somme += 10;
            }
            JoueurEnCours.gagner(somme);

        }

        private void initJoueurs()
        {
            //Console.Clear();
            afficherTitre();
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
            Console.ReadLine();


            Dictionary<Joueur, int> resultats = new Dictionary<Joueur, int>();

            for (int i = 0; i < nbJoueurs; i++)
            {
                Des.lancerDes();
                int res = Des.de1 + Des.de2;
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

            List<Joueur> finaleTriee = items.ToList();
            Joueurs = finaleTriee;
            Console.WriteLine("\n" + finaleTriee[0].Nom + " \n Vous jouez en premier");
            for (int i = 1; i < nbJoueurs; i++)
            {
                Console.WriteLine("\n" + finaleTriee[i].Nom + "\n Vous jouez en " + (i + 1) + "ème.");
            }

            Console.ReadLine();

        }


        public void initCouleurs()
        {
            Couleur Violet = new Couleur(0,100,500);
            Couleur BleuCiel = new Couleur(1,50,450);
            Couleur Rose = new Couleur(2, 50, 450);
            Couleur Orange = new Couleur(3, 100, 500);
            Couleur Rouge = new Couleur(4, 150, 750);
            Couleur Jaune = new Couleur(5, 150, 750);
            Couleur Vert = new Couleur(6, 200, 1000);
            Couleur Bleu = new Couleur(7, 200, 1000);




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
