using System;
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

        public void faireReparations(int prixParMaison, int prixParHotel)
        {
            int totalAPayer = 0;
            foreach (CasePropriete caseProp in ListeProprietes)
                if (caseProp is Terrain)
                {
                    Terrain t = caseProp as Terrain;
                    totalAPayer += t.NbMaisonsConstruites * prixParMaison + t.NbHotelsConstruits * prixParHotel;
                }
            perdre(totalAPayer);
        }

        public bool EstEnPrison
        {
            get;
            set;
        }

        public void ajouterCarteLiberePrison()
        {
            NbCarteLiberation++;
        }

        public void utiliserCarteLiberePrison()
        {
            NbCarteLiberation--;
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

        public void allerEnPrison()
        {
            EstEnPrison = true;
            NbTourPrison = 0;
        }

        public void passerUnTourPrison()
        {
            NbTourPrison++;
            if (NbTourPrison == 3)
                sortirPrison();

        }

        public void deplacerDe(int nbCases)
        {
            throw new System.NotImplementedException();
        }

        public void gagner(int somme)
        {
            Argent += somme;
        }

        public bool aTerrainConstructible()
        {
            foreach (CasePropriete propriete in ListeProprietes)
                if (propriete is Terrain)
                {
                    Terrain terrain = propriete as Terrain;
                    if (terrain.estConstructible())
                        return true;
                }
            return false;
        }

        public void construire()
        {
            MaConsole.ecrireLigneSansSauver("Vous possédez les propriétés constructibles suivantes :");

            // pour l'affichage et le choix de l'utilisateur
            int nbProp = 1;
            List<Terrain> terrainsConstructibles = new List<Terrain>();
            foreach (CasePropriete propriete in ListeProprietes)
            {
                if (propriete is Terrain)
                {
                    Terrain terrain = propriete as Terrain;
                    if (terrain.estConstructible())
                    {
                        terrainsConstructibles.Add(terrain);
                        MaConsole.ecrireLigneSansSauver("{0} - {1} ({2})", nbProp, terrain.Nom, terrain.Couleur.Nom);
                        String constructions = "   -> {0} maisons ({1}€/M) et {2} hôtel({3}€/H) construits";
                        MaConsole.ecrireLigneSansSauver(constructions, terrain.NbMaisonsConstruites, terrain.Couleur.prixMaison,
                            terrain.NbHotelsConstruits, terrain.Couleur.prixHotel);
                        nbProp++;
                    }
                }
            }

            MaConsole.ecrireLigneSansSauver("Choisissez la propriété sur laquelle vous souhaitez ");
            MaConsole.ecrireLigneSansSauver("construire en entrant son numéro : ");
            int nb = int.Parse(MaConsole.lireLigne());
            MaConsole.hauteurLigne++;

            Terrain aConstruire = terrainsConstructibles[nb - 1];
            if (aConstruire.peutConstruireMaison())
            {
                MaConsole.ecrireLigne("Vous avez acheté une maison pour {0}€ sur le terrain ", aConstruire.Couleur.prixMaison);
                MaConsole.ecrireLigne("{0}", aConstruire.Nom);
                perdre(aConstruire.Couleur.prixMaison);
                aConstruire.construireMaison();
            }
            else if (aConstruire.peutConstruireHotel())
            {
                MaConsole.ecrireLigne("Vous avez acheté un hôtel pour {0}€ sur le terrain ", aConstruire.Couleur.prixHotel);
                MaConsole.ecrireLigne("{0}", aConstruire.Nom);
                perdre(aConstruire.Couleur.prixHotel);
                aConstruire.construireHotel();
            }
            else
            {
                MaConsole.ecrireLigne("Vous ne pouvez rien construire sur cette propriété");
                MaConsole.ecrireLigne("pour l'instant.");
            }

        }

        public void perdre(int somme)
        {
            if (Argent >= somme) Argent -= somme;
            else hypothequer(somme);
        }

        public void deplacer(int resultatsDes, Plateau plateau)
        {
            MaConsole.ecrireLigne("Veuillez appuyer sur Entrée pour lancer les dés.");
            MaConsole.lireLigne();

            MaConsole.ecrireLigne("Votre avez obtenu {0} aux dés.", resultatsDes);

            // on fait avancer le joueur
            int position = CaseActuelle;
            if (position + resultatsDes < plateau.Cases.Count)
            {
                CasePlateau destination = plateau.getCaseFromNum(resultatsDes + position);
                deplacerA(destination, true);
            }
            else
            {
                CasePlateau destination = plateau.getCaseFromNum(resultatsDes + position - plateau.Cases.Count);
                deplacerA(destination, true);
            }
        }

        public void jouerTourPrison(Des des)
        {
            passerUnTourPrison();
            if (EstEnPrison)
            {
                MaConsole.ecrireLigne("Vous êtes en prison depuis {0} tours !", NbTourPrison);
                MaConsole.ecrireLigne("Pour sortir de prison, vous pouvez tenter de ");
                MaConsole.ecrireLigne("faire un double aux dés ou utiliser une de ");
                MaConsole.ecrireLigne("vos éventuelles cartes \"Libéré de Prison\" !");
                MaConsole.ecrireLigne("Au bout de 3 tours, vous sortez de prison et ");
                MaConsole.ecrireLigne("pouvez jouer normalement.");
                MaConsole.ecrireLigne("Entrez 1 pour utiliser une carte \"Libéré de Prison\"");
                MaConsole.ecrireLigne("Entrez 2 pour tenter de sortir avec un double.");

                int reponse = int.Parse(MaConsole.lireLigne());
                if (reponse == 1 && NbCarteLiberation > 0)
                {
                    utiliserCarteLiberePrison();
                    MaConsole.ecrireLigne("Vous utilisez une carte \"Libéré de Prison\" !");
                    sortirPrison();
                }
                else if (reponse == 1 && NbCarteLiberation == 0)
                {
                    MaConsole.ecrireLigne("Vous n'avez pas de carte \"Libéré de Prison\".");
                }

                // s'il n'a pas de carte, il tire aussi aux dés !
                if (reponse == 2 || (reponse == 1 && NbCarteLiberation == 0))
                {
                    MaConsole.ecrireLigne("Vous lancez les dés pour faire un double...");
                    des.lancerDes();
                    MaConsole.ecrireLigne("Vous avez fait {0} et {1} aux dés", des.de1, des.de2);
                    if (des.isDouble())
                    {
                        MaConsole.ecrireLigne("Vous avez fait un double !");
                        MaConsole.ecrireLigne("Vous sortez de prison !");
                        sortirPrison();
                    }
                    else
                    {
                        MaConsole.ecrireLigne("Dommage... Pas de double...");
                        MaConsole.ecrireLigne("Il vous reste {0} tours à passer en prison !", 3 - NbTourPrison);
                    }
                }
            }
            else
            {
                MaConsole.ecrireLigne("Vous avez passé 3 tours en prison !");
                MaConsole.ecrireLigne("Ouste ! Retour à la vraie vie !");
            }
        }

        public void sortirPrison()
        {
            EstEnPrison = false;
            NbTourPrison = 0;
        }

        public void entrerPrison()
        {
            EstEnPrison = true;
            NbTourPrison = 0;
        }

        // tente d'hypothéquer pour rassembler la somme en paramètre
        public void hypothequer(int dette)
        {
            throw new NotImplementedException();
        }
    }

}



