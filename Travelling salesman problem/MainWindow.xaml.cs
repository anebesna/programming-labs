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
using System.Windows.Shapes;

namespace prac2
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
        private void Genetic_Click(object sender, EventArgs e)
        {
            GAwin ga = new GAwin();
            Close();
            ga.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ga.Show();
        }
        private void Greedy_Click(object sender, EventArgs e)
        {
            GreedyWin greedy = new GreedyWin();
            Close();
            greedy.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            greedy.Show();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
