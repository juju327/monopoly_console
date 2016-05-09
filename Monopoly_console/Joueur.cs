using System.Collections.Generic;

namespace monopoly
{
    class Joueur
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
            throw new System.NotImplementedException();
        }

        public void deplacerDe(int nbCases)
        {
            throw new System.NotImplementedException();
        }

        public void gagner(int Somme)
        {
            throw new System.NotImplementedException();
        }

        public void perdre(int Somme)
        {
            throw new System.NotImplementedException();
        }
    }

}



