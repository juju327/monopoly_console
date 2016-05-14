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

        public bool PossesionTerrainCouleur
        {
            get;
            set;
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
            PossesionTerrainCouleur=false;
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
           
            CaseActuelle=CaseActuelle-nbCases;
        }

        public void gagner(int somme)
        {
            Argent += somme;
        }



        public void VerificationCouleur() //fonction qui vérifie si un joueur possède tous les terrains d'une même
        {
            ;
        }


        public void construire()
        {    
            
            
            Console.WriteLine("Vous possédez les propriétés suivantes :" );
            
            for (int i = 0; i < ListeProprietes.Count; i++)
            {

                Console.WriteLine(ListeProprietes[i].Nom);

                                


            }
            Console.WriteLine("Choississez la couleur sur laquelle vous souhaitez ajouter des maisons");
            int couleur = int.Parse(Console.ReadLine());
            Console.WriteLine(" Sélectionner le nombre de maison que vous souhaitez ajouter \n Appuyer sur 5 si vous souhaitez ajouter un hôtel.");
            int nombre = int.Parse(Console.ReadLine());

           

            
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



