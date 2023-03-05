using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for MedOrg.xaml
    /// </summary>
    public partial class MedOrg : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static int ID;
        public MedOrg()
        {
            InitializeComponent();
            con.Open();
            UpdateDT();
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
            com.CommandText = @"SELECT h.id, h.name, h.address, h.type, h.building_count, c.name FROM ""Hospital"" as h left join ""Hospital"" as c on c.id=h.id_polyclinic;";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select name from ""Hospital"" where type = 'Поліклініка';";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            clinic.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select name from ""Hospital"";";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            med_org.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.Dispose();
            con.Close();
        }
        private void Departments(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select d.id, d.name, d.id_building, d.room_count, d.bed_count, h.name from department as d join ""Hospital"" as h on id_hospital = h.id";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void Meds(object sender, EventArgs e)
        {
            con.Open();
            UpdateDT();
        }
        private void AddHospital(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into ""Hospital"" (name, type, address, building_count, id_polyclinic) values ('{name.Text}', '{type.Text}', '{address.Text}', {int.Parse(count.Text)}, (select id from ""Hospital"" where name = '{clinic.Text}'))";
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
        private void SelectionType(object sender, SelectionChangedEventArgs e)
        {
            if (type.SelectedIndex != -1)
            {
                if (type.SelectedItem.ToString().Contains("Лікарня"))
                {
                    clinic.IsEnabled = true;
                }
                else
                {
                    clinic.IsEnabled = false;
                }
            }
        }
        private void ClearHospital(object sender, EventArgs e)
        {
            name.Text = null;
            type.SelectedIndex = -1;
            address.Text = null;
            count.Text = null;
            clinic.SelectedIndex = -1;
            add1.IsEnabled = true;
            type.IsEnabled = true;
        }
        private void AddDep(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into department (name, id_hospital, id_building, room_count, bed_count) values ('{dep.Text}', (select id from ""Hospital"" where name = '{med_org.Text}'), {int.Parse(building.Text)}, {int.Parse(rooms.Text)}, {int.Parse(beds.Text)})";
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
        private void ClearDep(object sender, EventArgs e)
        {
            dep.Text = null;
            med_org.SelectedIndex = -1;
            building.Text = null;
            rooms.Text = null;
            beds.Text = null;
            add2.IsEnabled = true;
            med_org.IsEnabled = true;
        }
        private void SearchHospital()
        {
            ID = int.Parse(id_org.Text);
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"SELECT h.name, h.address, h.type, h.building_count, c.name FROM ""Hospital"" as h left join ""Hospital"" as c on c.id=h.id_polyclinic where h.id = {ID};";
            try
            {
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                com.Dispose();
                con.Close();
                name.Text = dt.Rows[0][0].ToString();
                address.Text = dt.Rows[0][1].ToString();
                type.Text = dt.Rows[0][2].ToString();
                count.Text = dt.Rows[0][3].ToString();
                clinic.Text = dt.Rows[0][4].ToString();
                add1.IsEnabled = false;
                clinic.IsEnabled = false;
                type.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            }
        }
        private void SearchDep()
        {
            ID = int.Parse(id_dep.Text);
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"select d.name, d.id_building, d.room_count, d.bed_count, h.name as hospital from department as d join ""Hospital"" as h on id_hospital = h.id where d.id = {ID};";
            try
            {
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                com.Dispose();
                con.Close();
                dep.Text = dt.Rows[0][0].ToString();
                building.Text = dt.Rows[0][1].ToString();
                rooms.Text = dt.Rows[0][2].ToString();
                beds.Text = dt.Rows[0][3].ToString();
                med_org.Text = dt.Rows[0][4].ToString();
                add2.IsEnabled = false;
                med_org.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            }
        }
        private void Search(object sender, EventArgs e)
        {
            if (id_dep.Text == "" && id_org.Text != "") SearchHospital();
            else if (id_dep.Text != "" && id_org.Text == "") SearchDep();
            else if (id_dep.Text != "" && id_org.Text != "")
            {
                SearchHospital();
                SearchDep();
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
        }
        private void Delete(object sender, EventArgs e)
        {
            if (id_dep.Text == "" && id_org.Text != "") DeleteHospital();
            else if (id_dep.Text != "" && id_org.Text == "") DeleteDep();
            else if (id_dep.Text != "" && id_org.Text != "")
            {
                DeleteHospital();
                DeleteDep();
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
        }
        private void DeleteHospital() 
        {

            ID = int.Parse(id_org.Text);
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @$"delete from ""Hospital"" where id = {ID}";
            try
            {
                com.ExecuteNonQuery();
                UpdateDT();
            }
            catch
            {
                MessageBox.Show("Здається, даних за цим ID не існує. Спробуйте ще!");
            }
            con.Close();
        }
        private void DeleteDep() 
        {

            ID = int.Parse(id_dep.Text);
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $"delete from department where id={ID}";
            try
            {
                com.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Здається, даних за цим ID не існує. Спробуйте ще!");
            }
            con.Close(); 
        }
    }
}
