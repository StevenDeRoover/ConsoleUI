using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.eArgs
{
    public class ClosingEventArgs : EventArgs
    {
        public bool IsDefaultPrevented { get; set; }
    }
}
