using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X4Tool
{
    static class Program
    {
        static Program()
        {
            LoadResourceDll.RegistDLL();
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            X4Tool tool = new X4Tool();
            tool.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(tool);
        }
    }
}
