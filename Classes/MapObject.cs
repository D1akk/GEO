using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO.Classes
{
    internal class MapObject
    {
        public string title;
        public DateTime creationDate;

     
        public MapObject(string Title)
        {
            this.title = Title;
            this.creationDate = DateTime.Now;
        }

        public string getTitle()
        {
            return title;
        }

        public DateTime getCreationDate()
        {
            return creationDate;
        }

        public virtual double getDistance(PointLatLng point)
        {
            return 0;
        }
        
        public virtual PointLatLng getFocus()
        {
            return new PointLatLng(0, 0);
        }
        
        public virtual GMapMarker getMarker()
        {
            return null;
        }
        
    }
}
