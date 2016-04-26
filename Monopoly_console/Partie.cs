using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class Partie
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
            Console.Write("Entrez le nombre de joueurs : ");
            int nbJoueurs = int.Parse(Console.ReadLine());
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.Write("\nEntrez le nom du joueur {0} : ", i+1);
                String s = Console.ReadLine();
            }
        }
    }
}
