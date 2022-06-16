using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Queries2.xaml
    /// </summary>
    public partial class Queries2 : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        public Queries2()
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
            com.CommandText = @"select concat(p.doc_id, ' ', p.surname, ' ', p.name, ' ', p.patronymic) as fullname from med_staff as p;";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            doc.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select distinct major from med_staff";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            major.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
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
            major.Text = "";
            doc.Text = "";
            room.Text = "";
            edate.SelectedDate = null;
            ldate.SelectedDate = null;
            con.Open();
            Back();
            con.Close();
        }
        private void Other(object sender, EventArgs e)
        {
            Close();
            Query_9 q = new Query_9();
            q.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            q.Show();
        }
        private void Sixth(object sender, EventArgs e)
        {
            int i;
            if (room.Text == "") i = 0;
            else i = int.Parse(room.Text);
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"select patient.surname, patient.name, patient.patronymic, healthcard.entry_date, current_state.state, current_state.temperature, med_staff.surname, med_staff.name, med_staff.patronymic from patient  
            join department on patient.id_dep = department.id 
            join ""Hospital"" as h on department.id_hospital = h.id 
            join healthcard on patient.id = id_patient
            join current_state on healthcard.id = id_card
            join med_staff on current_state.id_doc = med_staff.doc_id
            where h.type = 'Лікарня' and ((department.name = '{dep.Text}' or h.name = '{hospital.Text}') or (department.name = '{dep.Text}' and patient.id_room = {i}))";
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
        private void Seventh(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"select patient.surname, patient.name, patient.patronymic from patient  
            join department on patient.id_dep = department.id 
            join ""Hospital"" on department.id_hospital = ""Hospital"".id 
            join healthcard on patient.id = healthcard.id_patient
            join patient_doc on patient.id = patient_doc.id_patient
            join med_staff on patient_doc.id_doc = med_staff.doc_id
            where(concat(med_staff.doc_id, ' ', med_staff.surname, ' ', med_staff.name, ' ', med_staff.patronymic) = '{doc.Text}' or ""Hospital"".name = '{hospital.Text}')
            and leave_date = '{ldate.SelectedDate.Value:dd.MM.yyyy}' and entry_date = '{edate.SelectedDate.Value:dd.MM.yyyy}'";
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
        private void Eighth(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"select patient.surname, patient.name, patient.patronymic from patient  
            join department on patient.id_dep = department.id 
            join ""Hospital"" on department.id_hospital = ""Hospital"".id 
            join patient_doc on patient.id = patient_doc.id_patient
            join med_staff on patient_doc.id_doc = med_staff.doc_id
            where ""Hospital"".name = '{hospital.Text}'
            and med_staff.major = '{major.Text}'";
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
        private void SelectedDep(object sender, EventArgs e)
        {
            if (dep.SelectedIndex != -1)
            {
                string s = dep.Text;
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"select distinct id_room from patient as p join department as d on p.id_dep = d.id where d.name = '{s}'";
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                com.Dispose();
                con.Close();
                room.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            }
        }
        private void SelectedMajor(object sender, EventArgs e)
        {
            if (major.SelectedIndex != -1)
            {
                string s = major.Text;
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"select concat(p.doc_id, ' ', p.surname, ' ', p.name, ' ', p.patronymic) as fullname from med_staff as p where p.major = '{s}'";
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                com.Dispose();
                con.Close();
                doc.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                dep.IsEnabled = true;
            }
        }
    }
}
