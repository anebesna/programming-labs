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
using System.Linq;

namespace lab
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private static Players current = default;
        private static int clicks;
        private bool b = false;
        public Button[,] array = new Button[5, 5];
        public Window2()
        {
            InitializeComponent();
        }
        private void Exit2_Click(object obj, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
        private enum Players
        {
            O,
            X,
            None
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
            if(clicks > 8)
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
            return b;
        }
        public void BtnArr(Button[,] arr)
        {
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
            var element = Grid.Children.OfType<Button>().FirstOrDefault(x => Grid.GetColumn(x) == column && Grid.GetRow(x) == row);
            return element;
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
    }
}
