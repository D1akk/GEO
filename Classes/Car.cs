using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.ComponentModel;
using System.Device.Location;

namespace GEO.Classes
{
    [DisplayName("Машина")]
    public class Car : MapObject
    {
        public PointLatLng point;

        public Car(string title, PointLatLng point) : base(title)
        {
            this.point = point;
        }

        public override double getDistance(PointLatLng point)
        {
            PointLatLng p1 = this.point;
            PointLatLng p2 = point;

            GeoCoordinate c1 = new GeoCoordinate(p1.Lat,p1.Lng);
            GeoCoordinate c2 = new GeoCoordinate(p2.Lat,p2.Lng);

            double distance = c1.GetDistanceTo(c2);
            return distance;
        }

        public override PointLatLng getFocus()
        {
            return point;
        }

        public override GMapMarker getMarker()
        {
            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = title, // всплывающая подсказка
                    Source = new BitmapImage(new Uri("C:\\Users\\George\\Desktop\\GEO\\Resourses\\car.png")),
                    Margin = new System.Windows.Thickness(-16,-16,0,0),
                }
            };
            return marker;
        }
    }
}
