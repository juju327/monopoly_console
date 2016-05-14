using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace monopoly
{
    class Program
    {
        static void Main(string[] args)
        {
            // couleurs
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            // position
            Console.SetWindowPosition(0, 0);

            // taille de la fenêtre
            Console.WindowWidth = Console.LargestWindowWidth - 50;
            Console.WindowHeight = Console.LargestWindowHeight - 4;

            //Console.WindowLeft = 0;

            Console.Clear();

            Partie partie = new Partie();
        }
    }
}
