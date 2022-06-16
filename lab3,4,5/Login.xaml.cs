using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;
using System.Linq;
using System.Windows.Media;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static int tries = 3;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void toMenu(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"SELECT password FROM ""user"" where login = '{login.Text}'";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            if (GetMD5(password.Password.ToString()) == dt.Rows[0][0].ToString())
            {
                com.CommandText = $@"insert into audit_log values((SELECT now()::timestamp), '{login.Text}')";
                com.ExecuteNonQuery();
                Menu menu = new Menu();
                Close();
                menu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                menu.Show();
            }
            else
            {
                tries--;
                MessageBox.Show($"Wrong password! Left tries: {tries}");
                password.Password = "";

            }
            if (tries == 0) Application.Current.Shutdown();
            com.Dispose();
            con.Close();
        }
        string GetMD5(string value)
        {
            string res;
            if (value == "") return "";
            else
            {
                System.Security.Cryptography.MD5CryptoServiceProvider my_pass = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
                data = my_pass.ComputeHash(data);
                res = System.Text.Encoding.ASCII.GetString(data);
                res = res.Replace('"', '/');
                res = res.Replace('\'', '8');
                return res;
            }
        }
    }
}
