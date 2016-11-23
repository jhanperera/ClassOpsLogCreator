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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Update settings from previous version
            if (Properties.Settings.Default.UpdateSetting)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateSetting = false;
                Properties.Settings.Default.Save();
            }
            //Run application
            Application.Run(new LogCreator());
        }
    }
}
