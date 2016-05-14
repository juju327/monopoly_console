using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionCarteLiberePrison : Action
    {
        private bool carteSupplementaire;

        // le constructeur qui initialise la méthode à exécuter
        // et le nombre de cases
        public ActionCarteLiberePrison(bool carteSupplementaire)
        {
            this.carteSupplementaire = carteSupplementaire;
        }

        public override void executer(Partie p)
        {
            if (carteSupplementaire)
                p.JoueurEnCours.ajouterCarteLiberePrison();
            else
                p.JoueurEnCours.utiliserCarteLiberePrison();

        }
    }
}
