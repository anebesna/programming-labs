using System;
using System.Windows;

namespace lab2
{
    public class Primary
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
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
            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            app.Run(wnd);
        }
    }
}