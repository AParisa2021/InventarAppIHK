using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Location
    {
        private int LocationID;
        private string Floor;
        private string LocationName;

        public int GetLocationID() => LocationID;
        public string GetFloor() => Floor;

        public string GetLocationName() => LocationName;
        public Location(string floor, string locationName)
        {
            this.Floor = floor;
            this.LocationName = locationName;
        }
        public Location(int location_id, string floor, string locationName)
        {
            this.LocationID = location_id;
            this.Floor = floor;
            this.LocationName = locationName;
        }
        public Location()
        {
        }
    }
}
