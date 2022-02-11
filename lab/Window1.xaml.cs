using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace lab
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void Exit1_Click(object obj, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            Close();
            mw.Show();
        }
        private void Add_Click(object obj, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("students.txt", true))
            {
                string name = tb1.Text;
                string id = tb2.Text;
                string group = tb3.Text;
                tb1.Text = default;
                tb2.Text = default;
                tb3.Text = default;
                string line = $"{name}\t{id}\t{group}\n";
                sw.Write(line);
            }
        }
        private void Delete_Click(object obj, EventArgs e)
        {
            string id = tb4.Text;
            string tempFile = System.IO.Path.GetTempFileName();
            using (StreamReader sr = new StreamReader("students.txt"))
            {
                using (var sw = new StreamWriter(tempFile))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.Contains(id))
                            sw.WriteLine(line);
                    }
                }
            }
            File.Delete("students.txt");
            File.Move(tempFile, "students.txt");
            tb4.Text = default;
        }
    }
}