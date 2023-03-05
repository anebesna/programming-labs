using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Query_9.xaml
    /// </summary>
    public partial class Query_9 : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        public Query_9()
        {
            InitializeComponent();
            con.Open();
            Back();
            con.Close();
        }
        private void Back()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select name from ""Hospital"";";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            hospital.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select distinct name from department;";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dep.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            Menu m = new Menu();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void All(object sender, EventArgs e)
        {
            hospital.Text = "";
            dep.Text = "";
            con.Open();
            Back();
            con.Close();
        }
        private void SelectedHospital(object sender, EventArgs e)
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
        private void Ninth(object sender, EventArgs e)
        {
            string str;
            if (dep.Text == "") str = "";
            else str = $@"department.name = '{dep.Text}' and";
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"select (select sum(room_count) from department 
            join ""Hospital"" on department.id_hospital = ""Hospital"".id 
            where {str} ""Hospital"".name = '{hospital.Text}') as rooms, 
            (select sum(bed_count) from department
            join ""Hospital"" on department.id_hospital = ""Hospital"".id
            where {str} ""Hospital"".name = '{hospital.Text}') as beds,
            ((select sum(room_count) from department
            join ""Hospital"" on department.id_hospital = ""Hospital"".id
            where {str} ""Hospital"".name = '{hospital.Text}') -
            (select count(distinct id_room) from patient
            join department on patient.id_dep = department.id
            join ""Hospital"" on department.id_hospital = ""Hospital"".id
            where {str} ""Hospital"".name = '{hospital.Text}') ) as emptyrooms, 
            ((select sum(bed_count) from department join ""Hospital"" on department.id_hospital = ""Hospital"".id
            where {str} ""Hospital"".name = '{hospital.Text}') -
            (select count(patient.id) from patient
            join healthcard on patient.id = healthcard.id_patient
            join department on patient.id_dep = department.id
            join ""Hospital"" on department.id_hospital = ""Hospital"".id
            where healthcard.leave_date is null and {str} ""Hospital"".name = '{hospital.Text}')) as emptybeds";
            try
            {
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                com.Dispose();
            }
            catch
            {
                MessageBox.Show("Здається, деякі поля незаповнені. Спробуйте ще!");
            }
            con.Close();
        }
    }
}
