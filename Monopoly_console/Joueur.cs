using System;
using System.Collections.Generic;

namespace monopoly
{
    public class Joueur
    {
        public string Nom
        {
            get;
            private set;
        }

        public int Argent
        {
            get;
            private set;
        }

        public int CaseActuelle
        {
            get;
            private set;
        }

        public int NbCarteLiberation
        {
            get;
            private set;
        }

        public void ajouterCarteLiberePrison()
        {
            NbCarteLiberation++;
        }

        public void utiliserCarteLiberePrison()
        {
            NbCarteLiberation--;
        }

        public int NbTourPrison
        {
            get;
            private set;
        }

        public List<CasePropriete> ListeProprietes
        {
            get;
            private set;
        }

        public Joueur(string n)
        {
            Nom = n;
            CaseActuelle = 0;
            Argent = 1500;
            NbCarteLiberation = 0;
            NbTourPrison = 0;
            ListeProprietes = new List<CasePropriete>();
        }


        public void deplacerA(CasePlateau dest, bool passerParCaseDepart)
        {
            CaseActuelle = dest.Numero;

            // si on passe par la case départ
            if (passerParCaseDepart & dest.Numero < CaseActuelle)
                gagner(200);

            // si on s'arrête sur la case départ
            if (dest.Numero == 0)
                gagner(200);
        }

        public void deplacerDe(int nbCases)
        {
            throw new System.NotImplementedException();
        }

        public void gagner(int somme)
        {
            Argent += somme;
        }

        public void perdre(int somme)
        {
            if (Argent >= somme) Argent -= somme;
            else hypothequer(somme);
        }

        // tente d'hypothéquer pour rassembler la somme en paramètre
        public void hypothequer(int dette)
        {
            throw new NotImplementedException();
        }
    }

}



