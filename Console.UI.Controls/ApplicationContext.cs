using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.UI.Controls.LayoutControls;
using System.Threading;
using Console = System.Console;

namespace Console.UI.Controls
{
    public class ApplicationContext : IApplicationContext, IFocusManager
    {
        public IView MainView { get; set; }

        public void Run()
        {
            MainView.Render();
        }

        public void Message(IMessage message)
        {
            MainView.Message(message);
        }
    }
}
