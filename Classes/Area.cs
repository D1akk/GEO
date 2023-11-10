using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return 0.0;
        }

        public override PointLatLng getFocus()
        {
            return new PointLatLng(0, 0);
        }

        public override GMapMarker getMarker()
        {
            return null;
        }
    }
}
