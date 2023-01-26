using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Category
    {
        public string CategoryName { get; set; }

        public Category(string categoryName)
        { 
            this.CategoryName = categoryName; 
        }
    }
}
