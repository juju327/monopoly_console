using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class Terrain : CasePropriete, Serialisable
    {
        public Couleur Couleur
        {
            get;
            private set;
        }

        public int NbMaisonsConstruites
        {
            get;
            private set;
        }

        public int NbHotelsConstruits
        {
            get;
            private set;
        }

        public int PrixParMaison
        {
            get;
            private set;
        }

        public int PrixParHotel
        {
            get;
            private set;
        }

        public override int calculeLoyer()
        {
            return base.calculeLoyer();
        }

        public int[] Loyers
        {
            get;
            private set;
        }

        public Terrain(String n, int num, int[] loyers, int c)
            : base(n, num)
        {
            Loyers = new int[6];
            if (loyers.Count() == Loyers.Count())
                Loyers = loyers;
            Couleur = (Couleur)c;
        }
    }

    enum Couleur
    {
        Rose,
        BleuCiel,
        Violet,
        Orange,
        Rouge,
        Jaune,
        Vert,
        Bleu
    }
}
