using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionCarteLiberePrison : Action
    {
        // le constructeur qui initialise la méthode à exécuter
        // et le nombre de cases
        public ActionCarteLiberePrison(bool carteSupplementaire)
        {
            if (carteSupplementaire)
                executer = ajouterCarte;
            else
                executer = utiliserCarte;
        }

        // la méthode à exécuter
        private void ajouterCarte(Joueur j)
        {
            j.ajouterCarteLiberePrison();
        }

        private void utiliserCarte(Joueur j)
        {
            j.utiliserCarteLiberePrison();
        }
    }
}
