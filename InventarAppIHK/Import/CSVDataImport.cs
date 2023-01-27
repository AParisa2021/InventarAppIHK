using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK.Import
{
    class CSVDataImport
    {
        public static Mutex categoryAwait = new Mutex();
        public static void ImportData()
        {
            using (StreamReader reader = new StreamReader(@"C:\inventar.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    InsertCategory(values[3]);
                    InsertProduct(values[1].ToString(), values[2].ToString(), double.Parse(values[4].Replace(".", ",")), values[3].ToString());
                }
            }
        }

        public static void InsertProduct(string productName, string date, double price, string categoryName)
        {
            string query = "INSERT INTO product (productName, date, price, category_id) VALUES (@productName, @date, @price, @category_id)";
            int categoryId = GetCategoryId(categoryName);
         
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            try
            {
                MySqlConnection con = new MySqlConnection(sql);
                con.Open();
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.Add("productName", MySqlDbType.VarChar).Value = productName;
                    command.Parameters.Add("date", MySqlDbType.VarChar).Value = date;
                    command.Parameters.Add("price", MySqlDbType.Double).Value = price;
                    command.Parameters.Add("category_id", MySqlDbType.Int32).Value = categoryId;
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            catch(MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
         
        }

        public static void InsertCategory(string categoryName)
        {
  
            string query = "INSERT IGNORE INTO category (categoryname) VALUES (@categoryname)";
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            try
            {
                MySqlConnection con = new MySqlConnection(sql);
                con.Open();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("categoryname", MySqlDbType.VarChar).Value = categoryName;
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
    
        }

        public static int GetCategoryId(string categoryName)
        {
        
            string query = "SELECT category_id FROM category WHERE categoryname = @categoryname ";

            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            int categoryId = 0;
            try
            {
                MySqlConnection con = new MySqlConnection(sql);
                con.Open();
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.Add("@categoryname", MySqlDbType.VarChar).Value = categoryName;

                    categoryId = (int) command.ExecuteScalar();
                }
                con.Close();
                
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return categoryId;
        }
    }
}
