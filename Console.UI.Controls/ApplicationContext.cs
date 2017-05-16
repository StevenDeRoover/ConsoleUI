using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.UI.Controls.LayoutControls;
using System.Threading;
using Console = System.Console;
using Console.UI.Drawing;

namespace Console.UI.Controls
{
    public class ApplicationContext : IApplicationContext, IFocusManager
    {
        public IView MainView { get; set; }

        public void Run()
        {
            MainView.SetAvailableArea(Area.GetConsoleDrawingArea());
            MainView.Render();
        }

        public void Message(IMessage message)
        {
            MainView.Message(message);
        }
    }
}
