using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using monopoly;

namespace monopoly
{
    class Action
    {
        public Joueur JoueurBeneficiaire
        {
            get;
            protected set;
        }

        public Action(Joueur j)
        {
            JoueurBeneficiaire = j;
        }
    }
}
