using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class ActionPartie : Action
    {
        private String choix;

        public ActionPartie(String arg)
        {
            choix = arg;
        }

        public override void executer(Partie p)
        {
            switch (choix)
            {
                case "anniversaire":

                    int somme = 0;
                    foreach (Joueur joueur in p.Joueurs)
                    {
                        if (joueur != p.JoueurEnCours)
                        {
                            joueur.perdre(10);
                            somme += 10;
                        }
                    }

                    MaConsole.ecrireLigne("Vous avez gagné {0}€ !", somme);
                    p.JoueurEnCours.gagner(somme);

                    break;


                case "payer_ou_tirer":
                    MaConsole.ecrireLigne("Que choisissez vous de faire ?");
                    MaConsole.ecrireLigne("1 pour payer 10€ ; 2 pour tirer une carte Chance");
                    String rep = MaConsole.lireLigne();

                    if (rep == "1")
                    {
                        MaConsole.ecrireLigne("Vous perdez 10€.");
                    }
                    else if (rep == "2")
                    {
                        CartePioche c = p.CartesChance.piocher();
                        MaConsole.ecrireLigne("Vous piochez la carte :");
                        MaConsole.ecrireLigne(c.Description);
                        c.Action.executer(null);
                    }
                    else
                    {
                        MaConsole.ecrireLigne("erreur de saisie");
                    }
                    break;
                case "prison":
                    p.JoueurEnCours.allerEnPrison();
                    break;
            }
        }
    }
}
