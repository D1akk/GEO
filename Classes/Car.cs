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

namespace GEO.Classes
{
    [DisplayName("Машина")]
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

        //public override PointLatLng getFocus()
        //{
        //    return new PointLatLng(0, 0);
        //}

        public override GMapMarker getMarker()
        {
            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = title, // всплывающая подсказка
                    Source = new BitmapImage(new Uri("C:\\Users\\Platforma4\\Desktop\\geoapp\\Resourses\\car.png")),
                    Margin = new System.Windows.Thickness(-16,-16,0,0),
                }
            };
            return marker;
        }
    }
}
