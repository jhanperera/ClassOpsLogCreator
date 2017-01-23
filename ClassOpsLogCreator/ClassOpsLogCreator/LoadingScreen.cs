using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// A loading screen class that produced a splash screen while
    /// background work is happening. 
    /// </summary>
    public partial class LoadingScreen : MetroForm
    {
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static LoadingScreen splashForm;

        /// <summary>
        /// The main static method that creates the thread that the
        /// splashcreen will live in. 
        /// </summary>
        static public void ShowLoadingScreen()
        {
            // Make sure it is only launched once.

            if (splashForm != null)
                return;
            
            //Push this onto a new thread and start it.
            Thread thread = new Thread(new ThreadStart(LoadingScreen.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        /// <summary>
        /// The constructor of the splash screen. 
        /// Initializes everything. 
        /// </summary>
        static private void ShowForm()
        {
            //Load using the default constructor and init all components
            splashForm = new LoadingScreen();
            splashForm.InitializeComponent();
            Application.Run(splashForm);
        }

        /// <summary>
        /// The public static method that allows the user to close the splash screen 
        /// </summary>
        static public void CloseForm()
        {
            //Close the form
            splashForm.Invoke(new CloseDelegate(LoadingScreen.CloseFormInternal));
        }

        /// <summary>
        /// Close the splashscreen. 
        /// </summary>
        static private void CloseFormInternal()
        {
            splashForm.Close();
        }
    }

}
