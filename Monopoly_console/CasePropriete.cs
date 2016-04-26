using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class CasePropriete : CasePlateau
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

    }
}
