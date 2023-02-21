using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public int Category_id { get; set; }

        public Product(string productName, DateTime date, double price, int category_id)
        {
            this.ProductName = productName;
            this.Date = date;
            this.Price = price;
            Category_id = category_id;
        }
        public Product(int product_id, string productName, DateTime date, double price, int category_id)
        {
            this.ProductID = product_id;
            this.ProductName = productName;
            this.Date = date;
            this.Price = price;
            Category_id = category_id;
        }
    }
}
