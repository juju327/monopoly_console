using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    // une classe qui gère les déplacements d'un nombre précis de cases
    class ActionDeplacement : Action
    {
        // le nombre de cases à bouger
        public int NbCases
        {
            get;
            private set;
        }

        // le constructeur qui initialise la méthode à exécuter
        // et le nombre de cases
        public ActionDeplacement(int nb)
        {
            executer = deplacerDeNbCases;
            NbCases = nb;
        }

        // la méthode à exécuter
        private void deplacerDeNbCases(Joueur j)
        {
            j.deplacerDe(NbCases);
        }

    }
}
