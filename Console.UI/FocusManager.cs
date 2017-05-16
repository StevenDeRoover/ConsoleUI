using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IFocusManager
    {
        void Message(Console.UI.IMessage message);
    }
}
