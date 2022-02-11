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
using System.Text.RegularExpressions;

namespace lab
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        
        public Window3()
        {
            InitializeComponent();
        }

        private void Exit3_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private double num1 = default;
        private double num2 = default;
        private string operation = default;

        private void Result_Click(object obj, EventArgs e)
        {
            tb.Text = default;
            try
            {
                tb.Text+=Result();
            }
            catch (Exception)
            {
                tb.Text = "Error!";
            }
        }
        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            tb.Text += b.Content.ToString();
            num2 = double.Parse(tb.Text);
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            tb.Text = default;
            operation = b.Content.ToString();
            num1 = num2;
            num2 = default;
        }
        private string Result()
        {
            double res = 0;
            switch (operation)
            {
                case "+":
                    res = num1 + num2;
                    break;
                case "-":
                    res = num1 - num2;
                    break;
                case "×":
                    res = num1 * num2;
                    break;
                case "÷":
                    try { res = num1 / num2; }
                    catch (DivideByZeroException) { }
                    break;
                case "%":
                    res = num1 % num2;
                    break;
            }
            num2 = res;
            return res.ToString();
        }
        private void Delete(object obj, EventArgs e)
        {
            tb.Text = default;
        }
        private void Backspace(object obj, EventArgs e)
        {
            if (tb.Text.Length > 0)
            {            
                tb.Text = tb.Text[0..^1];
                num2 = double.Parse(tb.Text);
            }
        }
        private void Sign(object obj, EventArgs e)
        {
            tb.Text = null;
            num2 *= -1;
            tb.Text = $"{num2}";
        }
    }
}
