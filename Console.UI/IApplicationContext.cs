using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public interface IApplicationContext
    {
        event EventHandler RenderComplete;
        Task Run();
    }
}
