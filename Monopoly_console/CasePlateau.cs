using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    public abstract class CasePlateau : Serialisable
    {
        public String Nom
        {
            get;
            private set;
        }

        public int Numero
        {
            get;
            private set;
        }

        public CasePlateau(String n, int num)
        {
            Nom = n;
            Numero = num;
        }

        public abstract void estTombeSur(Joueur j);

        public override string ToString()
        {
            return String.Format("Case n°{0} : {1}", Numero, Nom);
        }

    }
}
