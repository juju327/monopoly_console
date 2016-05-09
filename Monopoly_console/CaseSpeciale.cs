using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class CaseSpeciale : CasePlateau, Serialisable
    {
        public Action ActionAEffectuer
        {
            get;
            private set;
        }

        public CaseSpeciale(String n, int num, Action action) : base(n, num)
        {

        }

    }
}
