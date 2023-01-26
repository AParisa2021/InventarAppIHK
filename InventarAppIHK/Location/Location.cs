using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Location
    {
        public string Floor { get; set; }

        public string LocationName { get; set; }

        public Location(string floor, string locationName)
        {
            this.Floor = floor;
            this.LocationName = locationName;
        }
    }
}
