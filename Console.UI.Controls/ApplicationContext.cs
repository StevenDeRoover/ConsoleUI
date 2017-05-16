using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.UI.Controls.LayoutControls;

namespace Console.UI.Controls
{
    public class ApplicationContext : IApplicationContext
    {
        public IView MainView { get; set; }

        public void Run()
        {
            MainView.Render();
        }
    }
}
