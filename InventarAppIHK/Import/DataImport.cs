using Google.Protobuf;
using InventarAppIHK.SelectInventar;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        //static string connstring = string.Format("datasource=localhost;port=3306;username=root;password=;database=inventar");
        //public MySqlConnection conn = new MySqlConnection(connstring);

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
            catch (MySqlException e)
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

        public static string formatCategoryId(string valueString)
        {
            if (valueString == "" || valueString == null) return "";

            return (valueString);
        }

        /// <summary>
        /// Wenn im Price-TextBox nicht eingetragen wird, wird 0 gespeichert
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public static double formatDouble(string valueString)
        {
            if (valueString == "" || valueString == null) return 0;

            return double.Parse(valueString);
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
        /// <param name = "categoryName" ></ param >
        /// < returns = "categoryId" ></ returns >
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

                    categoryId = (int)command.ExecuteScalar();
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

        public static void UpdateProductLocation(Inventar inventar)
        {
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
            string query = "UPDATE inventar SET product_id=@product_id, category_id=@category_id, location_id=@location_id WHERE inventar_id=@inventar_id";

            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add("@inventar_id", MySqlDbType.Int32).Value = inventar.Inventar_id;
                command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = inventar.Product_id;
                command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = inventar.Category_id;
                command.Parameters.Add("@location_id", MySqlDbType.Int32).Value = inventar.Location_id;
                command.ExecuteNonQuery();

                MessageBox.Show("Total update");

                MySqlDataReader dr = command.ExecuteReader();
                conn.Close();
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
                command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = product.ProductID;
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
        /// Füllt die ComboBox auf der ProductInsertForm mit den Kategorien aus der Tabelle Kategorie
        /// </summary>
        /// <param name="comboCategory"></param>
        public static void FillComboBox(ComboBox comboCategory)
        {
            {
                comboCategory.Items.Clear();
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
                string sql = "select categoryName from category";
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader dr = comm.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;
                        comboCategory.Items.Add(dr[0].ToString());
                    }
                }
                comm.Dispose();
                conn.Close();
                dr.Close();
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
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0,10), dr[3].ToString(), dr[4].ToString());
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
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }

        public void PickDgv(string textBox, DataGridView dgv, DataGridViewCellEventArgs e)
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

            if (price.Trim() != "")
            {
                if (price.Trim() != "") sql += " AND ";
                sql += "price <= @price";
            }

            return sql;
        }


        /// <summary>
        /// Clear Methode um alle TextBoxen vom Inhalt zu bereinigen   https://gist.github.com/rahuldass/028d2657ded7266c7893
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

        public static void CategoryEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {

            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            MySqlConnection con = new MySqlConnection(sql);

            string columnName = "";
            columnName = dgv.Columns[e.ColumnIndex].Name;
            con.Open();
            if (columnName == "edit")
            {
                CategoryInsertForm catInsert = new CategoryInsertForm();
                catInsert.txtId.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                catInsert.txtCategory.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                catInsert.ShowDialog();
            }
            else if (columnName == "delete")
            {

                MySqlCommand comm = new MySqlCommand("DELETE FROM category WHERE category_id= '" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

                comm.ExecuteNonQuery();
            }

            con.Close();
        }

        public static void ProductEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            int productID = DataImport.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());

            if (columnName == "edit")
            {
                ProductInsertForm productInsert = new ProductInsertForm();
                productInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                productInsert.dtOrder.Text = (dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                productInsert.txtName.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
                productInsert.txtPrice.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                productInsert.comboCategory.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
             
                productInsert.ShowDialog();
            }
          
            else if (columnName == "delete")
            {
                if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz inklusive Verknüpfungen löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MySqlConnection con = new MySqlConnection(sql);
                    try { 
                    con.Open();
                    int product_id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);
                    MySqlCommand commandProduct = new MySqlCommand("DELETE FROM product WHERE product_id=" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()), con);
                    commandProduct.ExecuteNonQuery();
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sie können diese Zeile nicht löschen, da sie in einer Elternspalte verwendet wird");
                        con.Close();

                    }
                }
            } 
        }

        public static void LocationEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            MySqlConnection con = new MySqlConnection(sql);

            string columnName = "";
            columnName = dgv.Columns[e.ColumnIndex].Name;
            con.Open();
            if (columnName == "Edit")
            {
                LocationInsertForm locInsert = new LocationInsertForm();
                locInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                locInsert.txtfloor.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                locInsert.txtRoomName.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();

                locInsert.ShowDialog();
            }
            else if (columnName == "Delete")
            {
                MySqlCommand comm = new MySqlCommand("DELETE FROM location WHERE location_id= '" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

                comm.ExecuteNonQuery();
            }
            con.Close();
        }

        /// <summary>
        /// Löschen und Bearbeiten des Datensatzes im DataGridView
        /// dafür greife ich auf die Methoden GetId der anderen Tabellen zu
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        public static void TotalEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            TotalForm catID = new TotalForm();
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            int productID = DataImport.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());

            if (columnName == "edit")
            {
                ProductLocationForm productLocationForm = new ProductLocationForm();
                //productLocationForm.txtLNumber.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                productLocationForm.txtInventarId.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                //productLocationForm.txtPNumber = dgv.Rows[e.RowIndex].Cells[].Value.ToString();
                productLocationForm.txtCatID.Text = (DataImport.GetCategoryId(dgv.Rows[e.RowIndex].Cells[4].Value.ToString())).ToString();
                productLocationForm.txtLocationID.Text = (DataImport.GetLocationId(dgv.Rows[e.RowIndex].Cells[6].Value.ToString())).ToString();
                productLocationForm.txtProductID.Text = (DataImport.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString())).ToString();

                productLocationForm.txtLNumber.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                productLocationForm.txtLName.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                productLocationForm.txtDate.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                productLocationForm.txtPName.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                productLocationForm.txtPrice.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                productLocationForm.txtCategoryName.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                //CategoryInsertForm catInsert = new CategoryInsertForm();
                //catInsert.Name = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productLocationForm.ShowDialog();
            }
            //else if(columnName == "delete")
            //{
            //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
            else if (columnName == "delete")
            {
                if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MySqlConnection con = new MySqlConnection(sql);

                    con.Open();
                    int product_id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);
                    //MySqlCommand commandProduct = new MySqlCommand("DELETE FROM product WHERE product_id=" + Convert.ToInt32(dgvTotal.Rows[e.RowIndex].Cells[0].Value.ToString()), con);
                    MySqlCommand commandProduct = new MySqlCommand("DELETE FROM inventar WHERE inventar_id=" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()), con);

                    commandProduct.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static void TxtLocation(DataGridView dgv, string txtSelectLocation)
        {
            dgv.Rows.Clear();
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
            //string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like '" + txtSelectLocation.Text + "'"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@search_location", txtSelectLocation.ToUpper());
            MySqlDataReader dr = command.ExecuteReader();

            try
            {

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
                conn.Close();
                dr.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }

        /// <summary>
        /// lädt die Daten aus den Tabellen product und category mit einem INNER JOIN in ProductLocationForm
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="txtSelectProduct"></param>
        public static void TxtSelectProd(DataGridView dgv, string txtSelectProduct)
        {
            dgv.Rows.Clear();
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
            
            string query = "select P.product_id, P.productName, P.date, P.price, C.categoryName " +
            "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id " +
            "WHERE UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) like concat('%' , @search_product , '%')";

            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@search_product", txtSelectProduct);  //**************klappt nicht******
            MySqlDataReader dr = command.ExecuteReader();

            try
            {

                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), Datetime(dr[2].ToString()), dr[3].ToString(), dr[4].ToString());
                    }
                }

                command.Dispose();
                conn.Close();
                dr.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }
        private static string Datetime(string datetime)
        {
            //string timeFormat = datetime.Substring(datetime.Length - 7);
            string timeFormat = Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
            return timeFormat;
        }


        /// <summary>
        /// Filterung Preis von, Preis bis und Filterung nach Produktnamen
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="txtFrom"></param>
        /// <param name="txtTo"></param>
        /// <param name="txtSelect"></param>
        /// <param name="lblTotal"></param>
        public static void SelectPriceProduct(DataGridView dgv, string txtFrom, string txtTo, string txtSelect, Label lblTotal)
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            double total = 0;
            dgv.Rows.Clear();
            MySqlConnection conn = new MySqlConnection(sql);
            string query = "SELECT inventar_id, P.productName, P.date, P.price, C.categoryName, L.floor, L.locationName " +
                "from inventar AS I " +
                "INNER JOIN product AS P " +
                 "ON I.product_id=P.product_id " +
                "INNER JOIN category AS C " +
                "ON I.category_id=C.category_id " +
                "INNER JOIN location AS L " +
                "ON I.location_id=L.location_id ";

            if (txtFrom.Trim() != "" || txtTo.Trim() != "" || txtSelect.ToUpper().Trim() != "") query += " WHERE ";

            if (txtFrom.Trim().Length > 0 && txtTo.Trim().Length > 0)
            {
                query += " price BETWEEN @priceFrom AND @priceTo";
            }
            else if (txtFrom.Trim().Length > 0)
            {
                query += "price >= @priceFrom";
            }
            else if (txtTo.Trim().Length > 0)
            {
                query += " price <= @priceTo";
            }
            if (txtSelect.Trim() != "")
            {
                if (txtFrom.Trim().Length > 0 || txtTo.Trim().Length > 0)
                    query += " AND ";

                query += " concat(I.inventar_id, P.productName, P.date, P.price, C.categoryName, L.floor, L.locationName ) like concat('%' , @search_totalinventar , '%')";
            }
            using (MySqlCommand comm = new MySqlCommand(query, conn))
            {
                {
                    try
                    {
                        conn.Open();
                        if (txtSelect.Trim() != "")
                            comm.Parameters.AddWithValue("@search_totalinventar", txtSelect.ToUpper());

                        if (txtFrom.Trim() != "")
                            comm.Parameters.AddWithValue("@priceFrom", double.Parse(txtFrom));

                        if (txtTo.Trim() != "")
                            comm.Parameters.AddWithValue("@priceTo", double.Parse(txtTo));

                        MySqlDataReader dr = comm.ExecuteReader();
                        int i = 0;
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), double.Parse(dr[3].ToString()), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

                                total += double.Parse(dr[3].ToString());
                            }
                        }
                        comm.Dispose();
                        conn.Close();
                        dr.Close();

                        lblTotal.Text = total.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

    }
}
