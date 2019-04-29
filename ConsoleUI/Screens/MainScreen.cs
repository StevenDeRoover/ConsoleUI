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
                if (msg.KeyCode == ConsoleKey.F1)
                {
                    var textbox = new TextBox();
                    textbox.Text = "Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf Test qdsf dqssdfqfq qsdfqdfs qsdf qsdf qsdf qsdfqsdfqsf qsdf qdfsqsdf qsdf qsdf qsdf ";
                    textbox.BackgroundColor = ConsoleColor.Red;
                    _childPanel.Child = textbox;
                }
            }
        }
    }
}
