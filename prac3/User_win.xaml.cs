using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;
using System.Windows.Media;
using System.Linq;

namespace practice3
{
    /// <summary>
    /// Interaction logic for User_win.xaml
    /// </summary>
    public partial class User_win : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=prac3;");
        static int tries = 3;
        static string Password;
        static string current_login;
        bool restriction;
        static DataTable dt;
        public User_win()
        {
            InitializeComponent();
            defaultState();
        }

        private void toMain(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mw.Show();
        }

        private void Login(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT * FROM main WHERE ""Login"" = '" + login.Text + "';";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            Password = dt.Rows[0][3].ToString();
            current_login = dt.Rows[0][2].ToString();
            restriction = (bool)dt.Rows[0][5];
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Such user doesn't exist!");
            }
            else
            {
                bool status = Convert.ToBoolean(dt.Rows[0][4]);
                if (!status) MessageBox.Show("User was blocked by Admin");
                else
                {
                    if ((current_login == login.Text) && (GetMD5(password.Password) == Password))
                    {
                        name.Text = dt.Rows[0][0].ToString();
                        surname.Text = dt.Rows[0][1].ToString();
                        inSystemState();
                    }
                    else
                    {
                        tries--;
                        MessageBox.Show($"Wrong password! Left tries: {tries}");
                    }
                }
                login.Text = "";
                password.Password = "";
                if (tries == 0) Application.Current.Shutdown();
                con.Close();
            }
        }
        private void SignOut(object sender, EventArgs e)
        {
            defaultState();
            name.Text = "";
            surname.Text = "";
        }
        private void defaultState()
        {
            password.IsEnabled = true;
            new_pass.IsEnabled = false;
            new_pass2.IsEnabled = false;
            update.IsEnabled = false;
            login.IsEnabled = true;
            name.IsEnabled = false;
            surname.IsEnabled = false;
            log_in.IsEnabled = true;
            exit.IsEnabled = false;
            newname.IsEnabled = true;
            newlogin.IsEnabled = true;
            newpass.IsEnabled = true;
            newpass2.IsEnabled = true;
            newsurname.IsEnabled = true;
            sign_in.IsEnabled = false;
        }
        private void inSystemState()
        {
            name.IsEnabled = true;
            surname.IsEnabled = true;
            new_pass.IsEnabled = true;
            new_pass2.IsEnabled = true;
            update.IsEnabled = true;
            newname.IsEnabled = false;
            newlogin.IsEnabled = false;
            newpass.IsEnabled = false;
            newpass2.IsEnabled = false;
            newsurname.IsEnabled = false;
            sign_in.IsEnabled = false;
            log_in.IsEnabled = false;
            login.IsEnabled = false;
            password.IsEnabled = false;
            exit.IsEnabled = true;
        }
        private void Update(object sender, EventArgs e)
        {
            con.Open();
            string strQ;
            string _name = name.Text;
            string _surname = surname.Text;
            string New1 = new_pass.Password;
            string New2 = new_pass2.Password;
            if (New1 == New2 && New1 != "")
            {
                if (pb.Value == 100)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        strQ = $@"UPDATE main SET ""Password"" = '{GetMD5(New1)}', ""Name"" = '{_name}', ""Surname"" = '{_surname}' WHERE ""Login"" = '{current_login}';";
                        NpgsqlCommand com = new NpgsqlCommand(strQ, con);
                        com.ExecuteNonQuery();
                        Password = GetMD5(New1);
                        MessageBox.Show("Data has been succesfully changed!");
                    }
                }
                else  MessageBox.Show("Password is not safe enough!");
            }
            else if (New1 == "")
            {
                strQ = $@"UPDATE main SET ""Name"" = '{_name}', ""Surname"" = '{_surname}' WHERE ""Login"" = '{current_login}';";
                NpgsqlCommand com = new NpgsqlCommand(strQ, con);
                com.ExecuteNonQuery();
                MessageBox.Show("Data has been succesfully changed!");
            }
            else
            {
                MessageBox.Show("Passwords don't match. Try again!");
            }
            con.Close();
            new_pass.Password = new_pass2.Password = "";
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
        private void SignIn(object sender, EventArgs e)
        {
            if (newpass.Password == newpass2.Password)
            {
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"insert into main (""Name"", ""Surname"", ""Login"", ""Password"") values ('{newname.Text}', '{newsurname.Text}', '{newlogin.Text}', '{GetMD5(newpass.Password)}')";
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("You've been successfully signed in!");
                    name.Text = newname.Text;
                    surname.Text = newsurname.Text;
                    inSystemState();

                }
                catch
                {
                    MessageBox.Show("Either user with such login already exists or login field is empty. Try again!");
                    newlogin.Text = "";
                    newpass.Password = "";
                    newpass2.Password = "";
                }
                con.Close();
            }
            else MessageBox.Show("Passwords do not match. Try again!");
            newname.Text = "";
            newsurname.Text = "";
            sign_in.IsEnabled = false;
            newlogin.Text = "";
            newpass.Password = "";
            newpass2.Password = "";
        }
        private void TextChanged(object sender, EventArgs e)
        {
            string s = newpass.Password;
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
                    pbStatus.Value = 100;
                    pbStatus.Foreground = Brushes.Green;
                    sign_in.IsEnabled = true;
                    break;
                case 3:
                    pbStatus.Value = 75;
                    pbStatus.Foreground = Brushes.GreenYellow;
                    sign_in.IsEnabled = false;
                    break;
                case 2:
                    pbStatus.Value = 50;
                    pbStatus.Foreground = Brushes.Yellow;
                    break;
                case 1:
                    pbStatus.Value = 25;
                    pbStatus.Foreground = Brushes.IndianRed;
                    break;
                case 0:
                    pbStatus.Value = 0;
                    break;
            }
            
        }
        private void PasswordChanged(object sender, EventArgs e)
        {
            if (restriction)
            {
                string s = new_pass.Password;
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
        }
    }
}
