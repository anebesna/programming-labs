using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Connections.xaml
    /// </summary>
    public partial class Connections : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        public Connections()
        {
            InitializeComponent();
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select name from ""Hospital"";";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            hospital1.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            hospital2.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select concat(p.doc_id, ' ', p.surname, ' ', p.name, ' ', p.patronymic) as fullname from med_staff as p;";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            doc1.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            doc2.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            doc3.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            doc4.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select concat(p.id, ' ', p.surname, ' ', p.name, ' ', p.patronymic) as fullname from patient as p;";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            patient1.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            patient2.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            UpdateDT();
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
        private void UpdateDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select concat(p.id_doc, ' ', m.surname, ' ', m.name, ' ', m.patronymic) as fullname, d.name as department, h.name as hospital, p.hire_date from doc_dep as p
            join med_staff as m on p.id_doc = m.doc_id
            join department as d on p.id_dep = d.id
            join ""Hospital"" as h on d.id_hospital = h.id";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            db1.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select concat(i.id_patient, ' ', p.surname, ' ', p.name, ' ', p.patronymic) as patient, concat(i.id_doc, ' ', m.surname, ' ', m.name, ' ', m.patronymic) as doctor, h.name from patient_doc as i
            join med_staff as m on i.id_doc = m.doc_id
            join patient as p on i.id_patient = p.id
            join ""Hospital"" as h on i.id_hospital = h.id";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            db2.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.Dispose();
            con.Close();
        }
        private void SelectedHospital(object sender, EventArgs e)
        {
            if (hospital1.SelectedIndex != -1)
            {
                string s = hospital1.Text;
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $@"SELECT d.name FROM department as d join ""Hospital"" as h on d.id_hospital = h.id where h.name = '{s}'";
                NpgsqlDataReader dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                dep1.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                dep1.IsEnabled = true;
                com.Dispose();
                con.Close();
            }
        }
        private void Add1(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into doc_dep (id_doc, id_dep, hire_date) values 
            ((select doc_id from med_staff where concat(med_staff.doc_id, ' ', med_staff.surname, ' ', med_staff.name, ' ', med_staff.patronymic) = '{doc1.Text}'),
            (select d.id from department as d join ""Hospital"" as h on d.id_hospital = h.id where h.name = '{hospital1.Text}' and d.name = '{dep1.Text}'),
            '{hire_date.SelectedDate.Value:dd.MM.yyyy}')";
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
        private void Delete1(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @$"delete from doc_dep using med_staff as m,
            department as d, ""Hospital"" as h
            where doc_dep.id_doc = m.doc_id and doc_dep.id_dep = d.id and d.id_hospital = h.id and concat(med_staff.doc_id, ' ', med_staff.surname, ' ', med_staff.name, ' ', med_staff.patronymic) = '{doc1.Text}' 
            and h.name = '{hospital1.Text}' and d.name = '{dep1.Text}'";
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
        private void Add2(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into patient_doc (id_doc, id_hospital, id_patient) values 
            ((select doc_id from med_staff where concat(med_staff.doc_id, ' ', med_staff.surname, ' ', med_staff.name, ' ', med_staff.patronymic) = '{doc2.Text}'),
            (select h.id from ""Hospital"" as h where h.name = '{hospital2.Text}'),
            (select id from patient where concat(id, ' ', surname, ' ', name, ' ', patronymic) = '{patient1.Text}'))";
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
        private void Delete2(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @$"delete from patient_doc as pd using med_staff as m, ""Hospital"" as h, patient as p 
            where pd.id_doc=m.doc_id and pd.id_hospital=h.id and pd.id_patient = p.id and concat(m.doc_id, ' ', m.surname, ' ', m.name, ' ', m.patronymic) = '{doc2.Text}' 
            and h.name = '{hospital2.Text}' and concat(p.id, ' ', p.surname, ' ', p.name, ' ', p.patronymic) = '{patient1.Text}'";
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
        private void Add3(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into operation (id_doc, type, result, id_patient) values 
            ((select doc_id from med_staff where concat(med_staff.doc_id, ' ', med_staff.surname, ' ', med_staff.name, ' ', med_staff.patronymic) = '{doc3.Text}'),
            '{type.Text}', '{result.Text}',
            (select id from patient where concat(id, ' ', surname, ' ', name, ' ', patronymic) = '{patient2.Text}'))";
            try
            {
                com.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Здається, деякі з полів незаповнені. Спробуйте ще!");
            }
            con.Close();
        }
        private void Delete3(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @$"delete from operation as pd using med_staff as m,
            patient as p where pd.id_doc = m.doc_id and pd.id_patient = p.id and concat(m.doc_id, ' ', m.surname, ' ', m.name, ' ', m.patronymic) = '{doc3.Text}' 
            and concat(p.id, ' ', p.surname, ' ', p.name, ' ', p.patronymic) = '{patient2.Text}'";
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
        private void deleteId1(object sender, EventArgs e)
        {
            if (idoperation.Text != "")
            {
                int ID = int.Parse(idoperation.Text);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $"delete from operation where id={ID}";
                try
                {
                    com.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Здається, даних за цим ID не існує. Спробуйте ще!");
                }
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            con.Close();
        }
        private void deleteId2(object sender, EventArgs e)
        {
            if (idvacation.Text != "")
            {
                int ID = int.Parse(idvacation.Text);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = $"delete from vacation where id={ID}";
                try
                {
                    com.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Здається, даних за цим ID не існує. Спробуйте ще!");
                }
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            con.Close();
        }
        private void Add4(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into vacation (id_doc, date_start, date_end) values 
            ((select doc_id from med_staff where concat(med_staff.doc_id, ' ', med_staff.surname, ' ', med_staff.name, ' ', med_staff.patronymic) = '{doc4.Text}'),
            '{start.SelectedDate.Value:dd.MM.yyyy}', '{end.SelectedDate.Value:dd.MM.yyyy}')";
            try
            {
                com.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Здається, деякі з полів незаповнені. Спробуйте ще!");
            }
            con.Close();
        }
        private void Delete4(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @$"delete from vacation as pd using med_staff as m 
            where pd.id_doc = m.doc_id and concat(m.doc_id, ' ', m.surname, ' ', m.name, ' ', m.patronymic) = '{doc4.Text}' 
            and date_start = '{start.SelectedDate.Value:dd.MM.yyyy}' and date_end = '{end.SelectedDate.Value:dd.MM.yyyy}'";
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
