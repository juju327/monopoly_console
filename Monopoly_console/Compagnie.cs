using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class Compagnie : CasePropriete, Serialisable
    {
        public Compagnie(String nom, int num) : base(nom, num)
        {

        }

    }
}
