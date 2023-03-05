using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace lab2
{
    public class Calculator : Window
    {
        TextBlock tb;
        Button button;
        public Calculator()
        {
            var bc = new BrushConverter();
            Title = "Calculator";
            Height = 590;
            Width = 400;
            Grid grid = new Grid();
            Content = grid;
            Uri iconUri = new Uri(@"icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\anebesna\\source\\repos\\lab2\\lab2\\bin\\Debug\\netcoreapp3.1\\background.jpeg"));
            Background = myBrush;
            Style style = new Style(typeof(Border));
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(15)));
            Button Exit = new Button
            {
                Content = "Вихід",
                Margin = new Thickness(0, 470, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 44,
                Width = 132,
                FontSize = 26,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Exit.Click += new RoutedEventHandler(Exit_Click);
            Exit.Resources.Add(typeof(Border), style);
            grid.Children.Add(Exit);
            Border border = new Border
            {
                BorderBrush = (Brush)bc.ConvertFrom("#FFF9C4C4"),
                BorderThickness = new Thickness(1),
                VerticalAlignment = VerticalAlignment.Top,
                Height = 51,
                Margin = new Thickness(41, 23, 40, 0),
            };
            grid.Children.Add(border);
            tb = new TextBlock()
            {
                Text = "0",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Height = 48,
                Width = 299,
                FontSize = 34,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = new FontFamily("Yu Gothic UI Semilight"),
                RenderTransformOrigin = new Point(0.552, 0.798)
            };
            border.Child = tb;
            Grid grid2 = new Grid()
            {
                Width = 319,
                Height = 359,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 92, 0, 0)
            };
            grid.Children.Add(grid2);
            ColumnDefinition Col1 = new ColumnDefinition();
            ColumnDefinition Col2 = new ColumnDefinition();
            ColumnDefinition Col3 = new ColumnDefinition();
            ColumnDefinition Col4 = new ColumnDefinition();
            Col4.Width = new GridLength(55);
            grid2.ColumnDefinitions.Add(Col1);
            grid2.ColumnDefinitions.Add(Col2);
            grid2.ColumnDefinitions.Add(Col3);
            grid2.ColumnDefinitions.Add(Col4);
            RowDefinition Row1 = new RowDefinition();
            Row1.Height = new GridLength(45);
            RowDefinition Row2 = new RowDefinition();
            RowDefinition Row3 = new RowDefinition();
            RowDefinition Row4 = new RowDefinition();
            RowDefinition Row5 = new RowDefinition();
            grid2.RowDefinitions.Add(Row1);
            grid2.RowDefinitions.Add(Row2);
            grid2.RowDefinitions.Add(Row3);
            grid2.RowDefinitions.Add(Row4);
            grid2.RowDefinitions.Add(Row5);
            string[,] elements = { { "C", "=", "%", "⌫" }, { "7", "8", "9", "÷" }, { "4", "5", "6", "×" }, { "1", "2", "3", "-" }, { "+/-", "0", ".", "+" } };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (elements[i, j] == "%" || elements[i, j] == "÷" || elements[i, j] == "×" || elements[i, j] == "-" || elements[i, j] == "+")
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FF2C5AA0"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(Btn_Click);
                    }
                    else if (elements[i, j] == "=")
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FF2C5AA0"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(Result_Click);
                    }
                    else if (elements[i, j] == "C")
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FF2C5AA0"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(Delete);
                    }
                    else if (elements[i, j] == "+/-")
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FF2C5AA0"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(Sign);
                    }
                    else if (elements[i, j] == "⌫")
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FF2C5AA0"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(Backspace);
                    }
                    else if (elements[i, j] == ".")
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FF2C5AA0"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(Dot_Click);
                    }
                    else
                    {
                        button = new Button
                        {
                            Content = elements[i, j],
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 30,
                            BorderBrush = (Brush)bc.ConvertFrom("#FFF9C4C4"),
                            Background = null,
                        };
                        button.Click += new RoutedEventHandler(NumBtn_Click);
                    }
                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    grid2.Children.Add(button);
                }
            }
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
                tb.Text += Result();
            }
            catch (Exception)
            {
                tb.Text = "Error!";
            }
        }
        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (tb.Text == "0") tb.Text = b.Content.ToString();
            else
            {
                tb.Text += b.Content.ToString();
            }
            num2 = double.Parse(tb.Text);
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            tb.Text = b.Content.ToString();
            operation = b.Content.ToString();
            num1 = num2;
            tb.Text = "0";
        }
        private void Dot_Click(object o, EventArgs e)
        {
            Button b = (Button)o;
            tb.Text += b.Content.ToString();
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
                if (tb.Text == "") tb.Text = "0";
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