using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    static class MaConsole
    {
        static MaConsole()
        {
            consoleCentrale = new List<String>();
        }

        public static int hauteurLigne
        {
            get;
            set;
        }

        private static int largeurInterieur = 60;

        public static List<String> consoleCentrale
        {
            get;
            set;
        }

        public static String lireLigne()
        {
            hauteurLigne++;
            Console.CursorLeft = 10;
            String s = Console.ReadLine();

            return s;
        }

        public static void clearCentre()
        {
            consoleCentrale.Clear();
        }

        public static void afficherCentre()
        {
            hauteurLigne = 0;
            for (int i = 0; i < consoleCentrale.Count; i++)
            {
                ecrireLigneSansSauver(consoleCentrale[i]);
            }
        }

        public static void ecrireLigne(String phrase, params object[] args)
        {
            String nouvelleLigne = String.Format(phrase, args);
            consoleCentrale.Add(nouvelleLigne);
            ecrireLigneSansSauver(nouvelleLigne);
        }

        public static void ecrireLigneSansSauver(String phrase, params object[] args)
        {
            // todo gérer la taille des phrases affichées
            Console.CursorLeft = 10;
            Console.CursorTop = 6 + hauteurLigne;

            String nouvelleLigne = String.Format(phrase, args);

            /*while (nouvelleLigne.Count() > largeurInterieur)
            {

            }*/
            Console.WriteLine(nouvelleLigne);

            hauteurLigne++;
            Console.CursorTop = 6 + hauteurLigne;
        }

        // affichage des infos !
        public static void afficherConsole(List<Joueur> Joueurs, Joueur JoueurEnCours)
        {
            afficherPlateau();

            for (int i = 0; i < Joueurs.Count; i++)
            {
                int position = Joueurs[i].CaseActuelle;

                int nbJoueursMemeCase = 0;
                for (int j = 0; j < Joueurs.Count; j++)
                    if (Joueurs[j].CaseActuelle == position)
                        nbJoueursMemeCase++;
                if (nbJoueursMemeCase > 0)
                    afficherJoueurSurConsole(position, i, i);
                else
                    afficherJoueurSurConsole(position, i, nbJoueursMemeCase - i);

            }

            afficherInfos(JoueurEnCours);
        }

        public static void afficherJoueurSurConsole(int position, int numJoueur, int indiceCase)
        {
            indiceCase++;
            if (position < 11)
            {
                Console.CursorTop = 3;
                Console.CursorLeft = 7 * (position + 1) - indiceCase - 2;
            }
            // colonne de droite
            else if (position > 10 && position < 21)
            {
                Console.CursorTop = 3 + (position - 10) * 3;
                Console.CursorLeft = 70 + indiceCase;
            }
            // ligne du bas
            else if (position > 20 && position < 31)
            {
                Console.CursorTop = 33;
                Console.CursorLeft = indiceCase + (30 - position) * 7;
            }
            // ligne de gauche pos < 39
            else
            {
                Console.CursorTop = 3 + (40 - position) * 3;
                Console.CursorLeft = indiceCase;
            }

            Console.WriteLine(numJoueur);
            Console.CursorLeft = 0;
            Console.CursorTop = 6;
        }

        private static void afficherPlateau()
        {
            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    afficherLigne(false);
                else if (i == 10)
                    afficherLigne(true);
                else
                {
                    afficherCase(i * 3 + 1, 0);
                    afficherCase(i * 3 + 1, 70);
                }
            }
        }

        private static void afficherLigne(bool ligneDuBas)
        {
            if (ligneDuBas)
            {
                Console.CursorTop -= 1;
                Console.WriteLine("|______|______ ______ ______ ______ ______ ______ ______ ______ ______|______|");
            }
            else {

                for (int i = 0; i < 11; i++)
                    Console.Write(" ______");
                Console.WriteLine();
            }

            for (int i = 0; i < 11; i++)
            {
                Console.Write("|      ");

            }
            Console.WriteLine("|");

            Console.CursorTop -= 1;
            for (int i = 0; i < 11; i++)
            {
                Console.CursorLeft += 1;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("      ");
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine();



            for (int i = 0; i < 11; i++)
                Console.Write("|      ");
            Console.WriteLine("|");

            for (int i = 0; i < 11; i++)
                Console.Write("|______");
            Console.WriteLine("|");
        }

        private static void afficherCase(int top, int left)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.Write("|      |");
            Console.CursorLeft = left;
            Console.CursorTop = top + 1;
            Console.Write("|      |");
            Console.CursorLeft = left;
            Console.CursorTop = top + 2;
            Console.Write("|______|");
            Console.WriteLine();

        }

        public static void afficherInfos(Joueur j)
        {
            int tmpHauteur = hauteurLigne;

            hauteurLigne = 20;
            ecrireLigneSansSauver("Solde {0} :  --- Nombre de propriétés : {1}", j.Argent, j.ListeProprietes.Count);
            ecrireLigneSansSauver("Cartes \"Libéré de Prison\" : {0}", j.NbCarteLiberation);

            if (j.NbTourPrison > 0)
            {
                Console.CursorLeft = 45;
                Console.CursorTop = 30;
                ecrireLigneSansSauver("Nombre de tours en prison : {0}", j.NbTourPrison);
            }

            hauteurLigne = tmpHauteur;
        }

    }
}
