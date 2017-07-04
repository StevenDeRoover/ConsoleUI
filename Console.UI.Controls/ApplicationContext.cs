using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.UI.Controls.LayoutControls;
using System.Threading;
using Console = System.Console;
using Console.UI.Drawing;
using Console.UI.IO;

namespace Console.UI.Controls
{
    public class ApplicationContext : IApplicationContext, IFocusManager
    {
        public event EventHandler RenderComplete;

        public IView MainView { get; set; }

        public Task Run()
        {
            MainView.SetAvailableArea(Area.GetConsoleDrawingArea());
            MainView.Render();
            OnRenderComplete(this, EventArgs.Empty);
            return new Task(() => {
                while (!MainView.IsClosed)
                {
                }
            });
        }

        private void OnRenderComplete(ApplicationContext applicationContext, EventArgs e)
        {
            RenderComplete?.Invoke(this, e);
        }

        public void Message(IMessage message)
        {
            MainView.Message(message);
        }
    }
}
