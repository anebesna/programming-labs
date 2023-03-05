using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace lab2
{
    internal class DevInfo : Window
    {
        private Label label1, label2, label3, label4, label5, label6;
        public DevInfo()
        {
            var bc = new BrushConverter();
            Title = "DevInfo";
            Height = 400;
            Width = 650;
            Grid grid = new Grid();
            Content = grid;
            Uri iconUri = new Uri(@"icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\anebesna\\source\\repos\\lab2\\lab2\\bin\\Debug\\netcoreapp3.1\\background.jpeg"));
            Background = myBrush;
            label1 = new Label
            {
                Content = "Інформація про розробника",
                Margin = new Thickness(0, 26, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 35,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = new FontFamily("Yu Gothic UI Light")
            };
            grid.Children.Add(label1);
            label2 = new Label
            {
                Content = "ПІП: Небесна Анна Андріївна",
                Margin = new Thickness(23, 100, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label2);
            label3 = new Label
            {
                Content = "Дата народження: 17.11.2003",
                Margin = new Thickness(23, 137, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label3);
            label4 = new Label
            {
                Content = "Місце навчання: КПІ ім. Ігоря Сікорського",
                Margin = new Thickness(23, 174, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label4);
            label5 = new Label
            {
                Content = "Факультет/група: ФПМ/КП-11",
                Margin = new Thickness(23, 211, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label5);
            label6 = new Label
            {
                Content = "Рік створення застосунку: 2022",
                Margin = new Thickness(23, 248, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight")
            };
            grid.Children.Add(label6);
            Style style = new Style(typeof(Border));
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(15)));
            Button Exit = new Button
            {
                Content = "Повернутися до головного вікна",
                Margin = new Thickness(0, 325, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 44,
                Width = 367,
                FontSize = 23,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Exit.Click += new RoutedEventHandler(Exit_Click);
            Exit.Resources.Add(typeof(Border), style);
            grid.Children.Add(Exit);
            Image img = new Image
            {
                Source = new BitmapImage(new Uri(@"C:\Users\anebesna\source\repos\lab2\lab2\bin\Debug\netcoreapp3.1\girl.png")),
                Margin = new Thickness(347, 118, -33, 83)
            };
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            flipTrans.ScaleX = -1;
            img.RenderTransform = flipTrans;
            grid.Children.Add(img);
            Separator s = new Separator
            {
                Height = 45,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Background = (Brush)bc.ConvertFrom("#FFF8C3C4"),
                Margin = new Thickness(0, 63, 0, 0),
                Width = 590
            };
            grid.Children.Add(s);
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mw.Show();
        }
    }
}