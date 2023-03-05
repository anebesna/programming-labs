using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for MedInterventions.xaml
    /// </summary>
    public partial class MedInterventions : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static int ID;
        public MedInterventions()
        {
            InitializeComponent();
            con.Open();
            UpdateDT();
        }
        private void UpdateDT()
        {
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = @"select i.id_card, concat(p.surname, ' ', p.name, ' ', p.patronymic) as fullname, i.type, i.result, i.date, concat(m.surname, ' ', m.name, ' ', m.patronymic) as doctor from med_intervention as i join healthcard as h on i.id_card = h.id join patient as p on h.id_patient = p.id join med_staff as m on i.id_doc = m.doc_id";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void Back(object sender, EventArgs e)
        {
            Close();
            Healthcard h = new Healthcard();
            h.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            h.Show();
        }
        private void Clear(object sender, EventArgs e)
        {
            date.SelectedDate = null;
            type.Text = null;
            result.Text = null;
            card.Text = null;
            doc.Text = null;
            add.IsEnabled = true;
            con.Open();
            UpdateDT();
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
                com.CommandText = $@"select i.id_card, concat(p.surname, ' ', p.name, ' ', p.patronymic) as fullname, i.type, i.result, i.date, concat(m.surname, ' ', m.name, ' ', m.patronymic) as doctor from med_intervention as i join healthcard as h on i.id_card = h.id join patient as p on h.id_patient = p.id join med_staff as m on i.id_doc = m.doc_id where i.id_card = {ID} order by date desc;";
                try
                {
                    NpgsqlDataReader dr = com.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);
                    com.Dispose();
                    db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                    card.Text = dt.Rows[0][0].ToString();
                    type.Text = dt.Rows[0][2].ToString();
                    result.Text = dt.Rows[0][3].ToString();
                    date.SelectedDate = (DateTime)dt.Rows[0][4];
                    add.IsEnabled = false;
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
        private void Add(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into med_intervention (date, type, result, id_doc, id_card) values ('{date.SelectedDate.Value:dd.MM.yyyy}', '{type.Text}', '{result.Text}', {int.Parse(doc.Text)}, {int.Parse(card.Text)})";
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
    }
}
