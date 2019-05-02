using Console.UI.Controls.LayoutControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console.UI;
using Console.UI.IO;
using Console.UI.Controls.InputControls;

namespace ConsoleUI.Screens
{
    public partial class MainScreen : View
    {
        public MainScreen()
        {
            InitializeComponent();
            OnRenderComplete += MainScreen_OnRenderComplete;
        }

        private void MainScreen_OnRenderComplete(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                for (var i = 1; i <= 100; i++)
                {
                    _consoleWriter.ForegroundColor = (i % 4 == 0) ? ConsoleColor.Yellow : ConsoleColor.White;
                    var text = "Uploading file " + i;
                    _childPanel.ProgressBarText = text + $" ({i}%)";
                    if (i % 4 == 0) { text = "\t" + text; }
                    _consoleWriter.WriteLine(text); _childPanel.ProgressBarValue = i;
                    Thread.Sleep(200);
                }
            });
        }

        public override void Message(IMessage message)
        {
            //base.Message(message);
            if (message is KeyMessage)
            {
                KeyMessage msg = message as KeyMessage;
                if ((msg.Modifier & Modifiers.LeftCtrl) > 0 && msg.KeyCode == ConsoleKey.F5)
                {
                    this.Close();
                }
            }
        }
    }
}
