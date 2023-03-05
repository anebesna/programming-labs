using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace lab2
{
    public class TicTacToe : Window
    {
        private enum Players
        {
            O,
            X,
            None
        }
        Button button;
        private static Players current = default;
        private static int clicks;
        private bool b = false;
        public Button[,] array = new Button[5, 5];
        Label label;
        Grid grid2;
        public TicTacToe()
        {
            var bc = new BrushConverter();
            Title = "TicTacToe";
            Height = 600;
            Width = 550;
            Grid grid = new Grid();
            Content = grid;
            Uri iconUri = new Uri(@"icon.ico", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\anebesna\\source\\repos\\lab2\\lab2\\bin\\Debug\\netcoreapp3.1\\background.jpeg"));
            Background = myBrush;
            label = new Label()
            {
                Name = "label",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(68, 8, 0, 0),
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 22
            };
            grid.Children.Add(label);
            Style style = new Style(typeof(Border));
            style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(15)));
            Button Restart = new Button
            {
                Content = "Restart",
                Margin = new Thickness(222, 10, 222, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 37,
                Width = 132,
                FontSize = 22,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Restart.Click += new RoutedEventHandler(Restart_Click);
            Restart.Resources.Add(typeof(Border), style);
            grid.Children.Add(Restart);
            Style style1 = new Style(typeof(Border));
            style1.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(15)));
            Button Exit = new Button
            {
                Content = "Вихід",
                Margin = new Thickness(222, 516, 222, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 37,
                Width = 132,
                FontSize = 22,
                FontFamily = new FontFamily("Yu Gothic UI SemiLight"),
                BorderBrush = null,
                Background = (Brush)bc.ConvertFrom("#FFD5E5FF"),
                Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
            };
            Exit.Click += new RoutedEventHandler(Exit_Click);
            Exit.Resources.Add(typeof(Border), style1);
            grid.Children.Add(Exit);
            grid2 = new Grid()
            {
                Margin = new Thickness(68, 59, 68, 93),
                Name = "grid"
            };
            grid.Children.Add(grid2);
            ColumnDefinition Col1 = new ColumnDefinition();
            ColumnDefinition Col2 = new ColumnDefinition();
            ColumnDefinition Col3 = new ColumnDefinition();
            ColumnDefinition Col4 = new ColumnDefinition();
            ColumnDefinition Col5 = new ColumnDefinition();
            grid2.ColumnDefinitions.Add(Col1);
            grid2.ColumnDefinitions.Add(Col2);
            grid2.ColumnDefinitions.Add(Col3);
            grid2.ColumnDefinitions.Add(Col4);
            grid2.ColumnDefinitions.Add(Col5);
            RowDefinition Row1 = new RowDefinition();
            RowDefinition Row2 = new RowDefinition();
            RowDefinition Row3 = new RowDefinition();
            RowDefinition Row4 = new RowDefinition();
            RowDefinition Row5 = new RowDefinition();
            grid2.RowDefinitions.Add(Row1);
            grid2.RowDefinitions.Add(Row2);
            grid2.RowDefinitions.Add(Row3);
            grid2.RowDefinitions.Add(Row4);
            grid2.RowDefinitions.Add(Row5);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    button = new Button
                    {
                        FontSize = 60,
                        Foreground = (Brush)bc.ConvertFrom("#FF162D50"),
                        BorderBrush = (Brush)bc.ConvertFrom("#FFF9C4C4"),
                        FontFamily = new FontFamily("Yu Gothic UI Light"),
                        Background = null
                    };
                    button.Click += new RoutedEventHandler(Click);
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
        private void Restart_Click(object o, EventArgs e)
        {
            label.Content = $"Player {current}";
            clicks = 0;
            current = Players.None;
            b = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    array[i, j].IsEnabled = true;
                    array[i, j].Content = default;
                }
            }
        }
        private void Click(object obj, EventArgs e)
        {
            label.Content = $"Player {current}";
            if (clicks == 0) BtnArr(array);
            Button btn = (Button)obj;
            if (current != Players.X)
            {
                current = Players.X;
                btn.Content = "X";
            }
            else
            {
                current = Players.O;
                btn.Content = "O";
            }
            btn.IsEnabled = false;
            clicks++;
            if (clicks > 8)
            {
                if (checkWin(array))
                {
                    MessageBox.Show($"{current} won!");
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            array[i, j].IsEnabled = false;
                        }
                    }
                }
                if (clicks == 25)
                {
                    MessageBox.Show($"Tie!");
                }
            }
        }
        private bool checkWin(Button[,] arr)
        {
            b = false;
            int count;
            for (int i = 0; i < 5; i++)
            {
                count = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (arr[i, j].Content == arr[i, j + 1].Content) count++;
                    else count = 0;
                    if (count == 4)
                    {
                        b = true;
                    }
                }
            }

            for (int j = 0; j < 5; j++)
            {
                count = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (arr[i, j].Content == arr[i + 1, j].Content) count++;
                    else count = 0;
                    if (count == 4) b = true;
                }
            }
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        if (arr[i, i].Content == arr[i + 1, i + 1].Content) count++;
                        else count = 0;
                        if (count == 4) b = true;
                    }
                }
            }
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 4; j > 0; j--)
                {
                    if (i + j == 4)
                    {
                        if (arr[i, j].Content == arr[i + 1, j - 1].Content) count++;
                        else count = 0;
                        if (count == 4) b = true;
                    }
                }
            }
            count = 0;
            return b;
        }
        public void BtnArr(Button[,] arr)
        {
            var bc = new BrushConverter();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arr[i, j] = (Button)GetElementInGridPosition(i, j);
                }
            }
        }
        private UIElement GetElementInGridPosition(int row, int column)
        {
            var element = grid2.Children.OfType<Button>().FirstOrDefault(x => Grid.GetColumn(x) == column && Grid.GetRow(x) == row);
            return element;
        }

    }
}