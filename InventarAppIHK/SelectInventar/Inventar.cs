using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK.SelectInventar
{
    public class Inventar
    { 
        public double Product_id { get; set; }
        public int Category_id { get; set; }
        public int Location_id { get; set; }


        public Inventar(int product_id, int category_id, int location_id)
        {
            this.Product_id = product_id;
            this.Category_id = category_id;
            this.Location_id = location_id;
        }
    }
}

