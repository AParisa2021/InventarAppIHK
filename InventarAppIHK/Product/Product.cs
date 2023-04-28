using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Product
    {
        private int ProductID;
        private string ProductName;
        private DateTime Date;
        private double Price;
        private int Category_id;

        public int GetProductID() => ProductID;
        public string GetProductName() => ProductName;
        public DateTime GetDate() => Date;
        public double GetPrice() => Price;
        public int GetCategory_id() => Category_id;

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
