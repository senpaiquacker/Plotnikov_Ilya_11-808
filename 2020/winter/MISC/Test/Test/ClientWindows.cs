using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace Client
{
    public static class ClientWindows
    {
        public static MainWindow MainWindow = new MainWindow();

        private static Form currentWindow;
        public static Form CurrentWindow
        {
            get
            {
                return currentWindow;
            }
            set
            {
                if (currentWindow == null)
                    currentWindow = MainWindow;
                if (currentWindow == MainWindow)
                    currentWindow.Hide();
                else
                    currentWindow.Close();
                value.Show();
                if (value == null)
                    currentWindow = MainWindow;
                else
                    currentWindow = value;
            }
        }
        public static void MessageMaker(string message)
        {
            MessageBox.Show(message);
        }
    }
}
