
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace practice3
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

            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wnd.Show();
        }
        //private static void TestConnection()
        //{
        //    using(NpgsqlConnection con = GetConnection())
        //    {
        //        if(con.State = ConnectionState.Open)
        //        {
        //            Console.WriteLine("Connected!");
        //        }
        //    }
        //}
        //private static NpgsqlConnection GetConnection()
        //{
        //    return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=prac3;");        }
    }
}