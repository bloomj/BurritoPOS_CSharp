using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using log4net;
using log4net.Config;
using BurritoPOS.presentation.WinForm;

namespace BurritoPOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // WinForm entry
            Application.Run(new LoginUI());
            
            // WPF entry
            /*LoginUI myWindow = new LoginUI();
            myWindow.Show(); //Will show the window on the screen
            Application.Run(); //Will run the application*/
        }
    }
}
