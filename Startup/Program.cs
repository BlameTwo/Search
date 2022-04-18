using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            string startpaht = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            if (args[0] == "添加启动项")
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShellClass();

                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(StartPath + "\\快搜.lnk");
                shortcut.TargetPath = AppDomain.CurrentDomain.BaseDirectory + "Search.exe";

                shortcut.Description = "快捷搜索的快捷方式";
                shortcut.IconLocation = AppDomain.CurrentDomain.BaseDirectory + "Search.exe";
                shortcut.Save();
            }
        }
    }
}
