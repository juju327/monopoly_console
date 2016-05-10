using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using monopoly;

namespace monopoly
{
    // une action peut être un déplacement ou un gain d'argent ou ...
    public class Action
    {
        public Action()
        {
        }

        // un type pour exécuter les actions sur un joueur bénéficiaire
        public delegate void actionAFaire(Joueur j);

        // l'action à faire qui varie en fonction de l'instance créée
        // (peut être un gain d'argent, reculer de 3 cases ...)
        public actionAFaire executer
        {
            get;
            protected set;
        }
    }
}
