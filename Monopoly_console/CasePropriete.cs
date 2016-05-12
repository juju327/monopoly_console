using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    public abstract class CasePropriete : CasePlateau
    {
        public int PrixAchat
        {
            get;
            protected set;
        }

        public bool Vendu
        {
            get;
            protected set;
        }

        public bool PrixHypotheque
        {
            get;
            protected set;
        }

        public Joueur Proprietaire
        {
            get;
            protected set;
        }

        public virtual int calculeLoyer()
        {
            return 0;
        }

        public CasePropriete(String n, int num)
            : base(n, num)
        {

        }

    }
}
