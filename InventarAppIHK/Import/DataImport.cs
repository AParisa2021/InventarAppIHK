﻿using InventarAppIHK.SelectInventar;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InventarAppIHK.Import
{
    class DataImport
    {
        public static Mutex categoryAwait = new Mutex();

        /// <summary>
        /// Herstellung der Verbindung mit der Datenbank MYSQL
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Importiert Datensätze aus einer CSV Datei. Mit InsertCategory Methode speichert er den Kategorienamen (Index 3 in der CSV Datei)
        /// in die Datenbank-Tabelle category ein
        /// Mit der Methode InsertProduct speichert er den gesamten Datensatz jeweils in der richtigen Datenbank-Tabellenreihenfolge ein
        /// </summary>
        /// <param name="reader"></param>
        public static void ImportData(StreamReader reader)
        {
            using (reader)
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
        /// <summary>
        /// Trägt Produktdaten in die Datenbank-Tablle product ein. Mit der Methode GetCategoryId holt er aus der Tabelle category den entsprechenden
        /// categoryName der zugehörigen category_id
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="categoryName"></param>
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
        /// <summary>
        /// Überladung? Ich versuche mit dieser Methode auf die Klasse Product zuzugreifen und Date-Format zu ändern
        /// Jedoch klappt dan die Methode in der CSV Implementierung nicht mehr wegen der Parameteranzahl*****************
        /// </summary>
        /// <param name="product"></param>
        public static void InsertProduct(Product product)
        {
            string query = "INSERT INTO product (productName, date, price, category_id) VALUES (@productName, @date, @price, @category_id)";
            //int categoryId = GetCategoryId(product.CategoryName);

            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            try
            {
                MySqlConnection con = new MySqlConnection(sql);
                con.Open();
                using (MySqlCommand command = new MySqlCommand(query, con))
                {                
                    command.Parameters.Add("@productName", MySqlDbType.VarChar).Value = product.ProductName;
                    command.Parameters.Add("@date", MySqlDbType.VarChar).Value = product.Date.ToString("yyyy-MM-dd");
                    command.Parameters.Add("@price", MySqlDbType.Double).Value = product.Price;
                    command.Parameters.AddWithValue("@category_id", MySqlDbType.Int32).Value = product.Category_id;

                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Die Zeilen mit ungültigen Daten, die einen Error verursachen werden ignoriert und die Zeilen mit gültigen Daten
        /// werden in die Tabelle category gespeichert
        /// </summary>
        /// <param name="categoryName"></param>
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
        /// <summary>
        /// holt die jeweilige category_id des jeweiligen categoryName aus der category Tabelle und gibt die categoryId als Rückgabewert zurück
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns="categoryId"></returns>
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

        /// <summary>
        /// holt die jeweilige product_id des jeweiligen locationName aus der category Tabelle und gibt die LocationId als Rückgabewert zurück
        /// </summary>
        /// <param name="productName"></param>
        /// <returns="productid"></returns>
        public static int GetProductId(string productName)
        {

            string query = "SELECT product_id FROM product WHERE productName = @productName";

            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            int productid = 0;
            try
            {
                MySqlConnection con = new MySqlConnection(sql);
                con.Open();
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.Add("@productName", MySqlDbType.VarChar).Value = productName;

                    productid = (int)command.ExecuteScalar();
                }
                con.Close();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return productid;
        }

        /// <summary>
        /// holt die jeweilige location_id des jeweiligen locationName aus der category Tabelle und gibt die LocationId als Rückgabewert zurück
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="locationName"></param>
        /// <returns="locationId"></returns>
        public static int GetLocationId(string locationName)
        {

            string query = "SELECT location_id FROM location WHERE locationName = @locationName";

            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            int locationId = 0;
            try
            {
                MySqlConnection con = new MySqlConnection(sql);
                con.Open();
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = locationName;

                    locationId = (int)command.ExecuteScalar();
                }
                con.Close();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return locationId;
        }

        /// <summary>
        /// Mit der TextBox einen Datensatz im DataGridView filtern
        /// </summary>
        /// <param name="search_location"></param>
        public static void FilterTxtLocation(Location locationName)
        {   //klappt Nicht*****************************************************
            DataGridView dgv = new DataGridView();
            dgv.Rows.Clear();
            TextBox txt = new TextBox();
            //string query = "select * from category";
            string query = "select location_id, floor, locationName from location where concat(@location_id, @floor, @locationName) like '%' || @search_location || '%'"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter

            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                command.Parameters.AddWithValue("@search_location", MySqlDbType.VarChar).Value = locationName; 
                                
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    }
                }
         
                command.Dispose();
                con.Close();
                dr.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }

        /// <summary>
        /// Den Namen der Kategorie verändert oder erweitern
        /// </summary>
        /// <param name="categoryName"></param>
        public static void UpdateCategory(Category category)
        {
            string query = "UPDATE category SET categoryName=@categoryName WHERE category_id=@category_id";
            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = category.CategoryID;
                command.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = category.CategoryName;                

                command.ExecuteNonQuery();
                MessageBox.Show("Kategorie update");

                MySqlDataReader da = command.ExecuteReader();

                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Verändert oder Erweitert mit Update die Etage den Namen des Ortes
        /// </summary>
        /// <param name="location"></param>
        public static void UpdateLocation(Location location)
        {
            string query = "UPDATE location SET floor=@floor, locationName=@locationName WHERE location_id=@location_id";
            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("@location_id", MySqlDbType.VarChar).Value = location.LocationID;
                command.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.Floor;
                command.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.LocationName;
                command.ExecuteNonQuery();
                MessageBox.Show("Ort update");

                MySqlDataReader da = command.ExecuteReader();

                //if ((int)command.ExecuteScalar() == 1)
                //{
                //    MessageBox.Show(location.Floor.ToString());
                //    MessageBox.Show(location.LocationName.ToString());
                //}
                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Ort nicht hinzugefügt!");
            }
        }

        /// <summary>
        /// Änderung eines Datensatzes der Tabelle product mit Update-Query
        /// </summary>
        /// <param name="product"></param>
        public static void UpdateProduct(Product product)
        {
            string query = "UPDATE product SET productName =@productName, date =@date, price=@price, " +
                "category_id=@category_id WHERE product_id=@product_id";
             
            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("@product_id", MySqlDbType.VarChar).Value = product.ProductID;
                command.Parameters.Add("@productName", MySqlDbType.VarChar).Value = product.ProductName;
                command.Parameters.Add("@date", MySqlDbType.VarChar).Value = product.Date.ToString("yyyy-MM-dd");
                command.Parameters.Add("@price", MySqlDbType.Double).Value = product.Price;
                command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = product.Category_id;

                command.ExecuteNonQuery();
                MessageBox.Show("Produkt update");

                MySqlDataReader da = command.ExecuteReader();
         
                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Damit bei einem leeren Datumseintrag das Programm nicht abstürzt, stattdessen in der DataGridView Tabelle ein - als Zeichen für leer bzw. Datum nicht vorhanden ausgibt
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private string NullProblemDatabase(string datetime)
        {
            if (datetime == "") return "-";
            else return Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        public static void LoadFormCategory(DataGridView dgv)              //update datagridview mit einem Button updaten
        {
            dgv.Rows.Clear();
            string query = "select * from category";
            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString());
                    }
                }
                command.Dispose();
                con.Close();
                dr.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }
        /// <summary>
        /// Lädt User aus der Datenbank Tabelle product & gibt die jeweils in den einzelnen Zeilen des dgvProduct aus,
        /// indem mit Hilfe eines Joins zwei Tabellen ausgegeben werden
        /// </summary>
        public static void LoadFormProduct(DataGridView dgv)              //update datagridview mit einem Button updaten
        {
            dgv.Rows.Clear();
            string query = "select product_id, productName, date, price, C.categoryName " +
                "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id";

            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                    }
                }
                command.Dispose();
                con.Close();
                dr.Close();
            }
            catch(MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }

        /// <summary>
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        public static void LoadFormLocation(DataGridView dgv)              //update datagridview mit einem Button updaten
        {
            dgv.Rows.Clear();
            string query = "select * from location";
            try
            {
                MySqlConnection con = GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    }
                }
                command.Dispose();
                con.Close();
                dr.Close();
            }
            catch(MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }

        public void PickDgv(string textBox, DataGridView dgv ,DataGridViewCellEventArgs e)
        {           
            int i = 0;
            textBox = dgv.Rows[e.RowIndex].Cells[i].Value.ToString();
        }
        /// <summary>
        /// MySQL Tabelle totalinventar aus Tabelle location und produkt Inventar wählen und in totalinventar einfügen
        /// </summary>
        /// <param name="product-id</param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="category_id"></param>
        /// <param name="location_id"></param>
        public static void ChooseInventar(Inventar inventar)
        {
            try
            {
                string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
                string query = "INSERT INTO inventar(product_id, category_id, location_id) VALUES (@product_id, @category_id, @location_id)";

                //category_id = CSVDataImport.GetCategoryId(txtCategoryName.Text);
                //location_id = CSVDataImport.GetLocationId(txtLName.Text);
                MySqlConnection con = new MySqlConnection(sql);
                MySqlCommand command = new MySqlCommand(query, con);    //In der Datenbank klasse erstellen und immer wieder darauf zugreifen
                command.CommandType = CommandType.Text;
                con.Open();

                command.Parameters.AddWithValue("@product_id", MySqlDbType.Int32).Value = inventar.Product_id;
                command.Parameters.AddWithValue("@category_id", MySqlDbType.Int32).Value = inventar.Category_id;
                command.Parameters.AddWithValue("@location_id", MySqlDbType.Int32).Value = inventar.Location_id;
                              
                command.ExecuteNonQuery();
                con.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// Baut SQL Query zusammen. Hier befinden sich der string für die Query DIe Suchfunktion Preis soll gleichzetitig zu der Suchfunktion für die strings erfolgen
        /// </summary>
        /// <returns></returns>
        public static string AufbauSuchMethode(/*string textBoxSearch,*//* string price*/)
        {
            string price = "";
            string sql = "SELECT total_id, productName, date, price, C.categoryName, L.floor, L.locationName " +
                "from totalinventar AS T " +
                "INNER JOIN category AS C " +
                "ON T.category_id=C.category_id " +
                "INNER JOIN location AS L " +
                "ON T.location_id=L.location_id";


            if (/*textBoxSearch.Trim() != "" ||*/ price.Trim() != "") sql += " WHERE ";
            //if (textBoxSearch.Trim() != "")
            //{
            //    sql += "UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) " +
            //           "LIKE '%" + textBoxSearch.ToUpper() + "%'";
            //}
            if (price.Trim() != "")
            {
                if (price.Trim() != "") sql += " AND ";
                sql += "price <= @price";
            }

            return sql;
        }

        //public static void EditDeleteCategory(DataGridView dgvCategory, DataGridViewCellEventArgs e/*, Category category*//*, string columnName*/)
        //{
        //    string columnName = "";
        //    columnName = dgvCategory.Columns[e.ColumnIndex].Name;
        //    //if(columnName == "edit")
        //    //{
        //    //    CategoryInsertForm catInsert = new CategoryInsertForm();
        //    //    catInsert.Name = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    //}
        //    //else if(columnName == "delete")
        //    //{
        //    //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();

        //    string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'";
        //    MySqlConnection con = GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        command.ExecuteNonQuery();
        //        con.Close();
        //    //}
        //}
        //public static void EditDeleteProduct(DataGridView dgvProduct, DataGridViewCellEventArgs e/*, Category category*//*, string columnName*/)
        //{
        //    string columnName = "";
        //    columnName = dgvProduct.Columns[e.ColumnIndex].Name;
        //    //if(columnName == "edit")
        //    //{
        //    //    CategoryInsertForm catInsert = new CategoryInsertForm();
        //    //    catInsert.Name = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    //}
        //    //else if(columnName == "delete")
        //    //{
        //    //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();

        //    string query = "DELETE FROM category where categoryName LIKE" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'";
        //    MySqlConnection con = GetConnection();
        //    MySqlCommand command = new MySqlCommand(query, con);
        //    command.ExecuteNonQuery();
        //    con.Close();
        //    //}
        //}

        /// <summary>
        /// Clear Methode um alle TextBoxen vom Inhalt zu bereinigen
        /// </summary>
        /// <param name="con"></param>
        public static void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }

    }
}