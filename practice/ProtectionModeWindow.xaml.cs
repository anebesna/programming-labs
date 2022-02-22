using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace practice
{
    /// <summary>
    /// Interaction logic for ProtectionModeWindow.xaml
    /// </summary>
    public partial class ProtectionModeWindow : Window
    {
        public ProtectionModeWindow()
        {
            InitializeComponent();
        }
        private void CloseProtectionMode_Click(object sender, RoutedEventArgs e)
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
        double alpha;
        bool restarted = false;
        int x1 = 0;
        int x2 = 0;
        double buf;
        private void Restart()
        {
            count = 0;
            pressed = false;
            intervals = new List<TimeSpan>();
            SymbolCount.Content = count.ToString();
        }
        private void TriesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tries = int.Parse(((ComboBoxItem)TriesBox.SelectedItem).Content.ToString());
            buf = tries;
        }
        private void AlphaSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alpha = double.Parse(((ComboBoxItem)AlphaSelector.SelectedItem).Content.ToString());
        }
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (tries > 0)
            {
                if (restarted)
                {
                    InputField.Text = default;
                    restarted = false;
                }
                if (InputField.Text != keyword.Substring(0, count) || InputField.Text.Length > keyword.Length)
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
                MessageBox.Show("That's all!");
                InputField.IsEnabled = false;
                if (x1 > x2) DispField.Content = "oднорідні";
                else DispField.Content = "неoднорідні";
                StatisticsBlock.Content = $"{Math.Round(r / (buf*lines), 3)}";
                P1Field.Content = $"{Math.Round((buf - p1) / buf, 3)}";
                P2Field.Content = $"{Math.Round(p1 / buf, 3)}";
            }
        }
        double r = 0;
        int lines;
        double p1 = 0;
        private void MakeStat()
        {
            double M = 0;
            double S = 0;
            double t;
            double tt = 0;
            double Sf;
            switch(alpha){
                case 0.1: 
                    tt = 2.01;
                    break;
                case 0.05:
                    tt = 2.57;
                    break;
                case 0.01:
                    tt = 4.03;
                    break;
            }
            using (StreamReader sr = new StreamReader("models.txt"))
            {
                lines = File.ReadLines("models.txt").Count();
                double[] Me = new double[lines];
                double[] Se = new double[lines];
                string line;
                bool success = false;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] temp = line.Split(" ");
                    Me[i] = double.Parse(temp[0]);
                    Se[i] = double.Parse(temp[1]);
                    i++;
                }
                for (int j = 0; j < intervals.Count; j++) M += intervals[j].TotalSeconds / intervals.Count;
                for (int j = 0; j < intervals.Count; j++) S += Math.Pow(intervals[j].TotalSeconds - M, 2) / (intervals.Count - 1);
                for (int j = 0; j < lines; j++)
                {
                    double F = Math.Max(Se[j], S) / Math.Min(Se[j], S);
                    if (F > 3.79) x2++;
                    else x1++;
                    Sf = Math.Sqrt((Se[j] + S) * (intervals.Count - 1) / (2 * intervals.Count - 1));
                    t = Math.Abs(Me[j] - M) / (Sf * Math.Sqrt(2.0 / intervals.Count));
                    if (t < tt)
                    {
                        r++;
                        success = true;
                    }
                    
                }
                if (success) p1++;
            }
        }
    }
}
