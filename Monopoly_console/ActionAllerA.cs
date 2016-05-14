using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    // une classe qui gère les déplacements jusqu'à une certaine case
    class ActionAllerA : Action
    {
        // la case visée
        private CasePlateau destination;

        // true si on passe par la case départ
        private bool passerParDepart;

        // constructeur
        public ActionAllerA(CasePlateau dest, bool passerParDepart)
        {
            destination = dest;
            this.passerParDepart = passerParDepart;
        }

        // la méthode à exécuter
        public override void executer(Partie p)
        {
            p.JoueurEnCours.deplacerA(destination, passerParDepart);
            destination.estTombeSur(p);
        }
    }
}
