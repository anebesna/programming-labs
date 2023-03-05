using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static string path;
        static DataTable dt;
        static int ID;
        public Staff()
        {
            InitializeComponent();
            con.Open();
            UpdateDT();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT name FROM ""Hospital"";";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            hospital.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void Choose(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelHeight = 200;
                bitmap.EndInit();
                ImageBox.Source = bitmap;
                path = bitmap.UriSource.ToString();
            }
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            Menu m = new Menu();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void UpdateDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT id_staff, surname, s.name, patronymic, category, ""Hospital"".name, birth_date, salary FROM staff as s join ""Hospital"" on id_hospital = ""Hospital"".id;";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void Add(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into staff (name, surname, patronymic, category, id_hospital, photo, birth_date, salary) values ('{name.Text}', '{surname.Text}', '{patronymic.Text}', '{major.Text}', '(select ""Hospital"".id from ""Hospital"" where ""Hospital"".name='{hospital.Text}')', '{path}', '{bd.SelectedDate.Value:dd.MM.yyyy}', {int.Parse(salary.Text)})";
            try
            {
                com.ExecuteNonQuery();
                UpdateDT();
            }
            catch
            {
                MessageBox.Show("Здається, деякі з полів незаповнені. Спробуйте ще!");
            }
            con.Close();
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
        private void Delete(object sender, EventArgs e)
        {
            if (id.Text != "")
            {
                ID = int.Parse(id.Text);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $"delete from staff where id_staff={ID}";
                try
                {
                    com.ExecuteNonQuery();
                    UpdateDT();
                }
                catch
                {
                    MessageBox.Show("Здається, даних за цим ID не існує. Спробуйте ще!");
                }
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            con.Close();
        }
        private void Clear(object sender, EventArgs e)
        {
            name.Text = null;
            surname.Text = null;
            patronymic.Text = null;
            major.Text = null;
            salary.Text = null;
            bd.SelectedDate = null;
            add.IsEnabled = true;
            hospital.SelectedIndex = -1;
            choose.IsEnabled = true;
            ImageBox.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\\Mac\Home\Downloads\user (2).png");
        }
        private void Search(object sender, EventArgs e)
        {
            if (id.Text != "")
            {
                ID = int.Parse(id.Text);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"SELECT s.name, surname, patronymic, category, ""Hospital"".name, photo, birth_date, salary FROM staff as s join ""Hospital"" on id_hospital = ""Hospital"".id where id_staff = {ID};";
                try
                {
                    NpgsqlDataReader dr = com.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);
                    com.Dispose();
                    name.Text = dt.Rows[0][0].ToString();
                    surname.Text = dt.Rows[0][1].ToString();
                    patronymic.Text = dt.Rows[0][2].ToString();
                    major.Text = dt.Rows[0][3].ToString();
                    hospital.Text = dt.Rows[0][4].ToString();
                    bd.SelectedDate = (DateTime)dt.Rows[0][6];
                    salary.Text = dt.Rows[0][7].ToString();
                    if (dt.Rows[0][5].ToString() == "") ImageBox.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\\Mac\Home\Downloads\user (2).png");
                    else ImageBox.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(dt.Rows[0][5].ToString());
                    add.IsEnabled = false;
                    choose.IsEnabled = false;
                    hospital.IsEnabled = false;
                }
                catch
                {
                    MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
                }
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            con.Close();
        }
    }
}
