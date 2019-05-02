using Console.UI.Controls.LayoutControls;
using ConsoleUI.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Screens
{
    public partial class MainScreen
    {
        private PanelWithTimeAndProgressBar _childPanel;
        private ConsoleWriter _consoleWriter;
        internal void InitializeComponent()
        {
            _childPanel = new PanelWithTimeAndProgressBar {
            };
            _childPanel.ProgressBarText = "";
            _childPanel.ProgressBarValue = 0;
            _childPanel.TimeColor = ConsoleColor.Blue;
            _childPanel.BackgroundColor = ConsoleColor.DarkGray;
            _childPanel.ForegroundColor = ConsoleColor.White;
            _childPanel.BorderColor = ConsoleColor.White;
            _childPanel.Title = "DCA Data Migration";
            //_childPanel.Title = " Test";
            this.Child = _childPanel;
            _consoleWriter = new ConsoleWriter();
            _childPanel.Child = _consoleWriter;
        }
    }
}
