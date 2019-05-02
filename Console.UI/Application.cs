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
        public static bool AttachMouse { get; set; } = true;

        private static MouseIO _mouseIO;

        [STAThread]
        public static void Run<T>(T applicationContext) where T : IApplicationContext, IFocusManager
        {
            if (AttachMouse)
            {
                _mouseIO = new MouseIO();
            }
            System.Console.CursorVisible = false;
            WndProc.Init();
            applicationContext.RenderComplete += (obj, e) =>
            {
                if (AttachMouse)
                {
                    WndProc.Attach(_mouseIO);
                }
                WndProc.Attach(applicationContext);
            };
            
            Task.Run(async () =>
            {
                try
                {
                    await applicationContext.Run();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }).GetAwaiter().GetResult();
        }
    }
}
