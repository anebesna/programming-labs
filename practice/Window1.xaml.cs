using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;

namespace practice
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StudyModeWindow : Window
    {
        public StudyModeWindow()
        {
            InitializeComponent();
            File.WriteAllText("models.txt", string.Empty);
        }
        private void Restart()
        {
            count = 0;
            pressed = false;
            intervals = new List<TimeSpan>();
            SymbolCount.Content = count.ToString();
        }
        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
        bool pressed = false;
        List<TimeSpan> intervals = new List<TimeSpan>();
        private Stopwatch sw;
        static readonly string keyword = "чебупели";
        int count = 0;
        int tries;
        bool restarted = false;
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (tries > 0)
            {
                if (restarted)
                {
                    InputField.Text = default;
                    restarted = false;
                }
                if (InputField.Text != keyword.Substring(0, count)|| InputField.Text.Length > keyword.Length)
                {
                    MessageBox.Show("Wrong input. Try again!");
                    Restart();
                    restarted = true;
                }
                else
                {
                    count++;
                    SymbolCount.Content = count.ToString();
                    if (!pressed)
                    {
                        sw = new Stopwatch();
                        sw.Start();
                        pressed = true;
                    }
                    else
                    {
                        sw.Stop();
                        intervals.Add(sw.Elapsed);
                        sw.Restart();
                    }
                    if (InputField.Text == keyword)
                    {
                        MakeStat();
                        tries--;
                        MessageBox.Show($"Left tries: {tries}");
                        Restart();
                        restarted = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(s);
                InputField.IsEnabled = false;
            }
        }
        string s = default;
        private void MakeStat()
        {
            double M = 0;
            double S = 0;
            double t;
            bool check = true;
            for (int i = 0; i < intervals.Count; i++)
            {
                for (int j = 0; j < intervals.Count; j++)
                {
                    if (intervals.IndexOf(intervals[j]) == i) continue;
                    M += intervals[j].TotalSeconds / (intervals.Count - 1);
                }
                for (int j = 0; j < intervals.Count; j++)
                {
                    if (intervals.IndexOf(intervals[j]) == i) continue;
                    S += Math.Pow(intervals[j].TotalSeconds - M, 2)/ (intervals.Count - 2);
                }
                S = Math.Sqrt(S);
                t = Math.Abs((intervals[i].TotalSeconds - M) / S / Math.Sqrt(intervals.Count - 1));
                if (t > 2.57)
                {
                    check = false;
                    break;
                }
                M = 0;
                S = 0;
            }
            if (check)
            {
                for (int i = 0; i < intervals.Count; i++) M += intervals[i].TotalSeconds / intervals.Count;
                for (int i = 0; i < intervals.Count; i++) S += Math.Pow(intervals[i].TotalSeconds - M, 2) / (intervals.Count - 1);
                using (StreamWriter sw = new StreamWriter("models.txt", true))
                {
                    sw.WriteLine($"{M} {S}");
                    s+= $"{M} {S}\n";
                }
            }
            else
            {
                MessageBox.Show("Try again!");
                tries++;
                Restart();
                restarted = true;
            }
        }
        private void TriesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tries = int.Parse(((ComboBoxItem)TriesBox.SelectedItem).Content.ToString());
        }
    }
}
