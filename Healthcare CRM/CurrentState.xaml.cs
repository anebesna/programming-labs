using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;
using System.Data;

namespace lab3_4_5
{
    /// <summary>
    /// Interaction logic for CurrentState.xaml
    /// </summary>
    public partial class CurrentState : Window
    {
        static NpgsqlConnection con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=wolikesh;Database=medDB;");
        static DataTable dt;
        static int ID;
        public CurrentState()
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
            com.CommandText = @"select c.id_card, concat(p.surname, ' ', p.name, ' ', p.patronymic) as fullname, c.state, c.temperature, c.medicines, c.time, concat(m.surname, ' ', m.name, ' ', m.patronymic) as doctor from current_state as c join healthcard as h on c.id_card = h.id join patient as p on h.id_patient = p.id join med_staff as m on c.id_doc = m.doc_id";
            NpgsqlDataReader dr = com.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            com.Dispose();
            con.Close();
            db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
        private void Clear(object sender, EventArgs e)
        {
            date.SelectedDate = null;
            time.Text = null;
            state.Text = null;
            medicines.Text = null;
            card.Text = null;
            doc.Text = null;
            add.IsEnabled = true;
            con.Open();
            UpdateDT();
        }
        private void Add(object sender, EventArgs e)
        {
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = $@"insert into current_state (time, state, temperature, medicines, id_doc, id_card) values ('{date.SelectedDate.Value:dd.MM.yyyy} {time.Text}', '{state.Text}', '{temp.Text}', '{medicines.Text}', {int.Parse(doc.Text)}, {int.Parse(card.Text)})";
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
            Healthcard h = new Healthcard();
            h.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            h.Show();
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
                com.CommandText = $@"select c.id_card, concat(p.surname, ' ', p.name, ' ', p.patronymic) as fullname, c.state, c.temperature, c.medicines, c.time, concat(m.surname, ' ', m.name, ' ', m.patronymic) as doctor from current_state as c join healthcard as h on c.id_card = h.id join patient as p on h.id_patient = p.id join med_staff as m on c.id_doc = m.doc_id where c.id_card = {ID} order by time desc";
                try
                {
                    NpgsqlDataReader dr = com.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dr);
                    com.Dispose();
                    db.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
                    card.Text = dt.Rows[0][0].ToString();
                    state.Text = dt.Rows[0][2].ToString();
                    temp.Text = dt.Rows[0][3].ToString();
                    medicines.Text = dt.Rows[0][4].ToString();
                    DateTime d = DateTime.Parse(dt.Rows[0][5].ToString());
                    date.SelectedDate = DateTime.Parse(d.ToString("dd/MM/yyyy"));
                    time.Text = d.ToString("H:mm:ss");
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
    }
}
