using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Console.UI
{
    public abstract class Application
    {
        public static void Run<T>(T applicationContext) where T : IApplicationContext, IFocusManager
        {
            System.Console.CursorVisible = false;
            applicationContext.Run();
            while (true)
            {
                applicationContext.Message(new KeyMessage {KeyInfo = System.Console.ReadKey()});
            }
        }
    }
}
