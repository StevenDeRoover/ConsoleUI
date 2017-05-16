using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.UI;

namespace ConsoleUI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Console.WindowHeight = Console.LargestWindowHeight;
            //Console.WindowWidth = Console.LargestWindowWidth;
            int index = 0;
            while (true)
            {
                var input = System.Console.ReadKey(true);

                if (index++ < 10)
                {
                    System.Console.Write(input.KeyChar);
                }

                if (input.Key == ConsoleKey.Enter)
                    break;
            }

            System.Console.Write("TEST");
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
            var test = ConsoleReaderX.ReadFromBuffer((short)0, (short)0, (short)5, (short)1).ToList();
            ConsoleReaderX.DrawRect(5,5,40,10);
            System.Console.ReadKey();
        }
    }
}
