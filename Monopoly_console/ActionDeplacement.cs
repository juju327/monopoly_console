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
        private int nbCases;

        // le constructeur qui initialise la méthode à exécuter
        // et le nombre de cases
        public ActionDeplacement(int nb)
        {
            nbCases = nb;
        }

        // la méthode à exécuter
        public override void executer(Partie p)
        {
            p.JoueurEnCours.deplacerDe(nbCases);
        }

    }
}
