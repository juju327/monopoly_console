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

        public bool gagnerArgent
        {
            get;

            protected set;
        }

        

        public ActionArgent(bool gagnerArgent, int s)
        {
            this.gagnerArgent = gagnerArgent;

            if (gagnerArgent)
                executer = gagner;
            else
                executer = perdre;
            Somme = s;
        }

        public void gagner(Joueur j)
        {
            j.gagner(Somme);
        }

        public void perdre(Joueur j)
        {
            j.perdre(Somme);
        }
    }
}
