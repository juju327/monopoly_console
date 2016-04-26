using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionDeplacement : Action
    {
        public ActionDeplacement(CasePlateau dest, bool passerParCaseDepart)
        {
            Joueur.deplacerA(dest, passerParCaseDepart);
        }

        public ActionDeplacement(int nbCases)
        {
            Joueur.deplacerDe(nbCases);
        }
    }
}
