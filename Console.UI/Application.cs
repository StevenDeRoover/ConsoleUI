using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI
{
    public abstract class Application
    {
        public static void Run(IApplicationContext applicationContext)
        {
            applicationContext.Run();
        }
    }
}
