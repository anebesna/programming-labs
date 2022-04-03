using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace practice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wnd = new Window
            {
                Height = 0,
                ShowInTaskbar = false,
                Width = 0,
                WindowStyle = WindowStyle.None
            };
            wnd.Show();
            wnd.Hide();
            wnd = new MainWindow();


            wnd.Show();
        }
    }
}
