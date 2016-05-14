using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionReparations : Action
    {
        private int prixParMaison;

        private int prixParHotel;

        public ActionReparations(int prixParMaison, int prixParHotel)
        {
            this.prixParMaison = prixParMaison;
            this.prixParHotel = prixParHotel;
        }

        public override void executer(Partie p)
        {
            p.JoueurEnCours.faireReparations(prixParMaison, prixParHotel);
        }
    }
}
