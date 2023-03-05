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
    /// Interaction logic for AuditLog.xaml
    /// </summary>
    public partial class AuditLog : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static string password;

        public AuditLog()
        {
            InitializeComponent();
            con.Open();
            RefreshDT();
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            Menu m = new Menu();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void Update(object sender, EventArgs e)
        {
            con.Open();

            string strQ;
            string New1 = pass1.Password.ToString();
            string New2 = pass2.Password.ToString();
            if (New1 != "" && New1 == New2)
            {
                if (pb.Value == 100)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        strQ = $@"UPDATE ""user"" SET password ='" + GetMD5(New1) + $@"'WHERE login = '{login.Text}';";
                        NpgsqlCommand com = new NpgsqlCommand(strQ, con);
                        com.ExecuteNonQuery();
                        password = GetMD5(New1);
                        MessageBox.Show("The password has been succesfully changed!");
                    }
                }
                else MessageBox.Show("Password is not safe enough!");
            }
            else MessageBox.Show("Passwords do not match. Try again!");
            pass1.Password = pass2.Password = "";
            con.Close();
        }
        private void Add(object sender, EventArgs e)
        {
            con.Open();
            string New1 = pass1.Password.ToString();
            string New2 = pass2.Password.ToString();
            if (New1 != "" && New1 == New2)
            {
                if (pb.Value == 100)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        NpgsqlCommand com = new NpgsqlCommand();
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        com.CommandText = @$"insert into ""user"" values('{login.Text}', '{GetMD5(New1)}')";
                        com.ExecuteNonQuery();
                        MessageBox.Show("The user has been succesfully added!");
                    }
                }
                else MessageBox.Show("Password is not safe enough!");
            }
            else MessageBox.Show("Passwords do not match. Try again!");
            pass1.Password = pass2.Password = "";
            con.Close();
        }
        private void RefreshDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT datetime, login FROM audit_log";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
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
        private void PasswordChanged(object sender, EventArgs e)
        {
            string s = pass1.Password;
            int count = 0;
            bool upper = s.Any(char.IsUpper);
            bool lower = s.Any(char.IsLower);
            bool nums = s.Any(char.IsDigit);
            bool symb = s.Any(p => !char.IsLetterOrDigit(p));
            bool[] pass = { upper, lower, nums, symb };
            for (int i = 0; i < pass.Length; i++)
            {
                if (pass[i]) count++;
            }
            switch (count)
            {
                case 4:
                    pb.Value = 100;
                    pb.Foreground = Brushes.Green;

                    break;
                case 3:
                    pb.Value = 75;
                    pb.Foreground = Brushes.GreenYellow;
                    break;
                case 2:
                    pb.Value = 50;
                    pb.Foreground = Brushes.Yellow;
                    break;
                case 1:
                    pb.Value = 25;
                    pb.Foreground = Brushes.IndianRed;
                    break;
                case 0:
                    pb.Value = 0;
                    break;
            }
            
        }
        private void PopupShow(object sender, EventArgs e)
        {
            pop.IsOpen = true;
        }
        private void Ok(object sender, EventArgs e)
        {
            pop.IsOpen = false;
        }
    }
}
