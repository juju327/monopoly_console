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



 public void Lancerde()
        {
            Random des = new Random();
            de1 = des.Next(1, 7);
            de2 = des.Next(1, 7);
            Console.WriteLine("Le résultat du dé 1 est de : " + de1);
            Console.WriteLine("Le résultat du dé 2 est de : " + de2);


        }
