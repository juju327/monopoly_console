using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class Couleur
    {
        public int Numero
        {
            get;
            private set;
        }

        public int prixMaison
        {
            get;
            private set;
        }

        public int prixHotel
        {
            get;
            private set;
        }


        public Couleur(int Num, int prixM, int prixH)
        {
            Numero = Num;
            prixMaison = prixM;
            prixHotel = prixH;

        }

        public enum NomCouleur
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

        /*public NomCouleur GetNombyNumero(int numero)
        {
            return (NomCouleur)numero;
        }*/
    }
}
