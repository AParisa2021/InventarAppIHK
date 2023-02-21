using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public Category(string categoryName)
        { 
            this.CategoryName = categoryName; 
        }
        public Category(int category_id, string categoryName)
        {
            this.CategoryID = category_id;
            this.CategoryName = categoryName;
        }
    }
}
