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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Device.Location;
using GEO.Classes;
using GEO.Properties;


namespace GEO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            // настройка доступа к данным
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            // установка провайдера карт
            Map.MapProvider = BingMapProvider.Instance;

            // установка зума карты
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 15;
            // установка фокуса карты
            Map.Position = new PointLatLng(55.012823, 82.950359);

            // настройка взаимодействия с картой
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;

            PointLatLng point = new PointLatLng(55.016511, 82.946152);

            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = "Honda CR-V", // всплывающая подсказка
                    Source = new BitmapImage(new Uri("C:\\Users\\Platforma4\\Desktop\\geoapp\\Resourses\\car.png"))
                }
            };
            Map.Markers.Add(marker);
            // координаты точек замкнутой области (полигона)
            
            //List<PointLatLng> points = new PointLatLng[] {
            //     new PointLatLng(55.016351, 82.950650),
            //     new PointLatLng(55.017021, 82.951484),
            //     new PointLatLng(55.015795, 82.954526),
            //     new PointLatLng(55.015129, 82.953586) }.ToList();

            //GMapMarker marker = new GMapPolygon(points)
            //{
            //    Shape = new Path
            //    {
            //        Stroke = Brushes.Black, // стиль обводки
            //        Fill = Brushes.Aquamarine, // стиль заливки
            //        Opacity = 0.7 // прозрачность
            //    }
            //};

            Map.Markers.Add(marker);
        }
        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
