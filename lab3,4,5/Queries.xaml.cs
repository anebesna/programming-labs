using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Queries.xaml
    /// </summary>
    public partial class Queries : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        public Queries()
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
            hospital.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select distinct major from med_staff;";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            major.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.CommandText = @"select distinct category from staff;";
            dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            category.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
            com.Dispose();
            con.Close();
        }
        private void All(object sender, EventArgs e)
        {
            hospital.Text = "";
        }
        private void All2(object sender, EventArgs e) 
        {
            rank.Text = "";
        }
        private void toMain(object sender, EventArgs e)
        {
            Close();
            Menu m = new Menu();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void Other(object sender, EventArgs e) 
        {
            Close();
            Queries2 q = new Queries2();
            q.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            q.Show();
        }
        private void First(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            string str;
            if (hospital.Text == "") str = "";
            else str = $@"and ""Hospital"".name = '{hospital.Text}'";
            com.CommandText = $@"select m.surname, m.name, m.patronymic, count(*) over() as total from med_staff as m " +
            "join doc_dep on m.doc_id = doc_dep.id_doc " +
            "join department on doc_dep.id_dep = department.id " +
            @"join ""Hospital"" on department.id_hospital = ""Hospital"".id " +
            $"where major = '{major.Text}' {str}";
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
        private void Second(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            string str;
            if (hospital.Text == "") str = "";
            else str = $@"and ""Hospital"".name = '{hospital.Text}'";
            com.CommandText = $@"select s.surname, s.name, s.patronymic, count(*) over() as total from staff as s
            join ""Hospital"" on s.id_hospital = ""Hospital"".id
            where category = '{category.Text}' {str}";
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
        private void Third(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            string str;
            if (hospital.Text == "") str = "";
            else str = $@"and ""Hospital"".name = '{hospital.Text}'";
            com.CommandText = $@"select m.surname, m.name, m.patronymic, count(*) over() as total from med_staff as m " +
            "join doc_dep on m.doc_id = doc_dep.id_doc " +
            "join department on doc_dep.id_dep = department.id " +
            @"join ""Hospital"" on department.id_hospital = ""Hospital"".id " +
            "join operation on m.doc_id = operation.id_doc " +
            $"where major = '{major.Text}' {str} group by m.doc_id having count(operation.id) > {int.Parse(count.Text)}";
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
        private void Fourth(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            string str;
            if (hospital.Text == "") str = "";
            else str = $@"and ""Hospital"".name = '{hospital.Text}'";
            com.CommandText = $@"select m.surname, m.name, m.patronymic, count(*) over() as total from med_staff as m " +
            "join doc_dep on m.doc_id = doc_dep.id_doc " +
            "join department on doc_dep.id_dep = department.id " +
            @"join ""Hospital"" on department.id_hospital = ""Hospital"".id " +
            $"where major = '{major.Text}' {str} and extract(years from age((select current_date), hire_date))>{int.Parse(expirience.Text)}";
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
        private void Fifth(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            string str;
            string r;
            if (rank.Text == "") r = "";
            else r = $"and m.rank = '{rank.Text}'";
            if (hospital.Text == "") str = "";
            else str = $@"and ""Hospital"".name = '{hospital.Text}'";
            com.CommandText = $@"select m.surname, m.name, m.patronymic, count(*) over() as total from med_staff as m " +
            "join doc_dep on m.doc_id = doc_dep.id_doc " +
            "join department on doc_dep.id_dep = department.id " +
            @"join ""Hospital"" on department.id_hospital = ""Hospital"".id " +
            $"where major = '{major.Text}' {str} and m.degree = '{degree.Text}' {r}";
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
