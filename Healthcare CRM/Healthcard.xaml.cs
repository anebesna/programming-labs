using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for Healthcard.xaml
    /// </summary>
    public partial class Healthcard : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static int ID;
        public Healthcard()
        {
            InitializeComponent();
            con.Open();
            UpdateDT();
        }
        private void Add(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into healthcard (gender, address, entry_date, complaints, blood_type, id, id_patient) values ('{gender.Text}', '{address.Text}', '{edate.SelectedDate.Value:dd.MM.yyyy}', '{complaints.Text}', '{blood.Text}', {int.Parse(card.Text)}, {int.Parse(patient.Text)})";
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
        private void Back(object sender, EventArgs e)
        {
            Close();
            Patients p = new Patients();
            p.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            p.Show();
        }
        private void Current(object sender, EventArgs e)
        {
            Close();
            CurrentState c = new CurrentState();
            c.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            c.Show();
        }
        private void Intervention(object sender, EventArgs e)
        {
            Close();
            MedInterventions m = new MedInterventions();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private void Clear(object sender, EventArgs e)
        {
            name.Text = null;
            address.Text = null;
            blood.Text = null;
            gender.SelectedIndex = -1;
            gender.IsEnabled = true;
            complaints.Text = null;
            edate.SelectedDate = null;
            card.Text = null;
            patient.Text = null;
            add.IsEnabled = true;
            con.Open();
            UpdateDT();
        }
        private void UpdateDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select h.id, concat(p.surname, ' ', p.name, ' ', p.patronymic) as fullname, h.gender, h.address, h.entry_date, h.leave_date, h.complaints, h.blood_type from healthcard as h join patient as p on h.id_patient=p.id ";
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
                com.CommandText = $@"select h.id, concat(p.surname, ' ', p.name, ' ', p.patronymic) as fullname, h.gender, h.address, h.entry_date, h.leave_date, h.complaints, h.blood_type from healthcard as h join patient as p on h.id_patient=p.id where h.id={ID} order by h.id desc";
                try
                {
                    NpgsqlDataReader dr = com.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);
                    com.Dispose();
                    db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                    card.Text = dt.Rows[0][0].ToString();
                    name.Text = dt.Rows[0][1].ToString();
                    gender.Text = dt.Rows[0][2].ToString();
                    address.Text = dt.Rows[0][3].ToString();
                    complaints.Text = dt.Rows[0][6].ToString();
                    edate.SelectedDate = (DateTime)dt.Rows[0][4];
                    blood.Text = dt.Rows[0][7].ToString();
                    add.IsEnabled = false;
                    gender.IsEnabled = false;
                }
                catch
                {
                    MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
                }
            }
            else MessageBox.Show("Здається, такого ID не існує. Спробуйте ще!");
            con.Close();
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
