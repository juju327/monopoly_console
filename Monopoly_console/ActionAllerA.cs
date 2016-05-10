using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    // une classe qui gère les déplacements jusqu'à une certaine case
    public class ActionAllerA : Action
    {
        // la case visée
        public CasePlateau Destination
        {
            get;
            private set;
        }

        // true si on passe par la case départ
        public bool PasserParDepart
        {
            get;
            private set;
        }

        // constructeur
        public ActionAllerA(CasePlateau dest, bool passerParDepart)
        {
            Destination = dest;
            PasserParDepart = passerParDepart;
            executer = allerA;
        }

        // la méthode à exécuter
        private void allerA(Joueur j)
        {
            j.deplacerA(Destination, PasserParDepart);
        }
    }
}
