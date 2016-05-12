﻿using System;
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
            return 150;
        }

        public int[] Loyers
        {
            get;
            private set;
        }

        public Terrain(String n, int num, int[] loyers, int c, int prixAchat)
            : base(n, num)
        {
            Loyers = new int[6];
            if (loyers.Count() == Loyers.Count())
                Loyers = loyers;
            Couleur = (Couleur)c;
            PrixAchat = prixAchat;
        }


        public override void estTombeSur(Joueur j)
        {
            if (Proprietaire == null)
            {
                Console.WriteLine("Souhaitez-vous acheter {0} pour {1} ? (o/n)", Nom, PrixAchat);
                String reponse = Console.ReadLine();
                if (reponse == "o")
                {
                    if (j.Argent >= PrixAchat)
                    {
                        // on ajoute la ppté à la liste du joueur
                        j.ListeProprietes.Add(this);

                        // il achète la ppté
                        j.perdre(PrixAchat);

                        // on change son propriétaire
                        Proprietaire = j;

                        Console.WriteLine("Vous avez acheté {0} pour {1} ! ", Nom, PrixAchat);
                    }
                    else
                    {
                        Console.WriteLine("Vous n'avez pas assez d'argent !");
                    }
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas souhaité acheter.");
                }
            }
            else if (Proprietaire != j)
            {
                int loyer = calculeLoyer();
                Console.WriteLine("Vous êtes chez {0}, vous lui devez {1} !", Proprietaire.Nom, loyer);
                if (j.Argent >= loyer)
                {
                    j.perdre(loyer);
                    Console.WriteLine("Vous avez payé {0} de loyer à {1}", Proprietaire.Nom, loyer);
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez d'argent ! Vous avez perdu !");
                }
            }
            else
            {
                Console.WriteLine("Vous êtes chez vous !");
            }
        }

        public override void serialiser(XElement racine)
        {
            XElement c = new XElement("case");

            XElement numero = new XElement("numero", Numero);
            XElement param = new XElement("param");
            param.SetAttributeValue("type", "propriete");
            param.SetAttributeValue("spec", "terrain");
            XElement nom = new XElement("nom", Nom);
            XElement couleur = new XElement("couleur", (int)Couleur);
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

        public static new object deserialiser(XElement racine)
        {
            int numCase = (int)racine.Element("numero");
            String nom = (String)racine.Element("nom");
            int couleur = (int)racine.Element("couleur");
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


            return new Terrain(nom, numCase, loyers, couleur, prixAchat);
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
