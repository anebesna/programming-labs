using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static int ID;

        public Patients()
        {
            InitializeComponent();
            con.Open();
            UpdateDT();
            con.Open();
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
        private void UpdateDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"SELECT p.id, surname, p.name, patronymic, d.name as department, ""Hospital"".name as hospital, birth_date, id_room as room, id_bed as bed FROM patient as p join department as d on p.id_dep = d.id join ""Hospital"" on d.id_hospital = ""Hospital"".id;";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void MedSelected(object sender, EventArgs e)
        {
            if (hospital.SelectedIndex != -1)
            {
                string s = hospital.Text; 
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"SELECT d.name FROM department as d join ""Hospital"" as h on d.id_hospital = h.id where h.name = '{s}'";
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                com.Dispose();
                con.Close();
                dep.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                dep.IsEnabled = true;
            }
        }
        private void Add(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @$"select count(*) from patient as p join department as d on p.id_dep = d.id join ""Hospital"" as h on d.id_hospital = h.id join healthcard on p.id = healthcard.id_patient where healthcard.leave_date is null and h.name='{hospital.Text}' and d.name = '{dep.Text}' and id_room = {int.Parse(room.Text)} and id_bed = {int.Parse(bed.Text)}";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            if (int.Parse(dt.Rows[0][0].ToString()) == 0)
            {
                com.CommandText = $@"insert into patient (name, surname, patronymic, id_dep, id_room, id_bed, birth_date) values ('{name.Text}', '{surname.Text}', '{patronymic.Text}', (select d.id from department as d join ""Hospital"" as h on d.id_hospital = h.id where h.name='{hospital.Text}' and d.name = '{dep.Text}'), {int.Parse(room.Text)}, {int.Parse(bed.Text)}, '{bd.SelectedDate.Value:dd.MM.yyyy}')";
                try
                {
                    com.ExecuteNonQuery();
                    UpdateDT();
                }
                catch
                {
                    MessageBox.Show("Здається, деякі з полів незаповнені. Спробуйте ще!");
                }
            }
            else MessageBox.Show("Здається, це місце вже зайняте. Спробуйте ще!");
            con.Close();
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
                com.CommandText = $"delete from patient where id={ID}";
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
            room.Text = null;
            bed.Text = null;
            dep.SelectedIndex = -1;
            bd.SelectedDate = null;
            add.IsEnabled = true;
            hospital.SelectedIndex = -1;
            hospital.IsEnabled = true;
            dep.IsEnabled = false;
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
                com.CommandText = $@"select surname, p.name, patronymic, d.name, ""Hospital"".name, birth_date, id_room, id_bed FROM patient as p join department as d on p.id_dep = d.id join ""Hospital"" on d.id_hospital = ""Hospital"".id where p.id = {ID};";
                try
                {
                    NpgsqlDataReader dr = com.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);
                    string temp = dt.Rows[0][3].ToString();
                    surname.Text = dt.Rows[0][0].ToString();
                    name.Text = dt.Rows[0][1].ToString();
                    patronymic.Text = dt.Rows[0][2].ToString();
                    hospital.Text = dt.Rows[0][4].ToString();
                    bd.SelectedDate = (DateTime)dt.Rows[0][5];
                    room.Text = dt.Rows[0][6].ToString();
                    bed.Text = dt.Rows[0][7].ToString();
                    com.CommandText = $@"SELECT d.name FROM department as d join ""Hospital"" as h on d.id_hospital = h.id where h.name = '{hospital.Text}'";
                    dr = com.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);
                    dep.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                    dep.Text = temp;
                    add.IsEnabled = false;
                    dep.IsEnabled = false;
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
        private void Medcard(object sender, EventArgs e)
        {
            Close();
            Healthcard h = new Healthcard();
            h.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            h.Show();
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            Menu m = new Menu();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
        private void Release(object sender, EventArgs e)
        {
            if (id.Text != "")
            {
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $"update healthcard set leave_date = (select current_date) where id_patient = {ID}";
                try
                {
                    com.ExecuteNonQuery();
                    UpdateDT();
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
