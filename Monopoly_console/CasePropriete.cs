using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    abstract class CasePropriete : CasePlateau
    {
        public int PrixAchat
        {
            get;
            protected set;
        }

        public bool Vendu
        {
            get;
            protected set;
        }

        public bool PrixHypotheque
        {
            get;
            protected set;
        }

        public Joueur Proprietaire
        {
            get;
            protected set;
        }

        public virtual int calculeLoyer()
        {
            return 0;
        }

        public CasePropriete(String n, int num)
            : base(n, num)
        {

        }

        public override void estTombeSur(Partie partie)
        {
            Joueur j = partie.JoueurEnCours;
            if (Proprietaire == null)
            {
                MaConsole.ecrireLigne("Souhaitez-vous acheter : ");
                MaConsole.ecrireLigne(" > {0} pour {1}€ ? (o/n)", Nom, PrixAchat);
                String reponse = MaConsole.lireLigne();
                if (reponse == "o")
                {
                    if (j.Argent >= PrixAchat)
                    {
                        j.ListeProprietes.Add(this);
                        j.perdre(PrixAchat);
                        Proprietaire = j;
                        MaConsole.ecrireLigne("Vous avez acheté : ");
                        MaConsole.ecrireLigne(" > {0} pour {1}€ ! ", Nom, PrixAchat);
                    }
                    else
                    {
                        MaConsole.ecrireLigne("Vous n'avez pas assez d'argent !");
                    }
                }
                else
                {
                    MaConsole.ecrireLigne("Vous n'avez pas souhaité acheter.");
                }
            }
            else if (Proprietaire != j)
            {
                int loyer = calculeLoyer();
                MaConsole.ecrireLigne("Vous êtes chez {0}, vous lui devez {1}€ !", Proprietaire.Nom, loyer);
                if (j.Argent >= loyer)
                {
                    j.perdre(loyer);
                    MaConsole.ecrireLigne("Vous avez payé {0}€ de loyer à {1}", loyer, Proprietaire.Nom);
                }
                else
                    MaConsole.ecrireLigne("Vous n'avez pas assez d'argent ! Vous avez perdu !");

            }
            else
                MaConsole.ecrireLigne("Vous êtes chez vous !");
        }

        public void setProprietaire(Joueur j)
        {
            Proprietaire = j;
        }
    }

}
