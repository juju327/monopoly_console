using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Couleur : Serialisable
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

        public int nbTerrains
        {
            get;
            private set;
        }

        public String Nom
        {
            get;
            private set;
        }

        public Couleur(int Num, String nom, int prixM, int prixH)
        {
            Terrains = new List<Terrain>();
            Numero = Num;
            prixMaison = prixM;
            prixHotel = prixH;
            Nom = nom;
        }

        public List<Terrain> Terrains
        {
            get;
            private set;
        }

        public void ajouteTerrain(Terrain terrain)
        {
            Terrains.Add(terrain);
        }

        public static List<Couleur> Couleurs
        {
            get;
            set;
        }

        public static new object deserialiser(XElement racine)
        {
            int numero = (int)racine.Attribute("numero");

            String nom = (String)racine.Attribute("nom");

            int prixParMaison = (int)racine.Element("maisons");
            int prixParHotel = (int)racine.Element("hotel");

            return new Couleur(numero, nom, prixParMaison, prixParHotel);
        }

        public static Couleur getCouleurByNumero(int numCouleur)
        {
            foreach (Couleur couleur in Couleurs)
            {
                if (couleur.Numero == numCouleur)
                    return couleur;
            }
            return null;
        }
    }
}
