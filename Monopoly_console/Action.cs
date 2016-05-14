using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using monopoly;

namespace monopoly
{
    // une action peut être un déplacement ou un gain d'argent ou ...
    abstract class Action
    {
        // un type pour exécuter les actions sur un joueur bénéficiaire
        public abstract void executer(Partie p);

    }
}
