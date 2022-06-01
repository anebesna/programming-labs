using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;
using System.Linq;
using System.Windows.Media;

namespace practice3
{
    /// <summary>
    /// Interaction logic for Dev_win.xaml
    /// </summary>
    public partial class Dev_win : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=prac3;");
        static DataTable dt;
        int length;
        public Dev_win()
        {
            InitializeComponent();
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT ""Name"", ""Surname"", ""Login"", ""Status"", ""Restriction"" FROM main order by id asc";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            length = dt.Rows.Count;
            label.Content = length;
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mw.Show();
        }
    }
}
