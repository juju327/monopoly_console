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

        // la case visée par son numéro
        private int numeroCase;

        // constructeur
        public ActionAllerA(CasePlateau dest, bool passerParDepart)
        {
            destination = dest;
            this.passerParDepart = passerParDepart;
        }

        public ActionAllerA(int numero, bool passerParDepart)
        {
            numeroCase = numero;
            this.passerParDepart = passerParDepart;
        }

        // la méthode à exécuter
        public override void executer(Partie p)
        {
            if (destination != null)
                destination = p.Plateau.getCaseFromNum(numeroCase);

            p.JoueurEnCours.deplacerA(destination, passerParDepart);
            destination.estTombeSur(p);
        }
    }
}
