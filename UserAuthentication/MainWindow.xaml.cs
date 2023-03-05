using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace practice3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



        }

        private void Admin(object sender, EventArgs e)
        {
            Hide();
            Admin_win aw = new Admin_win();
            aw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            aw.Show();
        }

        private void User(object sender, EventArgs e)
        {
            Hide();
            User_win uw = new User_win();
            uw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            uw.Show();
        }

        private void Developer(object sender, EventArgs e)
        {
            Hide();
            Dev_win dw = new Dev_win();
            dw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dw.Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
