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
        private static MouseIO _mouseIO = new MouseIO();
        [STAThread]
        public static void Run<T>(T applicationContext) where T : IApplicationContext, IFocusManager
        {
            System.Console.CursorVisible = false;
            WndProc.Init();
            applicationContext.RenderComplete += (obj, e) =>
            {
                WndProc.Attach(_mouseIO);
                WndProc.Attach(applicationContext);
            };
            
            Task.Run(async () =>
            {    
                await applicationContext.Run();
            }).GetAwaiter().GetResult();
        }
    }
}
