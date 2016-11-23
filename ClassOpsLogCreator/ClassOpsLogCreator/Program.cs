using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClassOpsLogCreator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Run our settings loader here from previous versions
            if (Properties.Settings.Default.UpdateSetting)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateSetting = false;
                Properties.Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InitialEmailLoginForm());
            //Application.Run(new LogCreator());
        }
    }
}
