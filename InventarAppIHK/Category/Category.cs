using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarAppIHK
{
    public class Category
    {
        private int CategoryID;   

        private string CategoryName;  

        public int GetCategoryId() => CategoryID;
        public string GetCategoryName() => CategoryName;

       
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
        public Category(int category_id, string categoryName)
        {
            CategoryID = category_id;
            CategoryName = categoryName;
        }
    }
}
