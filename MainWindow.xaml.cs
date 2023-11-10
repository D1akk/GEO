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
using System.Security;
using System.ComponentModel;

namespace GEO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool add_radio_checked = false;
        private bool search_radio_checked = false;
        private bool title;
        private PointLatLng point = new PointLatLng();
        private List<object> object_list = new List<object>();
        private List<Type> object_type = new List<Type>
        {
            typeof(Car),
            typeof(Human),
            typeof(Classes.Location),
            typeof(Area),
        };
       

        
        public MainWindow()
        {
            InitializeComponent();
            
            Type_ComboBox.ItemsSource = object_type;
            Type_ComboBox.DisplayMemberPath = "Name";
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
        
        }

        public void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {  
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
            this.point = point;

            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Ellipse
                {
                    Width = 2,
                    Height = 2,
                    Stroke = Brushes.Red,
                    Fill = Brushes.Red,
                    StrokeThickness = 1
                }
            };
            Map.Markers.Add(marker);
        }
       

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton selected_radio = sender as RadioButton;    

            if (selected_radio == Add_Radio)
            {
                add_radio_checked = true;
                search_radio_checked = false;
            }
            else if (selected_radio == Search_Radio)
            {
                add_radio_checked = false;
                search_radio_checked = true;
            }        
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            if (add_radio_checked)
            {
                Car car = new Car(Add_Input.Text, point);
                object_list.Add(car);
                GMapMarker car_marker = car.getMarker();
                Map.Markers.Add(car_marker);
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Map.Markers.Clear();
        }


      
    }
}
