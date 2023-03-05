using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;

namespace prac2
{
    /// <summary>
    /// Логика взаимодействия для GAwin.xaml
    /// </summary>
    public partial class GAwin : Window
    {
        static BrushConverter bc = new BrushConverter();
        static DispatcherTimer dT;
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static Polygon bestPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();
        static int population = 50;
        static int[][] pop = new int[population][];
        static double[] fitness = new double[population];
        static int[] bestOrder = new int[PointCount];
        static int[] current = new int[PointCount];
        static double bestdist = double.PositiveInfinity;
        static double rate;
        static int genscount = 1;
        static int maxg;
        static List<int[]> offspring = new List<int[]>();
        static List<double> dists = new List<double>();

        public GAwin()
        {
            dT = new DispatcherTimer();

            InitializeComponent();
            InitPoints();
            InitPolygon();
            InitbestPolygon();
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();

            for (int i = 0; i < PointCount; i++)
            {
                Point p = new Point();

                p.X = rnd.Next(Radius, (int)(0.62 * GA.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.87 * GA.Height - 3 * Radius));
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
        private void Exit(object sender, EventArgs e)
        {
            Close();
            MainWindow mw = new MainWindow();
            mw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mw.Show();
        }

        private void InitPolygon()
        {
            myPolygon.Stroke = (Brush)bc.ConvertFrom("#FF0C738C");
            myPolygon.StrokeThickness = 0.5;
        }
        private void InitbestPolygon()
        {
            bestPolygon.Stroke = (Brush)bc.ConvertFrom("#FF075F74");
            bestPolygon.StrokeThickness = 3;
        }

        private void PlotPoints()
        {
            TextBlock text;
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
            for (int i = 0; i < PointCount; i++)
            {
                text = new TextBlock()
                {
                    Text = $"{i + 1}",
                    Foreground = (Brush)bc.ConvertFrom("#FF0C738C"),
                    FontSize = 14,
                    FontFamily = new FontFamily("Yu Gothic UI Semibold")
                };
                Canvas.SetLeft(text, pC[i].X - Radius / 2 + 10);
                Canvas.SetTop(text, pC[i].Y - Radius / 2 + 4);
                MyCanvas.Children.Add(text);
            }
        }

        private void PlotWay()
        {
            //Fitness();
            PointCollection Points1 = new PointCollection();
            PointCollection Points2 = new PointCollection();
            nextGen();
            for (int i = 0; i < PointCount; i++)
                Points1.Add(pC[bestOrder[i]]);
            for (int i = 0; i < PointCount; i++)
                Points2.Add(pC[current[i]]);
            myPolygon.Points = Points2;
            bestPolygon.Points = Points1;
            MyCanvas.Children.Add(bestPolygon);
            MyCanvas.Children.Add(myPolygon);
        }

        private void Speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            dT.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt16(item.Content));
        }
        private void Points_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;

            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
            InitbestPolygon();
        }

        private void OneStep(object sender, EventArgs e)
        {
            if (genscount >= maxg)
            {
                MyCanvas.Children.Remove(myPolygon);
                dT.Stop();
            }
            MyCanvas.Children.Clear();
            PlotPoints();
            PlotWay();
        }

