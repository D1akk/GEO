using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO.Classes
{
    internal class Car : MapObject
    {
        public PointLatLng point;

        public Car(string title, PointLatLng point) : base(title)
        {
            this.point = point;
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
