using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IMessage
    {
    }

    public interface IKeyMessage : IMessage
    {
    }

    public class KeyMessage : IKeyMessage
    {
        public ConsoleKeyInfo KeyInfo { get; set; }
    }
}
