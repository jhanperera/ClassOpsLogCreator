using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class in charge of displaying status information about the system when it is running.
    /// </summary>
    public partial class DetailForm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Create the detail window with the initString
        /// </summary>
        /// <param name="initString"></param>
        public DetailForm(string initString)
        {
            InitializeComponent();

            this.detailTextBox.Text = initString;

            //Set the location to the left of the main Window
            int y = (Screen.PrimaryScreen.Bounds.Bottom/2) -  (this.Height/2);
            this.Location = new Point(0, y);
        }

        /// <summary>
        /// Send a string that will be written to the Detail window
        /// </summary>
        /// <param name="updateString"></param>
        public void updateDetail(string updateString)
        {
            //Write the time and the text to the detail window
            string time = DateTime.Now.ToString("H:mm:ss");

            this.detailTextBox.AppendText(Environment.NewLine +
                                    "(" + time + ") ", Color.Red);
            this.detailTextBox.AppendText(updateString);

            //Update the text box to go to the bottom 
            detailTextBox.SelectionStart = detailTextBox.Text.Length;
            detailTextBox.ScrollToCaret();

            //Force the window to update when text is written to it. 
            this.Invalidate();
            this.Update();
            this.Refresh();
            Application.DoEvents();
        }
    }

    /// <summary>
    /// A public inner class that helps us append text with different text colors.
    /// </summary>
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// This method appends text to the richtextbox with the set color
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
