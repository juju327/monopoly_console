using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace monopoly
{
    public class Serialisable
    {
        /// <summary>
        /// Insère les informations de l'objet qui se sérialise dans la racine donnée
        /// </summary>
        /// <param name="racine">le noeud xml dans lequel insérer l'objet</param>
        public virtual void serialiser(XElement racine)
        {
        }

        /// <summary>
        /// récupère les informations disponibles dans la racine et
        /// rend une instance de classe
        /// </summary>
        /// <param name="racine">le noeud xml dans lequel l'objet est</param>
        /// <returns></returns>
        public static Object deserialiser(XElement racine)
        {
            return null;
        }

        //List<Object> getAll(XElement racine);
    }
}
