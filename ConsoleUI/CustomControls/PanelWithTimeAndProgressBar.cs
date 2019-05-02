using Console.UI.Controls.LayoutControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console.UI;

namespace ConsoleUI.CustomControls
{
    public class PanelWithTimeAndProgressBar : Panel
    {
        private string _progressBarText;
        private int _progressBarValue;
        private ConsoleColor _progressColor = ConsoleColor.Green;
        private ConsoleColor _progressTextColor = ConsoleColor.Black;

        public string Format { get; set; } = "HH:mm:ss";
        public ConsoleColor TimeColor { get; set; }

        /// <summary>
        /// Value should range between 0% and 100%
        /// </summary>
        public int ProgressBarValue { get { return _progressBarValue; } set { _progressBarValue = value; RenderProgressBar(); } }

        public string ProgressBarText { get { return _progressBarText; } set { _progressBarText = value; RenderProgressBar(); } }

        public ConsoleColor ProgressColor { get { return _progressColor; } set { _progressColor = value; RenderProgressBar(); } }

        public ConsoleColor ProgressTextColor { get { return _progressTextColor; } set { _progressTextColor = value; RenderProgressBar(); } }

        public override void Render()
        {
            if (IsInit)
            {
                base.ReserveRightAreaTitle = Format.Length + 1;
                base.Render();
                RenderTime();
                RenderProgressBar();
            }
            base.Render();
        }

        private void RenderTime()
        {
            var g = CreateGraphics();
            g.FillRect((short)(g.Width - Format.Length - 1), 0, (short)Format.Length, 1, BorderColor);
            Timer t = new Timer(new TimerCallback((obj) =>
            {
                DrawTime();
            }), null, 0, 1000);
        }

        private void RenderProgressBar()
        {
            if (IsInit)
            {
                //validation
                if (ProgressBarValue < 0 || ProgressBarValue > 100) throw new Exception("The value of ProgressBarValue should be greater or equal to 0 and less or equal to 100");

                var g = CreateGraphics();

                var progressBarWidth = (g.Width - 2);

                g.FillRect(1, (short)(g.Height - 1), (short)(g.Width - 2), 1, ConsoleColor.Gray);

                {
                    //background
                    //rect to draw based on ProgressBarValue
                    var progressWidth = (int)((progressBarWidth * ProgressBarValue) / 100);
                    if (progressWidth > 0)
                    {
                        g.FillRect(1, (short)(g.Height - 1), (short)progressWidth, 1, ProgressColor);
                    }
                }

                {
                    //text
                    var progressBarText = ProgressBarText.Substring(0, Math.Min(ProgressBarText.Length, progressBarWidth));
                    var paddingLeft = (int)((progressBarWidth - progressBarText.Length) / 2);
                    g.DrawText((short)(paddingLeft + 1), (short)(g.Height - 1), progressBarText, _progressTextColor);
                }
            }
        }

        private void DrawTime()
        {
            var g = CreateGraphics();
            var dateTimeString = DateTime.Now.ToString(Format);
            g.DrawText((short)(g.Width - dateTimeString.Length - 1), 0, dateTimeString, TimeColor);
        }

        protected override void DrawTitle(ConsoleGraphics g)
        {
            base.DrawTitle(g);
            DrawTime();
        }
    }
}
