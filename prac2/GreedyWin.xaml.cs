using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace prac2
{
    /// <summary>
    /// Interaction logic for GreedyWin.xaml
    /// </summary>
    public partial class GreedyWin : Window
    {
        static BrushConverter bc = new BrushConverter();
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();
        public GreedyWin()
        {
            dT = new DispatcherTimer();
            InitializeComponent();
            InitPoints();
            InitPolygon();

            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }
        private void Exit(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mw.Show();
        }
        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.67 * greedy.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.95 * greedy.Height - 3 * Radius));
                pC.Add(p);
            }

            for (int i = 0; i < PointCount; i++)
            {
                Ellipse el = new Ellipse();

                el.StrokeThickness = 1;
                el.Height = el.Width = Radius;
                el.Stroke = (Brush)bc.ConvertFrom("#FF0C738C");
                el.Fill = (Brush)bc.ConvertFrom("#FF21252B");
                EllipseArray.Add(el);
            }
        }
        private void InitPolygon()
        {
            myPolygon.Stroke = (Brush)bc.ConvertFrom("#FF0C738C");
            myPolygon.StrokeThickness = 2;
        }

        private void PlotPoints()
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);

                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }
        private void PlotWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();

            for (int i = 0; i < BestWayIndex.Length; i++)
                Points.Add(pC[BestWayIndex[i]]);

            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }

        private void Speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            InitPoints();
            InitPolygon();
            dT.Start();

        }

        private void Points_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
        }

        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();
            PlotPoints();
            PlotWay(GetBestWay());
        }

        private int[] GetBestWay()
        {
            int[] way = new int[PointCount+1];
            for (int i = 0; i < PointCount+1; i++)
                way[i] = 0;
            PointCollection temppc = new PointCollection(pC);
            Point point = temppc[0];
            temppc.RemoveAt(0);
            double sum = 0;
            TextBlock text;
            for (int j = 0; j < PointCount - 1; j++)
            {
                int index = pC.IndexOf(temppc[0]);
                int k = 0;
                double min = GetDist(point, temppc[0]);
                
                for (int i = 1; i < temppc.Count; i++)
                {
                    double temp = GetDist(point, temppc[i]);
                    if (temp < min)
                    {
                        min = temp;
                        index = pC.IndexOf(temppc[i]);
                        k = i;
                    }
                }
                point = temppc[k];
                temppc.RemoveAt(k);
                way[j + 1] = index;
                sum += min;
            }
            best.Content = Math.Round(sum, 3);
            for(int i = 0; i<PointCount; i++)
            {
                text = new TextBlock()
                {
                    Text = $"{way[i] + 1}",
                    Foreground = (Brush)bc.ConvertFrom("#FF0C738C"),
                    FontSize = 14,
                    FontFamily = new FontFamily("Yu Gothic UI Semibold")
                };
                Canvas.SetLeft(text, pC[way[i]].X - Radius / 2 + 10);
                Canvas.SetTop(text, pC[way[i]].Y - Radius / 2 + 4);
                MyCanvas.Children.Add(text);
            }
            return way;
        }
        private double GetDist(Point i, Point j) => Math.Sqrt((Math.Pow(j.X - i.X, 2) + Math.Pow(j.Y - i.Y, 2)));
    }
}
