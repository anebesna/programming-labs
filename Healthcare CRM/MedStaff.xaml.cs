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
    /// Interaction logic for MedStaff.xaml
    /// </summary>
    public partial class MedStaff : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static string path;
        static string rank;
        static DataTable dt;
        static int ID;
        public MedStaff()
        {
            InitializeComponent();
            con.Open();
            UpdateDT();
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
        private void UpdateDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT doc_id, surname, name, patronymic, major, degree, birth_date, rank, (salary+(salary*coefficient)) as salary, coefficient FROM med_staff;";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            Menu m = new Menu();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void SelectedDegree(object sender, SelectionChangedEventArgs e)
        {
            if (degree.SelectedIndex != -1)
            {
                if (degree.SelectedItem.ToString().Contains("Кандидат медичних наук"))
                {
                    docent.IsEnabled = true;
                    professor.IsEnabled = false;
                }
                else
                {
                    professor.IsEnabled = true;
                    docent.IsEnabled = false;
                }
            }
        }
        private void Add(object sender, EventArgs e)
        {
            if(coef.Text != "0")
            {
                if(major.Text != "Стоматологія" && major.Text != "Рентгенологія")
                {
                    MessageBox.Show("Коефіцієнт до зарплати мають лише стоматологи та рентгенологи!");
                    coef.Text = "0";
                }
            }
            if ((bool)docent.IsChecked) rank = "docent";
            else if ((bool)professor.IsChecked) rank = "professor";
            else rank = null;
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into med_staff (name, surname, patronymic, major, degree, birth_date, rank, salary, coefficient, photo) values ('{name.Text}', '{surname.Text}', '{patronymic.Text}', '{major.Text}', '{degree.Text}', '{bd.SelectedDate.Value:dd.MM.yyyy}', '{rank}', {int.Parse(salary.Text)}, {double.Parse(coef.Text)}, '{path}')";
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
                com.CommandText = $"delete from med_staff where doc_id={ID}";
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
            degree.SelectedIndex = -1;
            docent.IsChecked = docent.IsEnabled = false;
            professor.IsChecked = professor.IsEnabled = false;
            salary.Text = null;
            coef.Text = null;
            bd.SelectedDate = null;
            add.IsEnabled = true;
            choose.IsEnabled = true;
            degree.IsEnabled = true;
            ImageBox.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\\Mac\Home\Downloads\user (2).png");
        }
        private void Operation(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select o.id_doc, m.surname, m.name, m.patronymic, o.type, o.result, o.id_patient, p.surname, p.name, p.patronymic from operation as o join med_staff as m on id_doc = doc_id join patient as p on id_patient = p.id;";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void Vacation(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select o.id_doc, m.surname, m.name, m.patronymic, o.date_start, o.date_end from vacation as o join med_staff as m on id_doc = doc_id;";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
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
                com.CommandText = $@"SELECT name, surname, patronymic, major, degree, birth_date, rank, (salary+(salary*coefficient)) as salary, coefficient, photo FROM med_staff where doc_id = {ID};";
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
                    degree.Text = dt.Rows[0][4].ToString();
                    bd.SelectedDate = (DateTime)dt.Rows[0][5];
                    if (dt.Rows[0][6].ToString().Contains("Доцент")) docent.IsChecked = true;
                    else if (dt.Rows[0][6].ToString().Contains("Професор")) professor.IsChecked = true;
                    salary.Text = dt.Rows[0][7].ToString();
                    coef.Text = dt.Rows[0][8].ToString();
                    if (dt.Rows[0][9].ToString() == "") ImageBox.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\\Mac\Home\Downloads\user (2).png");
                    else ImageBox.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(dt.Rows[0][9].ToString());
                    add.IsEnabled = false;
                    choose.IsEnabled = false;
                    degree.IsEnabled = false;
                    docent.IsEnabled = false;
                    professor.IsEnabled = false;
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
