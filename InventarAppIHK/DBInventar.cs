using InventarAppIHK;
using InventarAppIHK.Import;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public class DBInventar
    {
        ///// <summary>
        ///// Herstellung der Verbindung mit der Datenbank MYSQL
        ///// </summary>
        ///// <returns></returns>
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Connection Error" + ex.Message);
            }
            return con;
        }

        ///// <summary>
        ///// Kategorie in die Datenbank hinzufügen und SQL injection verhindern
        ///// </summary>
        ///// <param name="category"></param>
        //public static void AddCategory(Category category)
        //{
        //    string insert = "INSERT INTO category(categoryName) VALUES (@categoryName)";
        //    MySqlConnection con = GetConnection();
        //    MySqlCommand cmd = new MySqlCommand(insert, con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = category.CategoryName;
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Kategorie hinzugefügt");
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show("Kategorie nicht hinzugefügt!");
        //    }
        //    con.Close();
        //}

        ///// <summary>
        ///// Ort, an dem sich das Inventar befindet in die Datenbank hinzufügen und SQL injection verhindern
        ///// </summary>
        ///// <param name="location"></param>
        //public static void AddLocation(Location location)
        //{
        //    string insert = "INSERT INTO location(floor, locationName) VALUES (@floor, @locationName)";
        //    MySqlConnection con = GetConnection();
        //    MySqlCommand cmd = new MySqlCommand(insert, con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.Floor;
        //    cmd.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.LocationName;

        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Ort hinzugefügt");
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show("Ort nicht hinzugefügt!");
        //    }
        //    con.Close();
        //}

        ///// <summary>
        ///// Produkte in die Datenbank hinzufügen und SQL injection verhindern
        ///// </summary>
        ///// <param name="product"></param>
        public static void AddPrdoduct(Product product)
        {
            ComboBox comBox = new ComboBox();
            ProductInsertForm cat = new ProductInsertForm();
            int category_id = DataImport.GetCategoryId(cat.comboCategory.Text);
            string insert = "INSERT INTO product (productName, date, price, category_id)VALUES (@productName, @date, @price, @category_id)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(insert, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@productName", MySqlDbType.VarChar).Value = product.ProductName;
            cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = product.Date;
            cmd.Parameters.Add("@price", MySqlDbType.VarChar).Value = product.Price;
            cmd.Parameters.Add("@category_id", MySqlDbType.VarChar).Value = comBox.SelectedValue;



            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produkt hinzugefügt");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Produckt nicht hinzugefügt");
            }
            con.Close();
        }

        //public static void FillCombobox(Category category)
        //{


        //}


        //    public static void ShowPerson(string query, DataGridView gvdPerson)
        //    {
        //        string sql = query;
        //        MySqlConnection con = GetConnection();
        //        MySqlCommand cmd = new MySqlCommand(sql, con);
        //        MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adap.Fill(dt);
        //        con.Close();
        //    }

        //    public static void ShowAdress(string query, DataGridView gvdAdress)
        //    {
        //        string sql = query;
        //        MySqlConnection con = GetConnection();
        //        MySqlCommand cmd = new MySqlCommand(sql, con);
        //        MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adap.Fill(dt);
        //        con.Close();
        //    }
    }
}
