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
    /// Interaction logic for Admin_win.xaml
    /// </summary>
    public partial class Admin_win : Window
    {
        static string Password = "";
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=prac3;");
        static int tries = 3;
        static DataTable dt;
        int length;
        int index = 0;

        public Admin_win()
        {
            InitializeComponent();
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT ""Password"" FROM main WHERE ""Login"" = 'admin'";
            NpgsqlDataReader dr = com.ExecuteReader();
            dr.Read();
            Password = dr.GetString(0);
            defaultState();
            con.Close();
            com.Dispose();
        }
        private void Login_click(object sender, EventArgs e)
        {
            if (Password == null) Password = "";
            if (GetMD5(login_pswd.Password.ToString()) == Password)
            {
                login_pswd.Password = "";
                Current_pswd.IsEnabled = true;
                New_pswd1.IsEnabled = true;
                New_pswd2.IsEnabled = true;
                update.IsEnabled = true;
                new_login.IsEnabled = true;
                add.IsEnabled = true;
                delete.IsEnabled = true;
                exit.IsEnabled = true;
                set_a.IsEnabled = true;
                set_r.IsEnabled = true;
                users.IsEnabled = true;
                activity.IsEnabled = true;
                restr.IsEnabled = true;
                next.IsEnabled = length == 1 ? false : true;
                login_pswd.IsEnabled = false;
                Login.IsEnabled = false;
                con.Open();
                RefreshDT();
                users.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                name.Content = dt.Rows[0][0].ToString();
                surname.Content = dt.Rows[0][1].ToString();
                login.Content = dt.Rows[0][2].ToString();
                status.Content = dt.Rows[0][3].ToString();
                restriction.Content = dt.Rows[0][4].ToString();
                users.SelectedIndex = -1;
                activity.IsChecked = (bool)dt.Rows[0][3];
                restr.IsChecked = (bool)dt.Rows[0][4];
                con.Close();
            }
            else
            {
                tries--;
                MessageBox.Show($"Wrong password! Left tries: {tries}");
                login_pswd.Password = "";

            }
            if (tries == 0) Application.Current.Shutdown();
        }

        private void Update(object sender, EventArgs e)
        {
            con.Open();

            string strQ;
            string Current = Current_pswd.Password.ToString();
            string New1 = New_pswd1.Password.ToString();
            string New2 = New_pswd2.Password.ToString();
            if (GetMD5(Current) == Password && New1 != "" && New1 == New2)
            {
                if (pb.Value == 100)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        strQ = @"UPDATE main SET ""Password"" ='" + GetMD5(New1) + @"'WHERE ""Login"" = 'admin';";
                        NpgsqlCommand com = new NpgsqlCommand(strQ, con);
                        com.ExecuteNonQuery();
                        Password = GetMD5(New1);
                        MessageBox.Show("The password has been succesfully changed!");
                    }
                }
                else MessageBox.Show("Password is not safe enough!");
            }
            else MessageBox.Show("Passwords do not match. Try again!");
            Current_pswd.Password = New_pswd1.Password = New_pswd2.Password = "";
            con.Close();
        }

        private void toMain(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mw.Show();
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

        private void defaultState()
        {
            Current_pswd.IsEnabled = false;
            New_pswd1.IsEnabled = false;
            New_pswd2.IsEnabled = false;
            update.IsEnabled = false;
            prev.IsEnabled = false;
            next.IsEnabled = false;
            new_login.IsEnabled = false;
            add.IsEnabled = false;
            delete.IsEnabled = false;
            exit.IsEnabled = false;
            set_a.IsEnabled = false;
            set_r.IsEnabled = false;
            users.IsEnabled = false;
            activity.IsEnabled = false;
            restr.IsEnabled = false;
            login_pswd.IsEnabled = true;
            Login.IsEnabled = true;
            name.Content = "";
            surname.Content = "";
            login.Content = "";
            status.Content = "";
            restriction.Content = "";
        }
        private void Sign_out(object sender, EventArgs e)
        {
            defaultState();
            db.ItemsSource = null;
            db.Items.Refresh();
        }
        private void Add_user(object sender, EventArgs e)
        {
            string s = new_login.Text;
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into main (""Login"") values ('{s}')";
            try
            {
                com.ExecuteNonQuery();
                RefreshDT();
                new_login.Text = "";
                users.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                next.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("User with such login already exists. Try again!");
                new_login.Text = "";
            }             
            con.Close();
        }
        private void Delete_user(object sender, EventArgs e)
        {
            string s = new_login.Text;
            if (s != "admin")
            {
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"delete from main where ""Login"" = '{s}'";
                try
                {
                    com.ExecuteNonQuery();
                    RefreshDT();
                    new_login.Text = "";
                    users.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                    next.IsEnabled = true;
                }
                catch
                {
                    MessageBox.Show("Such user doesn't exist. Try again!");
                    new_login.Text = "";
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("You can't delete admin. Try again!");
                new_login.Text = "";
            }
            if (length == 1) next.IsEnabled = false;
        }
        private void RefreshDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT ""Name"", ""Surname"", ""Login"", ""Status"", ""Restriction"" FROM main order by id asc";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            length = dt.Rows.Count;
        }
        private void Prev(object sender, EventArgs e)
        {
            next.IsEnabled = true;
            if (index == 1) prev.IsEnabled = false;
            index--;
            name.Content = dt.Rows[index][0].ToString();
            surname.Content = dt.Rows[index][1].ToString();
            login.Content = dt.Rows[index][2].ToString();
            status.Content = dt.Rows[index][3].ToString();
            restriction.Content = dt.Rows[index][4].ToString();
            activity.IsChecked = (bool)dt.Rows[index][3];
            restr.IsChecked = (bool)dt.Rows[index][4];
        }
        private void Next(object sender, EventArgs e)
        {
            prev.IsEnabled = true;
            if (index >= length - 2) next.IsEnabled = false;
            index++;
            name.Content = dt.Rows[index][0].ToString();
            surname.Content = dt.Rows[index][1].ToString();
            login.Content = dt.Rows[index][2].ToString();
            status.Content = dt.Rows[index][3].ToString();
            restriction.Content = dt.Rows[index][4].ToString();
            activity.IsChecked = (bool)dt.Rows[index][3];
            restr.IsChecked = (bool)dt.Rows[index][4];
        }
        private void users_Selected(object sender, SelectionChangedEventArgs e)
        {
            if(users.SelectedIndex != -1) {
                int i = users.SelectedIndex;
                if (i == 0)
                {
                    next.IsEnabled = true;
                    prev.IsEnabled = false;
                }
                else if (i == length - 1)
                {
                    prev.IsEnabled = true;
                    next.IsEnabled = false;
                }
                else
                {
                    prev.IsEnabled = true;
                    next.IsEnabled = true;
                }
                name.Content = dt.Rows[i][0].ToString();
                surname.Content = dt.Rows[i][1].ToString();
                login.Content = dt.Rows[i][2].ToString();
                status.Content = dt.Rows[i][3].ToString();
                restriction.Content = dt.Rows[i][4].ToString();
                activity.IsChecked = (bool)dt.Rows[i][3];
                restr.IsChecked = (bool)dt.Rows[i][4];
                index = i;
            }
        }
        private void SetActivity(object sender, EventArgs e)
        {
            con.Open();
            bool userstatus = (bool)activity.IsChecked;
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"update main set ""Status"" = {userstatus} where ""Login"" = '{login.Content}'";
            com.ExecuteNonQuery();
            RefreshDT();
            con.Close();
            status.Content = userstatus.ToString();
        }
        private void SetRestriction(object sender, EventArgs e)
        {
            con.Open();
            bool userrestr = (bool)restr.IsChecked;
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"update main set ""Restriction"" = {userrestr} where ""Login"" = '{login.Content}'";
            com.ExecuteNonQuery();
            RefreshDT();
            con.Close();
            restriction.Content = userrestr.ToString();
        }
        private void PasswordChanged(object sender, EventArgs e)
        {
            if ((bool)restr.IsChecked)
            {
                string s = New_pswd1.Password;
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
