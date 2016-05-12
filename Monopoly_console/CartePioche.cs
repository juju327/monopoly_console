using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    class CartePioche : Serialisable
    {
        public Action Action
        {
            get;
            private set;
        }

        public String Description
        {
            get;
            private set;
        }

        public static Plateau Plateau
        {
            get;
            private set;
        }

        public CartePioche(Action act, String desc, Plateau p)
        {
            Action = act;
            Description = desc;
            Plateau = p;
        }

        public override void serialiser(XElement racine)
        {
            //base.serialiser(racine);
        }

        public static new Object deserialiser(XElement racine)
        {
            return null;
        }
    }
}
