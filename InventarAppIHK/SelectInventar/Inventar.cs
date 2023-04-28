using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace InventarAppIHK.SelectInventar
{
    public class Inventar
    {
        private int L_p_id;
        private int Product_id;
        private int Location_id;
        private string Seriennummer;

        public int GetL_p_id() => L_p_id;
        public int GetProduct_id() => Product_id;
        public int GetLocation_id() => Location_id;
        public string GetSeriennummer() => Seriennummer;

        public Inventar(int location_id, int product_id, string seriennummer)
        {
            this.Location_id = location_id;

            //this.Inventar_id = inventar_id;
            this.Product_id = product_id;
            //this.Category_id = category_id;
            this.Seriennummer = seriennummer;
        }
        public Inventar(int l_p_id, int location_id, int product_id, string seriennummer)
        {
            this.L_p_id = l_p_id;
            this.Product_id = product_id;
            //this.Category_id = category_id;
            this.Location_id = location_id;
            this.Seriennummer = seriennummer;
        }
    }
}

