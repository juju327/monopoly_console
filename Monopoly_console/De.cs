using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{

    class De
    {

        public int de1
        {
            get;
            private set;
        }


        public int de2
        {
            get;
            private set;
        }

        private Random Rnd
        {
            get;
            set;
        }

        public De()
        {
            Rnd = new Random();
            de1 = 0;
            de2 = 0;
        }

        public void Lancerde()
        {
            de1 = Rnd.Next(1, 7);
            de2 = Rnd.Next(1, 7);
        }
    }
}
