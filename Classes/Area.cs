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
using System.Windows.Media;
using System.Windows.Shapes;
using System.Device.Location;

namespace GEO.Classes
{
    [DisplayName("Область")]

    internal class Area: MapObject
    {

        public List<PointLatLng> points;

        public Area(string title, List <PointLatLng> points) : base(title)
        {
            this.points = points;
        }

        public override double getDistance(PointLatLng point)
        {
            List<double> distances = new List<double>();

            foreach (PointLatLng p in points)
            {
                GeoCoordinate c1 = new GeoCoordinate(p.Lat, p.Lng);
                GeoCoordinate c2 = new GeoCoordinate(point.Lat, point.Lng);
                distances.Add(c1.GetDistanceTo(c2));
            }

            return distances.Min();
        }

        public override PointLatLng getFocus()
        {
            double sumLat = 0;
            double sumLng = 0;

            foreach (PointLatLng p in points)
            {
                sumLat += p.Lat;
                sumLng += p.Lng;
            }

            double avgLat = sumLat / points.Count;
            double avgLng = sumLng / points.Count;

            return new PointLatLng(avgLat, avgLng);
        }

        public override GMapMarker getMarker()
        {
            GMapPolygon polygon = new GMapPolygon(points)
            {
                Shape = new Path
                {
                    Fill = new SolidColorBrush(Colors.White),
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 2,
                }
            };

            return polygon;
        }
    }
}
