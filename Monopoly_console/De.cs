using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
   
    
    class De
    {

        public int de1
        {
            get;
            private set;
        }


        public int de2
        {
            get;
            private set;
        }

        public int NbDoubles
        {
            get;
            private set;
        }

 public void Lancerde()
        {
            Random des = new Random();
            de1 = des.Next(1, 7);
            de2 = des.Next(1, 7);
            Console.WriteLine("Le résultat du dé 1 est de : " + de1);
            Console.WriteLine("Le résultat du dé 2 est de : " + de2);


            // cas où l'on a des doubles 
            if (de1 == de2)
            {
                NbDoubles++;
            }
            else
            {
                NbDoubles = 0;

            }

            if (NbDoubles == 3)
            { // aller en prison 

            }
            Console.WriteLine("le nombre de doubles est de " + NbDoubles);
        }
    }
}
