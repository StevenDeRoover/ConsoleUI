using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Console.UI;
using Console.UI.Controls;
using Console.UI.Controls.LayoutControls;

namespace ConsoleUI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new ApplicationContext { MainView = new View() });
        }
    }
}
