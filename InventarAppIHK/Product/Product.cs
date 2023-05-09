using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    //public class Product
    //{
    //    private int ProductID;
    //    private string ProductName;
    //    private DateTime Date;
    //    private double Price;
    //    private int Category_id;

    //    public int GetProductID() => ProductID;
    //    public string GetProductName() => ProductName;
    //    public DateTime GetDate() => Date;
    //    public double GetPrice() => Price;
    //    public int GetCategory_id() => Category_id;

    // 
    //    public Product(string productName, DateTime date, double price, int category_id)
    //    {
    //        this.ProductName = productName;
    //        this.Date = date;
    //        this.Price = price;
    //        Category_id = category_id;
    //    }
    //    public Product(int product_id, string productName, DateTime date, double price, int category_id)
    //    {
    //        this.ProductID = product_id;
    //        this.ProductName = productName;
    //        this.Date = date;
    //        this.Price = price;
    //        Category_id = category_id;
    //    }
    //}
    public class Product
    {
        private int ProductID;
        private string ProductName;
        private DateTime Date;
        private double Price;
        private string SerialNumber;
        private string CategoryName;
        private string Floor;
        private string LocationName;
        private int Category_id;
       
        public int GetCategory_id() => Category_id;
        public int GetProductID() => ProductID;
        public void SetProductID(int productID) => ProductID = productID;

        public string GetProductName() => ProductName;
        public void SetProductName(string productName) => ProductName = productName;

        public DateTime GetDate() => Date;
        public void SetDate(DateTime date) => Date = date;

        public double GetPrice() => Price;
        public void SetPrice(double price) => Price = price;

        public string GetSerialNumber() => SerialNumber;
        public void SetSerialNumber(string serialNumber) => SerialNumber = serialNumber;

        public string GetCategoryName() => CategoryName;
        public void SetCategoryName(string categoryName) => CategoryName = categoryName;

        public string GetFloor() => Floor;
        public void SetFloor(string floor) => Floor = floor;

        public string GetLocationName() => LocationName;
        public void SetLocationName(string locationName) => LocationName = locationName;

        public Product() { }

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
        public Product(string productName, DateTime date, double price, string serialNumber, string categoryName, string floor, string locationName)
        {
            this.ProductName = productName;
            this.Date = date;
            this.Price = price;
            this.SerialNumber = serialNumber;
            this.CategoryName = categoryName;
            this.Floor = floor;
            this.LocationName = locationName;
        }

        public Product(int productID, string productName, DateTime date, double price, string serialNumber, string categoryName, string floor, string locationName)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Date = date;
            this.Price = price;
            this.SerialNumber = serialNumber;
            this.CategoryName = categoryName;
            this.Floor = floor;
            this.LocationName = locationName;
        }
    }

}
