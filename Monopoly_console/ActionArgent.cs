using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionArgent : Action
    {

        public int Somme
        {
            get;
            private set;
        }

        public void gagner()
        {
            Joueur.gagner(Somme);
        }

        public void perdre()
        {
            Joueur.perdre(Somme);
        }
    }
}
