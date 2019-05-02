using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.LayoutControls
{
    using Extensions;
    using System.ComponentModel;

    public class Panel : Control
    {
        private string _title;
        private UIElement _child;

        public bool DrawBorder { get; set; } = true;
        public ConsoleColor BorderColor { get; set; } = ConsoleColor.White;
        public UIElement Child { get { return _child; } set { _child = value; Render(); } }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Title)));
                }
            }
        }

        public enum EnumTitlePosition
        {
            Left,
            Right,
            Center
        }

        public EnumTitlePosition TitlePosition { get; set; }

        public override void Init()
        {
            base.Init();
            if (Child != null) Child.Init();
        }

        public override void Render()
        {
            if (IsInit)
            {
                var g = CreateGraphics();
                g.FillRect(0, 0, g.Width, g.Height, this.BackgroundColor);
                if (DrawBorder)
                {
                    g.DrawRect(0, 0, g.Width, g.Height, this.BorderColor);
                    DrawTitle(g);
                }
                
                PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Title" && sender == this)
                    {
                    //refresh top line first
                    g.DrawHorizontalLine(1, 0, (short)(g.Width - 2), BorderColor);
                        DrawTitle(CreateGraphics());
                    }
                };
                //g.DrawText(this.Title)
                if (Child != null)
                {
                    Child.AvailableDrawingArea = GetClientArea();

                    Child.Render();
                }
            }
            base.Render();
        }

        protected int ReserveRightAreaTitle { get; set; } = 0;

        protected virtual void DrawTitle(ConsoleGraphics g)
        {
            var maxCharacters = g.Width - 2 - ReserveRightAreaTitle;
            var text = (this.Title = this.Title == null ? string.Empty : this.Title).TruncWords(maxCharacters);
            var trailing = "";
            switch (this.TitlePosition)
            {
                case EnumTitlePosition.Right:
                    for (var i = 0; i < maxCharacters - text.Length; i++) trailing += ' ';
                    break;
                case EnumTitlePosition.Center:
                    var leftAdd = (maxCharacters - text.Length) / 2;
                    for (var i = 0; i < leftAdd; i++) trailing += ' ';
                    break;
            }
            text = $"{trailing}{text}";
            g.DrawText(1, 0, text, this.ForegroundColor);

        }
    }
}
