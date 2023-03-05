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

namespace lab
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
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void StudentInfo_Click(object sender, EventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
            Hide();
        }
        private void TicTacToe_Click(object sender, EventArgs e)
        {
            Window2 win2 = new Window2();
            win2.Show();
            Hide();
        }
        private void Calc_Click(object sender, EventArgs e)
        {
            Window3 win3 = new Window3();
            win3.Show();
            Hide();
        }
        private void DevInfo_Click(object sender, EventArgs e)
        {
            Window4 win4 = new Window4();
            win4.Show();
            Hide();
        }
    }
}
