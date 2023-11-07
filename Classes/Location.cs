using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO.Classes
{
    internal class Location : MapObject
    {
        public PointLatLng point;
        public Location(string title, PointLatLng point) : base(title)
        {
            this.point = point;
        }

        public override double getDistance(PointLatLng point)
        {
            // Реализация расчета расстояния от точки до области
            // Может потребоваться сложная геометрия
            return 0.0;
        }

        public override PointLatLng getFocus()
        {
            // Расчет центральной точки области для фокусировки
            // Это может быть среднее значение координат точек
            return new PointLatLng(0, 0);
        }

        public override GMapMarker getMarker()
        {
            // Создание маркера для отображения области на карте
            // Маркер может представлять область как полигон или другую форму
            return null;
        }
    }
}
