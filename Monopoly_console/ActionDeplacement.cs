using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionDeplacement : Action
    {
        public ActionDeplacement(Joueur j, CasePlateau dest, bool passerParCaseDepart) : base (j)
        {



            JoueurBeneficiaire.deplacerA(dest, passerParCaseDepart);
        }

        public ActionDeplacement(Joueur j, int nbCases) : base(j)
        {
            JoueurBeneficiaire.deplacerDe(nbCases);
        }
    }
}
