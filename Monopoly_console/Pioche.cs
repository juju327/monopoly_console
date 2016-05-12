﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Pioche
    {
        public static Plateau Plateau
        {
            get;
            private set;
        }

        public List<CartePioche> Cartes
        {
            get;
            private set;
        }

        public Pioche(Plateau p, XElement racine)
        {
            Plateau = p;
            initCartes(racine);
        }

        public CartePioche piocher()
        {
            // on pioche la carte
            CartePioche c = Cartes.First();

            // on l'enlève donc de la pioche
            Cartes.Remove(c);

            // on la remet à la fin du paquet
            Cartes.Add(c);
            return c;
        }

        public void melanger()
        {

        }

        public static new Object deserialiser(XElement racine)
        {
            List<CartePioche> list = new List<CartePioche>();

            //Run query
            // on récupère tous les éléments cartes du document
            var result = from e in racine.Elements()
                         select e;


            foreach (XElement x in result)
            {
                //Console.WriteLine(x);

                String type = (String)x.Attribute("type");
                String spec = (String)x.Attribute("spec");

                String nom = (String)x.Element("nom");
                Action action;
                //Console.WriteLine(nom);
                CartePioche c = null;

                switch (type)
                {
                    case "argent":
                        int somme = (int)x.Element("somme");
                        bool gagnerArgent;
                        if (spec == "gagner")
                            gagnerArgent = true;
                        else gagnerArgent = false;

                        action = new ActionArgent(gagnerArgent, somme);
                        c = new CartePioche(action, nom, Plateau);
                        break;
                    case "speciale":
                        if (spec == "reparations")
                        {
                            // 25 M  / 100 H
                        }
                        else if (spec == "reparations_voirie")
                        {
                            // 40 M / 115 H
                        }
                        else if (spec == "prison")
                        {
                            action = new ActionCarteLiberePrison(true);
                            c = new CartePioche(action, nom, Plateau);
                        }
                        else if (spec == "anniversaire")
                        {
                            //action = new Action();
                            //action.executer = Plateau.Partie.anniversaire;
                        }
                        else if (spec == "payer_ou_tirer")
                        {

                        }
                        break;
                    case "deplacement":
                        // soit le numero d'une case, soit un nombre de case à se déplacer
                        int num = (int)x.Element("param");
                        Action act = null;

                        if (spec == "destination")
                        {
                            CasePlateau destination = Plateau.getCaseFromNum(num);
                            //  si c'est la case prison la direction, on ne passe pas par la case depart !
                            bool passerParDepart = num == 10 ? false : true;
                            act = new ActionAllerA(destination, passerParDepart);
                        }
                        else if (spec == "nbcases")
                        {
                            act = new ActionDeplacement(num);
                        }

                        c = new CartePioche(act, nom, Plateau);
                        break;
                }

                list.Add(c);

            }
            return list;
        }

        private void initCartes(XElement racine)
        {
            Cartes = (List<CartePioche>)Pioche.deserialiser(racine);
        }
    }
}
