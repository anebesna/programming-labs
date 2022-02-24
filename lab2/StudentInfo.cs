using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace lab2
{
    public class StudentInfo : Window
    {
        private Label label1, label2, label3, label4, label5, label6;
        private TextBox tb1, tb2, tb3, tb4;
        public StudentInfo()
        {
            var bc = new BrushConverter();
            Title = "StudentInfo";
            Height = 430;
            Width = 800;
            Grid grid = new Grid();
            Content = grid;
            Uri iconUri = new Uri(@"icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\anebesna\\source\\repos\\lab2\\lab2\\bin\\Debug\\netcoreapp3.1\\background.jpeg"));
            Background = myBrush;
            label1 = new Label
            {
                Content = "Додати",
                Margin = new Thickness(138, 3, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 38,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI Light")
            };
            grid.Children.Add(label1);
            label2 = new Label
            {
                Content = "Видалити",
                Margin = new Thickness(514, 3, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 38,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI Light")
            };
            grid.Children.Add(label2);
            label3 = new Label
            {
                Content = "ПІП",
                Margin = new Thickness(73, 95, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 25,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label3);
            label4 = new Label
            {
                Content = "№ ЗК",
                Margin = new Thickness(73, 157, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 25,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label4);
            label5 = new Label
            {
                Content = "Група",
                Margin = new Thickness(73, 217, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 25,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label5);
            label6 = new Label
            {
                Content = "№ ЗК",
                Margin = new Thickness(467, 108, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 25,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label6);
            Style style = new Style(typeof(Border));
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(25)));
            Button Exit = new Button
            {
                Content = "Вихід з програми",
                Margin = new Thickness(466, 269, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 54,
                Width = 262,
                FontSize = 30,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Exit.Click += new RoutedEventHandler(Exit_Click);
            Exit.Resources.Add(typeof(Border), style);
            grid.Children.Add(Exit);
            Rectangle rect1 = new Rectangle
            {
                Height = 259,
                Width = 289,
                Margin = new Thickness(59, 64, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Stroke = (Brush)bc.ConvertFrom("#FFF8C3C4")
            };
            grid.Children.Add(rect1);
            Rectangle rect2 = new Rectangle
            {
                Height = 153,
                Width = 289,
                Margin = new Thickness(0, 64, 59, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Stroke = (Brush)bc.ConvertFrom("#FFF8C3C4")
            };
            grid.Children.Add(rect2);
            tb1 = new TextBox
            {
                Height = 30,
                Width = 203,
                Margin = new Thickness(129, 102, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 22,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF7C2C3"),
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
                TextWrapping = TextWrapping.Wrap,
                Text = ""
            };
            grid.Children.Add(tb1);
            tb2 = new TextBox
            {
                Height = 30,
                Width = 180,
                Margin = new Thickness(152, 227, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 22,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF7C2C3"),
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
                TextWrapping = TextWrapping.Wrap,
                Text = ""
            };
            grid.Children.Add(tb2);
            tb3 = new TextBox
            {
                Height = 30,
                Width = 182,
                Margin = new Thickness(150, 166, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 22,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF7C2C3"),
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
                TextWrapping = TextWrapping.Wrap,
                Text = ""
            };
            grid.Children.Add(tb3);
            tb4 = new TextBox
            {
                Height = 30,
                Width = 203,
                Margin = new Thickness(544, 116, 72, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 22,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF7C2C3"),
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
                TextWrapping = TextWrapping.Wrap,
                Text = ""
            };
            grid.Children.Add(tb4);
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(17)));
            Button Add = new Button
            {
                Content = "Додати",
                Margin = new Thickness(143, 278, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 34,
                Width = 120,
                FontSize = 20,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Add.Click += new RoutedEventHandler(Add_Click);
            Add.Resources.Add(typeof(Border), style);
            grid.Children.Add(Add);
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(17)));
            Button Delete = new Button
            {
                Content = "Видалити",
                Margin = new Thickness(536, 173, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 34,
                Width = 120,
                FontSize = 20,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Delete.Click += new RoutedEventHandler(Delete_Click);
            Delete.Resources.Add(typeof(Border), style);
            grid.Children.Add(Delete);
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
