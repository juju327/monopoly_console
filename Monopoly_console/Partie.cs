using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private int nbCasesPlateau = 40;

        public Joueur JoueurEnCours
        {
            get;
            private set;
        }

        public Partie()
        {
            //afficherPlateau();
            Plateau = new Plateau(this);
            Plateau.initPlateau();

            // problème lors de la désérialisation
            if (Plateau.Cases.Count != nbCasesPlateau)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier XML : nombre d'éléments incorrects.");
                Console.WriteLine("Arrêt du programme...");
                Thread.Sleep(6000);
            }
            else {

                initCartes();
                //initCouleurs();

                Plateau.associerPioches(CartesChance, CartesCommunaute);

                MaConsole.hauteurLigne = 0;

                Des = new Des();
                //initJoueurs();
                initJ();
                jouer();
            }
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
                foreach (Joueur joueur in Joueurs)
                {
                    // Le joueur dont c'est le tour
                    JoueurEnCours = joueur;

                    // on nettoie l'affichage
                    Console.Clear();

                    // affichage du plateau, des joueurs, et des infos du joueur en cours
                    MaConsole.afficherConsole(Joueurs, JoueurEnCours, Plateau);

                    // affichage du nombre de tours joués et du joueur en cours
                    MaConsole.hauteurLigne = 0;
                    MaConsole.ecrireLigne(" * * Tour {0} - à {1} de jouer ! * *", compteurTour, JoueurEnCours.Nom);
                    MaConsole.ecrireLigne("");

                    //MaConsole.clearCentre();

                    // avant de commencer à jouer, on demande s'il veut acheter
                    if (JoueurEnCours.aTerrainConstructible())
                    {
                        bool continuer = true;
                        do
                        {
                            MaConsole.ecrireLigneSansSauver("Vous avez des propriétés constructibles.");
                            MaConsole.ecrireLigneSansSauver("Souhaitez-vous construire sur vos propriétés ? (o/n)");
                            if (MaConsole.lireLigne() == "o")
                            {
                                joueur.construire();
                                Console.Clear();
                                //MaConsole.consoleCentrale;
                                MaConsole.hauteurLigne = 0;
                                // on enlève les infos superflues pour ne pas surcharger la console

                                //MaConsole.clearCentre();
                                MaConsole.afficherConsole(Joueurs, JoueurEnCours, Plateau);
                                MaConsole.afficherCentre();
                            }
                            else {
                                MaConsole.ecrireLigneSansSauver("Vous n'avez pas souhaité construire.");
                                continuer = false;
                            }
                        } while (continuer);
                    }

                    // On propose ensuite de commencer à jouer
                    if (!JoueurEnCours.EstEnPrison)
                    {
                        Des.lancerDes();
                        int resultatsDes = Des.de1 + Des.de2;

                        JoueurEnCours.deplacer(resultatsDes, Plateau);

                        Console.Clear();

                        // on affiche le joueur sur la nouvelle case
                        MaConsole.afficherConsole(Joueurs, JoueurEnCours, Plateau);

                        // on réaffiche les infos du centre
                        MaConsole.afficherCentre();

                        // tant qu'il se déplace, il continue de jouer
                        bool seDeplace = true;
                        while (seDeplace)
                        {
                            // on s'occupe de la case sur laquelle il est tombé
                            CasePlateau caseTombe = Plateau.getCaseFromNum(JoueurEnCours.CaseActuelle);

                            // il a peut être une action à effectuer
                            caseTombe.estTombeSur(this);

                            // après avoir effectué son action liée à la case, il n'a pas bougé
                            if (JoueurEnCours.CaseActuelle == caseTombe.Numero)
                                seDeplace = false;

                            MaConsole.ecrireLigne("");
                        }

                        MaConsole.afficherInfos(JoueurEnCours);

                        //MaConsole.afficherConsole(Joueurs, JoueurEnCours);
                    }
                    // le joueur est en prison !
                    else {
                        JoueurEnCours.jouerTourPrison(Des);
                        
                    }
                    MaConsole.ecrireLigne("");
                    MaConsole.ecrireLigne("Appuyez sur Entrée pour finir votre tour.");
                    MaConsole.lireLigne();

                    Console.Clear();
                    MaConsole.clearCentre();

                }
                compteurTour++;
                MaConsole.hauteurLigne = 0;
            }
        }

        private void initJ()
        {
            Joueurs = new List<Joueur>();
            for (int i = 0; i < 4; i++)
            {
                Joueurs.Add(new Joueur("juju" + i));
                Joueurs[i].deplacerA(Plateau.getCaseFromNum(10), false);
                
            }
            Joueurs[0].entrerPrison();
            /*
            Joueurs[0].ListeProprietes.Add(Plateau.getCaseFromNum(1) as CasePropriete);
            ((CasePropriete)Plateau.getCaseFromNum(1)).setProprietaire(Joueurs[0]);
            Joueurs[0].ListeProprietes.Add(Plateau.getCaseFromNum(3) as CasePropriete);
            ((CasePropriete)Plateau.getCaseFromNum(3)).setProprietaire(Joueurs[0]);
            Joueurs[0].ListeProprietes.Add(Plateau.getCaseFromNum(6) as CasePropriete);
            ((CasePropriete)Plateau.getCaseFromNum(6)).setProprietaire(Joueurs[0]);
            Joueurs[0].ListeProprietes.Add(Plateau.getCaseFromNum(8) as CasePropriete);
            ((CasePropriete)Plateau.getCaseFromNum(8)).setProprietaire(Joueurs[0]);
            Joueurs[0].ListeProprietes.Add(Plateau.getCaseFromNum(9) as CasePropriete);
            ((CasePropriete)Plateau.getCaseFromNum(9)).setProprietaire(Joueurs[0]);*/
        }

        private void initJoueurs()
        {
            Joueurs = new List<Joueur>();

            MaConsole.ecrireLigne("Entrez le nombre de joueurs s'il vous plaît : ");
            int nbJoueurs = int.Parse(MaConsole.lireLigne());
            for (int i = 0; i < nbJoueurs; i++)
            {
                MaConsole.ecrireLigne("Entrez le nom du joueur {0} : ", i + 1);
                String s = MaConsole.lireLigne();
                Joueur J = new Joueur(s);
                Joueurs.Add(J);
            }

            MaConsole.ecrireLigne("Veuillez lancer les dés pour commencer");
            MaConsole.ecrireLigne("Appuyez sur la touche Entrée pour lancer les dés.");
            MaConsole.lireLigne();

            Dictionary<Joueur, int> resultats = new Dictionary<Joueur, int>();

            for (int i = 0; i < nbJoueurs; i++)
            {
                Des.lancerDes();
                int res = Des.de1 + Des.de2;
                MaConsole.ecrireLigne(Joueurs[i].Nom + " a fait " + res + " aux dés !");
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
            MaConsole.ecrireLigne("\n" + finaleTriee[0].Nom + " \n Vous jouez en premier");
            for (int i = 1; i < nbJoueurs; i++)
                MaConsole.ecrireLigne("\n" + finaleTriee[i].Nom + "\n Vous jouez en " + (i + 1) + "ème.");

            MaConsole.lireLigne();
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