        private void CreatePopulation()
        {
            Random rnd = new Random();
            int[] way = Enumerable.Range(0, PointCount).ToArray();
            for (int i = 0; i < population; i++)
            {
                pop[i] = way.OrderBy(x => rnd.Next()).ToArray();
                offspring.Add(pop[i]);
                dists.Add(CalcRoute(pop[i]));
            }
        }
        private void Fitness()
        {
            double dist;
            for(int i = 0; i < population; i++)
            {
                dist = CalcRoute(pop[i]);
                cdist.Text = Math.Round(dist, 3).ToString();
                if (dist < bestdist)
                {
                    bestdist = dist;
                    bestWay.Content = Math.Round(bestdist, 3);
                    bestOrder = pop[i];
                }
               // fitness[i] = 1 / dist;
            }
        }
        private void Normalize()
        {
            double sum = 0;
            for(int i = 0; i < fitness.Length; i++)
            {
                sum += fitness[i];
            }
            for (int i = 0; i < fitness.Length; i++)
            {
                fitness[i] = fitness[i] / sum;
            }
        }
        private int[] tournament_selection(int k)
        {
            Random rnd = new Random();
            //int m = rnd.Next(population);
            //int n = rnd.Next(population);
            //while(m == n) n = rnd.Next(population);
            int best = 0;
            for (int i = 0; i < k; i++)
            {
                int j = rnd.Next(population);
                if (best == 0 || fitness[j] > fitness[best]) best = j;
            }
            return pop[best];
        }
        private int[] PickWay()
        {
            Random rnd = new Random();
            int index = 0;
            double rand = rnd.NextDouble();
            while (rand > 0)
            {
                rand -= fitness[index];
                index++;
            }
            index--;
            return pop[index];
        } 
        private void nextGen()
        {
            Random rnd = new Random();

            int[] indx = Enumerable.Range(0, population*2).ToArray();

            gens.Text = genscount.ToString();
            //int[][] newpop = new int[population][];
            for(int i = 0; i < population-1; i++)
            {
                int[] wayA, wayB;
                int p1 = rnd.Next(population);
                int p2 = rnd.Next(population);
                while (p1 == p2) p2 = rnd.Next(population);
                //if (rnd.NextDouble() > 0.5)
                //{
                //    wayA = tournament_selection(population / 2);
                //    wayB = tournament_selection(population / 2);
                //    while (wayA == wayB) wayB = tournament_selection(population / 2);
                //    offspring[i] = wayA;
                //    dists[i] = CalcRoute(wayA);
                //    current = wayA;
                //}
                //else
                //{
                //    Normalize();
                //    wayA = PickWay();
                //    wayB = PickWay();
                //    while (wayA == wayB) wayB = PickWay();
                //    offspring[i] = wayB;
                //    dists[i] = CalcRoute(wayB);
                //    current = wayB;
                //}
                (wayA, wayB) = Crossover(pop[p1], pop[p2]);
                offspring.Add(mutate(wayA));
                dists.Add(CalcRoute(wayA));
                cdist.Text = Math.Round(CalcRoute(wayA), 3).ToString();
                current = wayA;
                offspring.Add(mutate(wayB));
                dists.Add(CalcRoute(wayB));
                cdist.Text = Math.Round(CalcRoute(wayB), 3).ToString();
                current = wayB;
            }

            Array.Sort<int>(indx, (a, b) => dists[a].CompareTo(dists[b]));
            dists.Sort();
            for (int i = 0; i < population; i++)
            {
                pop[i] = offspring[indx[i]];
            }
            dists = dists.Take(population).ToList();
            offspring = pop.ToList();
            Fitness();
            genscount++;

        }
        private (int[], int[]) Crossover(int[] A, int[] B)
        {
            Random rnd = new Random();
            int point = rnd.Next(1, PointCount-1);
            //int point = PointCount / 2;
            List<int> wayBstart = B.Take(point).ToList();
            List<int> wayAstart = A.Take(point).ToList();
            List<int> appearedA = wayAstart;
            List<int> appearedB = wayBstart;
            foreach(int place in B)
            {
                if (appearedA.Contains(place)) continue;
                wayAstart.Add(place);
            }
            for (int place = 0; place < A.Length; place++)
            {
                if (appearedB.Contains(A[place])) continue;
                wayBstart.Add(A[place]);
            }
            A = wayAstart.ToArray();
            B = wayBstart.ToArray();
            return (A, B);
        }
        private int[] mutate(int[] way)
        {
            Random rnd = new Random();
            int point = rnd.Next(PointCount);
            int amount = rnd.Next(PointCount);
            int i1, i2, tmp;
            if (rnd.NextDouble() < 0.5)
            {
                if (point < amount)
                {
                    Array.Reverse(way, point, PointCount - amount);
                }
                else Array.Reverse(way, amount, PointCount - point);
            }
            else
            {
                double rand = rnd.NextDouble();
                if (rand < rate)
                {
                    i1 = rnd.Next(PointCount);
                    i2 = rnd.Next(PointCount);
                    tmp = way[i1];
                    way[i1] = way[i2];
                    way[i2] = tmp;
                }
            }
            return way;
        }
        private double CalcRoute(int[] way)
        {
            double route = 0;
            double dist;
            for(int n = 1; n < PointCount; n++)
            {
                dist = Math.Sqrt(Math.Pow(pC[way[n-1]].X - pC[way[n]].X, 2) + Math.Pow(pC[way[n-1]].Y - pC[way[n]].Y, 2));
                route += dist;
            }
            route += Math.Sqrt(Math.Pow(pC[way[PointCount-1]].X - pC[way[0]].X, 2) + Math.Pow(pC[way[PointCount-1]].Y - pC[way[0]].Y, 2));
            return route;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            dT.Stop();
            CBpoints.IsEnabled = true;
            CBspeed.IsEnabled = true;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            bestOrder = new int[PointCount];
            current = new int[PointCount];
            bestdist = double.PositiveInfinity;
            InitializeComponent();
            InitPoints();
            InitPolygon();
            InitbestPolygon();
            dists = new List<double>();
            offspring = new List<int[]>();
            CreatePopulation();
            CBpoints.IsEnabled = false;
            CBspeed.IsEnabled = false;
            genscount = 0;
            dT.Start();
        }

        private void pops_TextChanged(object sender, TextChangedEventArgs e)
        {
            population = int.Parse(pops.Text);
            pop = new int[population][];
            fitness = new double[population];
        }

        private void Mrate_TextChanged(object sender, TextChangedEventArgs e)
        {
            rate = double.Parse(Mrate.Text);
        }

        private void maxgens_TextChanged(object sender, TextChangedEventArgs e)
        {
           maxg = int.Parse(maxgens.Text);
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            CBpoints.IsEnabled = false;
            CBspeed.IsEnabled = false;
            dT.Start();
        }
    }
}