using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class Compagnie : CasePropriete
    {
        public static int PrixAchatCompagnie { get; set; }

        public Compagnie(String nom, int num)
            : base(nom, num)
        {
            PrixAchat = PrixAchatCompagnie;
        }

        public override void serialiser(XElement racine)
        {
            XElement c = new XElement("case");

            XElement numero = new XElement("numero", Numero);
            XElement param = new XElement("param");
            param.SetAttributeValue("type", "propriete");
            param.SetAttributeValue("spec", "compagnie");
            XElement nom = new XElement("nom", Nom);

            c.Add(numero, param, nom);
            Console.WriteLine(c);
            //racine.Add(c);
        }


        public new static object deserialiser(XElement racine)
        {
            int numCase = (int)racine.Element("numero");
            String nomCase = (String)racine.Element("nom");
            //int achat = (int)racine.Element("achat");

            return new Compagnie(nomCase, numCase);
        }

        public override int calculeLoyer()
        {
            return 100;
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
                        j.ListeProprietes.Add(this);
                        j.perdre(PrixAchat);
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
    }
}
