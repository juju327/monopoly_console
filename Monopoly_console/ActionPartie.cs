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
                    p.anniversaire();
                    break;
                case "payer_ou_tirer":
                    p.payerOuTirer();
                    break;
                case "prison":
                    break;
            }
        }
    }
}
