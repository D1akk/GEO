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
using System.Reflection;
using GMap.NET.ObjectModel;

namespace GEO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool add_radio_checked = false;
        private bool search_radio_checked = false;
        private List<PointLatLng> points = new List<PointLatLng>();
        private List<MapObject> object_list = new List<MapObject>();
        private List<string> object_types;
        private ObservableCollection<string> search_results = new ObservableCollection<string>();
        private ObservableCollection<string> close_objects = new ObservableCollection<string>();


        public MainWindow()
        {
            InitializeComponent();
            object_types = GetObjectTypes();
            Type_ComboBox.ItemsSource = object_types;
            Type_ComboBox.SelectedIndex = 0;

        }

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            Map.MapProvider = BingMapProvider.Instance;
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 15;
            Map.Position = new PointLatLng(55.012823, 82.950359);
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
        }

        public void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
            points.Add(point);

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
                string selectedObjectType = Type_ComboBox.SelectedItem as string;

                if (!string.IsNullOrEmpty(selectedObjectType))
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var objectType = assembly.GetTypes()
                        .FirstOrDefault(t => t.IsSubclassOf(typeof(MapObject)) &&
                                             (t.Name == selectedObjectType || t.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName == selectedObjectType));

                    if (objectType != null)
                    {
                        GMapMarker marker;
                        if (objectType == typeof(Classes.Route) && points.Count >= 2)
                        {
                            Classes.Route obj = new Classes.Route(Add_Input.Text, new List<PointLatLng>(points));
                            object_list.Add(obj);
                            marker = obj.getMarker();
                            Map.Markers.Add(marker);
                            points.Clear();
                        }
                        if (objectType == typeof(Area) && points.Count >= 3)
                        {
                            Area obj = new Area(Add_Input.Text, new List<PointLatLng>(points));
                            object_list.Add(obj);
                            marker = obj.getMarker();
                            Map.Markers.Add(marker);
                            points.Clear();
                        }
                        if (objectType == typeof(Car))
                        {
                            for (int i = 0; i < points.Count; i++)
                            {
                                Car obj = new Car(Add_Input.Text, points[i]);
                                object_list.Add(obj);
                                marker = obj.getMarker();
                                Map.Markers.Add(marker);

                            }
                            points.Clear();
                        }
                        if (objectType == typeof(Classes.Location))
                        {
                            for (int i = 0; i < points.Count; i++)
                            {
                                Classes.Location obj = new Classes.Location(Add_Input.Text, points[i]);
                                object_list.Add(obj);
                                marker = obj.getMarker();
                                Map.Markers.Add(marker);
                            }
                            points.Clear();
                        }
                        if (objectType == typeof(Human))
                        {
                            for (int i = 0; i < points.Count; i++)
                            {
                                Human obj = new Human(Add_Input.Text, points[i]);
                                object_list.Add(obj);
                                marker = obj.getMarker();
                                Map.Markers.Add(marker);
                            }
                            points.Clear();
                        }
                        Add_Input.Text = null;
                    }
                }
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Map.Markers.Clear();
            points.Clear();
            object_list.Clear();
        }

        private List<string> GetObjectTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var objectTypes = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MapObject)))
                .ToList();

            var displayNames = new List<string>();

            foreach (var objectType in objectTypes)
            {
                var displayNameAttribute = objectType.GetCustomAttribute<DisplayNameAttribute>();
                displayNames.Add(displayNameAttribute.DisplayName);
            }
            return displayNames;
        }

        private void Search_Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (search_radio_checked)
            {
                foreach (var obj in object_list)
                {
                    if (((MapObject)obj).getTitle() == Search_Input.Text)
                    {
                        search_results.Add(((MapObject)obj).getTitle());
                    }
                }
                Search_Result.ItemsSource = search_results;
                search_results = null;
            }
        }

 
        private void Search_Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedObjectTitle = Search_Result.SelectedItem as string;

            if (selectedObjectTitle != null)
            {
                var selectedObject = object_list.FirstOrDefault(obj => obj.getTitle() == selectedObjectTitle);

                if (selectedObject != null)
                {
                    double distance;
                    close_objects.Clear(); 

                    foreach (var obj_tmp in object_list)
                    {
                        if (obj_tmp != selectedObject)
                        {
                            distance = selectedObject.getDistance(obj_tmp.getFocus());
                            close_objects.Add($"{obj_tmp.getTitle()}: {distance} метров");
                        }
                    }
                    Close_Objects.ItemsSource = close_objects;
                }
            }
        }

        private void Search_Clear_Click(object sender, RoutedEventArgs e)
        {
            Search_Result.ItemsSource = null;
            Close_Objects.ItemsSource = null;
        }
    }
}
