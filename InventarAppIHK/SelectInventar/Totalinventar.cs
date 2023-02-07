using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Totalinventar
    {
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public int Category_id { get; set; }
        public int Location_id { get; set; }


        public Totalinventar(string productName, DateTime date, double price, int category_id, int location_id)
        {
            this.ProductName = productName;
            this.Date = date;
            this.Price = price;
            this.Category_id = category_id;
            this.Location_id = location_id;
        }
    }
}
