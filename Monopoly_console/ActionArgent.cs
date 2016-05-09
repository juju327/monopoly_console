using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionArgent : Action
    {
        delegate void actionAFaire();

        public int Somme
        {
            get;
            private set;
        }

        public ActionArgent(Boolean gagnerArgent)
        {
            actionAFaire executer;
            if (gagnerArgent)
                executer = gagner;
            else
                executer = perdre;
        }

        public void gagner()
        {
            JoueurBeneficiaire.gagner(Somme);
        }

        public void perdre()
        {
            JoueurBeneficiaire.perdre(Somme);
        }
    }
}
