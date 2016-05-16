using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionArgent : Action
    {
        private int Somme;

        private bool GagnerArgent;

        public ActionArgent(bool gagnerArgent, int s)
        {
            GagnerArgent = gagnerArgent;
            Somme = s;
        }

        public override void executer(Partie p)
        {
            if (GagnerArgent)
            {
                MaConsole.ecrireLigne("Vous gagnez {0}€ !", Somme);
                p.JoueurEnCours.gagner(Somme);
            }
            else
            {
                MaConsole.ecrireLigne("Vous perdez {0}€ ", Somme);
                p.JoueurEnCours.perdre(Somme);

                p.Plateau.ParcGratuit += Somme;
                MaConsole.ecrireLigne("Le parc gratuit compte désormais {0}€ !", p.Plateau.ParcGratuit);
            }
        }
    }
}
