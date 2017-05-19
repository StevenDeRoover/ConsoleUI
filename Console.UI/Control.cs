using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IControl : IUIElement
    {
        
    }

    public abstract class Control : UIElement, IControl
    {
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
    }
}
