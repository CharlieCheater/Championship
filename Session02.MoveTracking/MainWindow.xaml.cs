using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Session02.MoveTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, SkudInfo> _skudControls;
        private Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            _skudControls = new Dictionary<int, SkudInfo>
            {
                { 17, new(17, Skud17)},
                { 18, new(18, Skud18) },
                { 19, new(19, Skud19) },
                { 20, new(20, Skud20) },
                { 21, new(21, Skud21) },
                { 22, new(22, Skud22) },
                { 0, new(0, Skud0) },
                { 1, new(1, Skud1) },
            };
        }
        public void TryDraw()
        {

            for (int i = 0; i < 20; i++)
            {
                int skudId = random.Next(17, 20);
                var skud = _skudControls[skudId];
                var grid = skud.Border.Child as Grid;
                var x = random.NextDouble() * (grid.ActualWidth - 10);
                var y = random.NextDouble() * (grid.ActualHeight - 10);
                DrawPoint(x, y, skud);
            }
        }
        public void DrawPoint(double x, double y, SkudInfo skud)
        {
            Ellipse point = new Ellipse()
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Blue,
                Margin = new Thickness(x, y, 0, 0)
            };
            var grid = skud.Border.Child as Grid;
            grid.Children.Add(point);
            skud.HumanPositions.Add(new HumanPosition
            {
                Ellipse = point,
                Id = random.Next(1000, 9999)
            });
            point.MouseLeftButtonDown += Point_MouseLeftButtonDown;
            //Canvas.SetLeft(point, x);
            //Canvas.SetTop(point, y);
        }

        private void Point_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse selectedPoint = sender as Ellipse;
            if (selectedPoint == null)
                return;
            var grid = selectedPoint.Parent as Grid;
            var parent = grid.Parent as Border;
            var skud = _skudControls.FirstOrDefault(x => x.Value.Border == parent).Value;
            if (skud == null)
                return;
            skud.HumanPositions.RemoveAll(x => x.Ellipse == selectedPoint);
            grid.Children.Remove(selectedPoint);
            selectedPoint.MouseLeftButtonDown -= Point_MouseLeftButtonDown;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TryDraw();
        }
    }
}
