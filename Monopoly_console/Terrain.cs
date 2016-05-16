using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Terrain : CasePropriete
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

        public override int calculeLoyer()
        {
            return 150;
        }

        public int[] Loyers
        {
            get;
            private set;
        }

        private int nbMaisonsMax = 4;
        private int nbHotelsMax = 1;

        public Terrain(String n, int num, int[] loyers, Couleur couleur, int prixAchat)
            : base(n, num)
        {
            Loyers = new int[6];
            if (loyers.Count() == Loyers.Count())
                Loyers = loyers;
            Couleur = couleur;
            PrixAchat = prixAchat;
            couleur.ajouteTerrain(this);
        }

        // un terrain est constructible si le propriétaire possède 
        // tous les terrains d'une même couleur
        // soit : les terrains d'une couleur ont tous le même propriétaire
        public bool estConstructible()
        {
            Joueur prop = Proprietaire;
            if (prop == null)
                return false;
            foreach (Terrain terrain in Couleur.Terrains)
                if (terrain.Proprietaire != prop)
                    return false;

            return true;
        }

        public override void serialiser(XElement racine)
        {
            XElement c = new XElement("case");

            XElement numero = new XElement("numero", Numero);
            XElement param = new XElement("param");
            param.SetAttributeValue("type", "propriete");
            param.SetAttributeValue("spec", "terrain");
            XElement nom = new XElement("nom", Nom);
            XElement couleur = new XElement("couleur", Couleur.Numero);
            XElement achat = new XElement("achat", PrixAchat);
            XElement loyers = new XElement("loyer");
            for (int i = 0; i < 6; i++)
            {
                XElement loyer = new XElement("loyer" + i, Loyers[i]);
                loyers.Add(loyer);
            }

            c.Add(numero, param, nom, couleur, achat, loyers);
            Console.WriteLine(c);
            //racine.Add(c);
        }

        public void construireMaison()
        {
            NbMaisonsConstruites++;
        }

        public void construireHotel()
        {
            // enlever les maisons et ajouter un hôtel
            NbMaisonsConstruites = 0;
            NbHotelsConstruits = 1;
        }

        public bool peutConstruireMaison()
        {
            // on peut alors mettre un hôtel
            if (NbMaisonsConstruites == nbMaisonsMax) return false;

            if (NbHotelsConstruits == nbHotelsMax) return false;

            // on ne peut pas rajouter une maison si il y en
            // a déjà une de plus que les autres terrains
            foreach (Terrain terrain in Couleur.Terrains)
                if (terrain.NbMaisonsConstruites < NbMaisonsConstruites)
                    return false;

            return true;
        }

        public bool peutConstruireHotel()
        {
            return NbMaisonsConstruites == nbMaisonsMax && NbHotelsConstruits < nbHotelsMax;
        }

        public static new object deserialiser(XElement racine)
        {
            int numCase = (int)racine.Element("numero");
            String nom = (String)racine.Element("nom");
            int numCouleur = (int)racine.Element("couleur");
            int prixAchat = (int)racine.Element("achat");

            int[] loyers = new int[6];
            var loyersXML = from e in racine.Descendants("loyer").Elements()
                            select e;
            int j = 0;
            foreach (XElement loyer in loyersXML)
            {
                loyers[j] = (int)loyer;
                j++;
            }

            Couleur couleur = Couleur.getCouleurByNumero(numCouleur);

            Terrain terrain = new Terrain(nom, numCase, loyers, couleur, prixAchat);



            return terrain;
        }
    }


}
