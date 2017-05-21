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
        private PanelWithTime _childPanel;
        internal void InitializeComponent()
        {
            _childPanel = new PanelWithTime {
            };
            _childPanel.TimeColor = ConsoleColor.Blue;
            _childPanel.BackgroundColor = ConsoleColor.DarkGray;
            _childPanel.ForegroundColor = ConsoleColor.White;
            _childPanel.BorderColor = ConsoleColor.White;
            _childPanel.Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum porttitor iaculis neque, eget malesuada nunc hendrerit sit amet";
            //_childPanel.Title = " Test";
            this.Child = _childPanel;
        }
    }
}
