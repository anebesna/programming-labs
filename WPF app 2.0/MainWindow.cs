using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace lab2
{
    public class MainWindow : Window
    {
        private Label label1;
        public MainWindow()
        {
            Title = "Main Window";
            Height = 510;
            Width = 910;
            Grid grid = new Grid();
            Content = grid;
            Uri iconUri = new Uri(@"icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\anebesna\\source\\repos\\lab2\\lab2\\bin\\Debug\\netcoreapp3.1\\background.jpeg"));
            Background = myBrush;
            label1 = new Label
            {
                Content = "Головне вікно",
                Width = 376,
                Height = 80,
                Margin = new Thickness(0, 10, 0, 0),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 60,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = new FontFamily("Yu Gothic UI Light")
            };
            grid.Children.Add(label1);
            var bc = new BrushConverter();
            Separator s = new Separator
            {
                Height = 23,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Background = (Brush)bc.ConvertFrom("#FFF8C3C4"),
                Margin = new Thickness(0, 87, 0, 0),
                Width = 376
            };
            grid.Children.Add(s);
            Button StudentInfo = new Button
            {
                Content = "StudentInfo",
                Margin = new Thickness(46, 146, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 80,
                Width = 200,
                FontSize = 40,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF8C3C4"),
                Background = null,
                Foreground = new SolidColorBrush(Colors.White)
            };
            StudentInfo.Click += new RoutedEventHandler(StudentInfo_Click);
            grid.Children.Add(StudentInfo);
            Button TicTacToe = new Button
            {
                Content = "TicTacToe",
                Margin = new Thickness(248, 260, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 80,
                Width = 200,
                FontSize = 40,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF8C3C4"),
                Background = null,
                Foreground = new SolidColorBrush(Colors.White)
            };
            TicTacToe.Click += new RoutedEventHandler(TicTacToe_Click);
            grid.Children.Add(TicTacToe);
            Button Calc = new Button
            {
                Content = "Calculator",
                Margin = new Thickness(454, 146, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 80,
                Width = 200,
                FontSize = 40,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF8C3C4"),
                Background = null,
                Foreground = new SolidColorBrush(Colors.White)
            };
            Calc.Click += new RoutedEventHandler(Calc_Click);
            grid.Children.Add(Calc);
            Button DevInfo = new Button
            {
                Content = "DevInfo",
                Margin = new Thickness(658, 260, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 80,
                Width = 200,
                FontSize = 40,
                FontFamily = new FontFamily("Yu Gothic UI Light"),
                BorderBrush = (Brush)bc.ConvertFrom("#FFF8C3C4"),
                Background = null,
                Foreground = new SolidColorBrush(Colors.White)
            };
            DevInfo.Click += new RoutedEventHandler(DevInfo_Click);
            grid.Children.Add(DevInfo);
            Style style = new Style(typeof(Border));
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(25)));
            Button Exit = new Button
            {
                Content = "Вихід з програми",
                Margin = new Thickness(0, 424, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
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
            Image img1 = new Image
            {
                Width = 116,
                Height = 116,
                Source = new BitmapImage(new Uri(@"C:\Users\anebesna\source\repos\lab2\lab2\bin\Debug\netcoreapp3.1\pointer.png")),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(138, 230, 0, 0)
            };
            img1.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            flipTrans.ScaleX = -1;
            img1.RenderTransform = flipTrans;
            grid.Children.Add(img1);
            Image img2 = new Image
            {
                Width = 116,
                Height = 116,
                Source = new BitmapImage(new Uri(@"C:\Users\anebesna\source\repos\lab2\lab2\bin\Debug\netcoreapp3.1\pointer.png")),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(348, 155, 0, 0)
            };
            ScaleTransform flipTrans1 = new ScaleTransform();
            img2.RenderTransformOrigin = new Point(0.5, 0.5);
            flipTrans1.ScaleX = -1;
            flipTrans1.ScaleY = -1;
            img2.RenderTransform = flipTrans1;
            grid.Children.Add(img2);
            Image img3 = new Image
            {
                Width = 116,
                Height = 116,
                Source = new BitmapImage(new Uri(@"C:\Users\anebesna\source\repos\lab2\lab2\bin\Debug\netcoreapp3.1\pointer.png")),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(542, 230, 0, 0)
            };
            img3.RenderTransformOrigin = new Point(0.5, 0.5);
            img3.RenderTransform = flipTrans;
            grid.Children.Add(img3);
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void StudentInfo_Click(object sender, RoutedEventArgs e)
        {
            StudentInfo win1 = new StudentInfo();
            win1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win1.Show();
            Hide();
        }
        private void TicTacToe_Click(object sender, EventArgs e)
        {
            TicTacToe win2 = new TicTacToe();
            win2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win2.Show();
            Hide();
        }
        private void Calc_Click(object sender, EventArgs e)
        {
            Calculator win3 = new Calculator();
            win3.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win3.Show();
            Hide();
        }
        private void DevInfo_Click(object sender, EventArgs e)
        {
            DevInfo win4 = new DevInfo();
            win4.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win4.Show();
            Hide();
        }
    }
}