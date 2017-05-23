using Console.UI.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Console.UI
{
    public static class Application
    {
        [STAThread]
        public static void Run<T>(T applicationContext) where T : IApplicationContext, IFocusManager
        {
            WndProc.Init();
            System.Console.CursorVisible = false;
            Task.Run(async () =>
            {    
                await applicationContext.Run();
            }).GetAwaiter().GetResult();
        }
    }
}
