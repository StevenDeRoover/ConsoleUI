using Console.UI.Controls.LayoutControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI.Screens
{
    public partial class MainScreen : View
    {
        public MainScreen()
        {
            InitializeComponent();
            Timer t = new Timer(new TimerCallback((obj) =>
            {
                //_childPanel.Title = "Test";
            }), null, 10000, Timeout.Infinite);
        }
    }
}
