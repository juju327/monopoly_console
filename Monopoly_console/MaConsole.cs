using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    // Ma console : une console centrale de 35 hauteur x 62 largeur caractères !
    // on y affiche les informations du tour en cours 
    // et des infos sur le joueur en train de jouer
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

        private static int hauteurInterieure = 35;
        private static int largeurInterieure = 62;

        // sauvegarde des évènements qui se déroulent durant un tour
        public static List<String> consoleCentrale
        {
            get;
            set;
        }

        // lit une ligne et renvoit l'input
        public static String lireLigne()
        {
            hauteurLigne++;
            Console.CursorLeft = 10;
            String s = Console.ReadLine();

            return s;
        }

        // nettoie le centre du plateau
        public static void clearCentre()
        {
            consoleCentrale.Clear();
        }

        // affiche les évènements du tour sauvegardés
        public static void afficherCentre()
        {
            hauteurLigne = 0;
            for (int i = 0; i < consoleCentrale.Count; i++)
            {
                ecrireLigneSansSauver(consoleCentrale[i]);
            }
        }

        // écrit une ligne dans la console centrale et la sauvegarde
        public static void ecrireLigne(String phrase, params object[] args)
        {
            // si on a trop écrit dans la console intérieure, on la vide
            if (hauteurLigne > hauteurInterieure - 11)
            {
                clearCentre();
            }
            String nouvelleLigne = String.Format(phrase, args);
            consoleCentrale.Add(nouvelleLigne);
            ecrireLigneSansSauver(nouvelleLigne);
        }

        // écrit une ligne dans la console centrale
        public static void ecrireLigneSansSauver(String phrase, params object[] args)
        {
            // todo gérer la taille des phrases affichées
            Console.CursorLeft = 10;
            Console.CursorTop = 5 + hauteurLigne;

            String nouvelleLigne = String.Format(phrase, args);

            /*while (nouvelleLigne.Count() > largeurInterieur)
            {

            }*/
            Console.WriteLine(nouvelleLigne);

            hauteurLigne++;
            Console.CursorTop = 5 + hauteurLigne;
        }

        // affichage du plateau, des joueurs, des infos des joueurs...
        public static void afficherConsole(List<Joueur> Joueurs, Joueur JoueurEnCours, Plateau plateau)
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
                    afficherSymboleSurConsole(position, i + "", i);
                else
                    afficherSymboleSurConsole(position, i + "", nbJoueursMemeCase - i);
            }

            afficherInfos(JoueurEnCours);
            afficherTexteCases(plateau);
            afficherProprietes(JoueurEnCours);
        }

        // affiche un texte à une position donnée
        public static void afficherTexteSurConsole(int position, string texte, int ligne, ConsoleColor couleurTexte, ConsoleColor couleurFond)
        {
            ConsoleColor tempForeground = Console.ForegroundColor;
            ConsoleColor tempBackground = Console.BackgroundColor;
            Console.ForegroundColor = couleurTexte;
            Console.BackgroundColor = couleurFond;
            afficherTexteSurConsole(position, texte, ligne);
            Console.ForegroundColor = tempForeground;
            Console.BackgroundColor = tempBackground;
        }

        public static void afficherTexteSurConsole(int position, string texte, int ligne)
        {
            if (position < 11)
            {
                Console.CursorTop = ligne;
                Console.CursorLeft = 7 * (position + 1) - 6;
            }
            // colonne de droite
            else if (position > 10 && position < 21)
            {
                Console.CursorTop = ligne + (position - 10) * 3;
                Console.CursorLeft = 71;
            }
            // ligne du bas
            else if (position > 20 && position < 31)
            {
                Console.CursorTop = 30 + ligne;
                Console.CursorLeft = (30 - position) * 7 + 1;
            }
            // ligne de gauche pos < 39
            else
            {
                Console.CursorTop = ligne + (40 - position) * 3;
                Console.CursorLeft = 1;
            }

            Console.WriteLine(texte);
            Console.CursorLeft = 0;
            Console.CursorTop = 6;
        }

        public static void afficherTexteCases(Plateau plateau)
        {
            List<CasePlateau> Cases = plateau.Cases;
            foreach (CasePlateau c in Cases)
            {
                string texteAfficherLigne1 = "";
                string texteAfficherLigne2 = "";
                ConsoleColor couleurTexte = ConsoleColor.Black;
                ConsoleColor couleurFond = ConsoleColor.White;
                texteAfficherLigne1 = c.Nom.Substring(0, 6);
                if (c is Terrain)
                {
                    Terrain terrain = c as Terrain;
                    int nbMaisons = terrain.NbMaisonsConstruites;

                    for (int i = 0; i < nbMaisons; i++)
                        texteAfficherLigne2 += "M";

                    if (terrain.NbHotelsConstruits > 0)
                        texteAfficherLigne2 = "  H";

                    couleurTexte = ConsoleColor.White;
                    switch (terrain.Couleur.Numero)
                    {
                        case 0: // violet
                            couleurFond = ConsoleColor.DarkMagenta;
                            break;
                        case 1: // bleu ciel
                            couleurTexte = ConsoleColor.Black;
                            couleurFond = ConsoleColor.Cyan;
                            break;
                        case 2: // rose
                            couleurFond = ConsoleColor.Magenta;
                            break;
                        case 3: // orange
                            couleurFond = ConsoleColor.DarkYellow;
                            break;
                        case 4: // jaune
                            couleurFond = ConsoleColor.Yellow;
                            couleurTexte = ConsoleColor.Black;
                            break;
                        case 5: // rouge
                            couleurFond = ConsoleColor.Red;
                            break;
                        case 6: // vert
                            couleurFond = ConsoleColor.DarkGreen;
                            break;
                        case 7: // bleu foncé
                            couleurFond = ConsoleColor.Blue;
                            break;
                    }
                }
                else if (c is Gare)
                {
                    couleurFond = ConsoleColor.DarkGray;
                    couleurTexte = ConsoleColor.White;
                }
                else if (c is Compagnie)
                {
                    couleurFond = ConsoleColor.Gray;
                    couleurTexte = ConsoleColor.Black;
                }
                else if (c is CasePioche)
                {
                    couleurFond = ConsoleColor.Gray;
                    couleurTexte = ConsoleColor.DarkRed;
                }
                else if (c is CaseSpeciale)
                {
                    if (c.Numero == 20) // parc gratuit
                        texteAfficherLigne2 = plateau.ParcGratuit + "€";
                    couleurFond = ConsoleColor.White;
                    couleurTexte = ConsoleColor.DarkGray;
                }

                // si ça n'est pas un terrain ou pas de maisons construites,
                // on peut afficher plus d'infos !
                if (texteAfficherLigne2 == "")
                {
                    texteAfficherLigne2 = c.Nom.Length > 12 ? c.Nom.Substring(6, 6) : c.Nom.Substring(6);
                    afficherTexteSurConsole(c.Numero, texteAfficherLigne2, 2, couleurTexte, couleurFond);
                }
                else
                    afficherTexteSurConsole(c.Numero, texteAfficherLigne2, 2);

                afficherTexteSurConsole(c.Numero, texteAfficherLigne1, 1, couleurTexte, couleurFond);
            }
        }

        // affiche un symbole à une position donnée (sans superposition des symboles)
        public static void afficherSymboleSurConsole(int position, string symbole, int indiceCase)
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

            Console.WriteLine(symbole);
            Console.CursorLeft = 0;
            Console.CursorTop = 6;
        }

        // affiche le plateau de jeu
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

        // affiche une ligne (différente si c'est celle du haut ou du bas)
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

        // affiche une case en fonction de l'endroit indiqué
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

        // affiche les informations du joueur donné
        public static void afficherInfos(Joueur j)
        {
            int tmpHauteur = hauteurLigne;

            hauteurLigne = 22;
            ecrireLigneSansSauver("Solde {0}€ --- Nombre de propriétés : {1}", j.Argent, j.ListeProprietes.Count);
            ecrireLigneSansSauver("Cartes \"Libéré de Prison\" : {0}", j.NbCarteLiberation);

            if (j.NbTourPrison > 0)
            {
                Console.CursorLeft = 45;
                Console.CursorTop = 30;
                ecrireLigneSansSauver("Nombre de tours en prison : {0}", j.NbTourPrison);
            }

            hauteurLigne = tmpHauteur;
        }

        public static void afficherProprietes(Joueur j)
        {
            Console.CursorLeft = 80;
            Console.WriteLine("Propriétés possédées :");
            Console.WriteLine(" ");

            foreach (CasePropriete prop in j.ListeProprietes)
            {
                Console.CursorLeft = 80;
                Console.WriteLine(" > " + prop.Nom);
            }
        }
    }
}
