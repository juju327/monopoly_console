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
            private set;
        }

        public bool Vendu
        {
            get;
            private set;
        }

        public bool PrixHypotheque
        {
            get;
            private set;
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
