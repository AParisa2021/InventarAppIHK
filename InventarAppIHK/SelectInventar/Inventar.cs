using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK.SelectInventar
{
    public class Inventar
    {
        public int L_p_id { get; set; }
        public int Product_id { get; set; }
        //public int Category_id { get; set; }
        public int Location_id { get; set; }
        public string Seriennummer { get; set; }

        public Inventar(int location_id, int product_id, string seriennummer)
        {
            this.Location_id = location_id;

            //this.Inventar_id = inventar_id;
            this.Product_id = product_id;
            //this.Category_id = category_id;
            this.Seriennummer = seriennummer;
        }
        public Inventar(int l_p_id,int product_id, int location_id, string seriennummer)
        {
            this.L_p_id = l_p_id;
            this.Product_id = product_id;
            //this.Category_id = category_id;
            this.Location_id = location_id;
            this.Seriennummer = seriennummer;
        }
    }
}

