using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void toMedOrg(object sender, EventArgs e)
        {
            Close();
            MedOrg mo = new MedOrg();
            mo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mo.Show();
        }
        private void toMedStaff(object sender, EventArgs e)
        {
            Close();
            MedStaff ms = new MedStaff();
            ms.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ms.Show();
        }
        private void toStaff(object sender, EventArgs e)
        {
            Close();
            Staff s = new Staff();
            s.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            s.Show();
        }
        private void toPatients(object sender, EventArgs e)
        {
            Close();
            Patients p = new Patients();
            p.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            p.Show();
        }
        private void toQueries(object sender, EventArgs e)
        {
            Close();
            Queries q = new Queries();
            q.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            q.Show();
        }
        private void toConnections(object sender, EventArgs e)
        {
            Close();
            Connections c = new Connections();
            c.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            c.Show();
        }
        private void toSettings(object sender, EventArgs e)
        {
            AuditLog a = new AuditLog();
            Close();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.Show();
        }
    }
}
