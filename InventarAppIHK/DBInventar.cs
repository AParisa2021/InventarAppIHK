using InventarAppIHK;
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
        /// Kategorie in die Datenbank hinzufügen und SQL injection verhindern
        /// </summary>
        /// <param name="category"></param>
        public static void AddCategory(Category category)
        {
            string insert = "INSERT INTO category(categoryname) VALUES (@categoryname)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(insert, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@categoryname", MySqlDbType.VarChar).Value = category.CategoryName;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kategorie hinzugefügt");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Kategorie nicht hinzugefügt!");
            }
            con.Close();
        }

        /// <summary>
        /// Ort, an dem sich das Inventar befindet in die Datenbank hinzufügen und SQL injection verhindern
        /// </summary>
        /// <param name="location"></param>
        public static void AddLocation(Location location)
        {
            string insert = "INSERT INTO location(floor, locationName) VALUES (@floor, @locationName)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(insert, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.Floor;
            cmd.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.LocationName;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ort hinzugefügt");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ort nicht hinzugefügt!");
            }
            con.Close();
        }

        /// <summary>
        /// Produkte in die Datenbank hinzufügen und SQL injection verhindern
        /// </summary>
        /// <param name="product"></param>
        public static void AddPrdoduct(Product product, Category category)
        {
            ComboBox comBox = new ComboBox();

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

        public static void FillCombobox(Category category)
        {
            //string select = "SELECT * FROM category";
            //MySqlConnection con = GetConnection();
            //MySqlCommand cmd = new MySqlCommand(select, con);

            //try
            //{
               
            //    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adap.Fill(ds);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    comboCategory.Items.Clear();
            //    comboCategory.DataSource =
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
       

      
        }

        //Unten kommt ein Join Befehl hin

        //public static void JoinAll(JoinAll join)
        //{
        //    string insert = "INSERT INTO joindaten(p_id, vorname, nachname, a_id, strasse, plz, land) VALUES (@p_id, @vorname, @nachname, @a_id, @strasse, @plz, @land)";
        //    MySqlConnection con = GetConnection();
        //    MySqlCommand cmd = new MySqlCommand(insert, con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.Add("@p_id", MySqlDbType.VarChar).Value = join.P_id;
        //    cmd.Parameters.Add("@vorname", MySqlDbType.VarChar).Value = join.Vorname;
        //    cmd.Parameters.Add("@nachname", MySqlDbType.VarChar).Value = join.Nachname;
        //    cmd.Parameters.Add("@a_id", MySqlDbType.VarChar).Value = join.A_id;
        //    cmd.Parameters.Add("@strasse", MySqlDbType.VarChar).Value = join.Strasse;
        //    cmd.Parameters.Add("@plz", MySqlDbType.VarChar).Value = join.Plz;
        //    cmd.Parameters.Add("@land", MySqlDbType.VarChar).Value = join.Land;
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("JoinAll added");
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show("Person not inserted");
        //    }
        //    con.Close();
        //}

        public static void ShowPerson(string query, DataGridView gvdPerson)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            con.Close();
        }

        public static void ShowAdress(string query, DataGridView gvdAdress)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            con.Close();
        }
    }
}
//https://www.youtube.com/watch?v=NrJiEjzyhYc



//ComboBox comBox = new ComboBox();
//ProductInsertForm dtOrder = new ProductInsertForm();
//string insert = "INSERT INTO product (productName, date, price)VALUES (@productName, @date, @price)";
//MySqlConnection con = GetConnection();
//MySqlCommand cmd = new MySqlCommand(insert, con);
//cmd.CommandType = CommandType.Text;
//cmd.Parameters.Add("@productName", MySqlDbType.VarChar).Value = product.ProductName;
////cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = product.Date;
//cmd.Parameters.Add(new MySqlParameter("@date", dtOrder));
//cmd.Parameters.Add("@price", MySqlDbType.VarChar).Value = product.Price;
////cmd.Parameters.Add("@category_id", MySqlDbType.VarChar).Value = comBox.SelectedValue;
////MySqlDataReader reader = cmd.ExecuteReader();
//cmd.ExecuteNonQuery();


//string insert2 = "INSERT INTO product(category_id) VALUES(@category_id)";
//MySqlCommand cmd2 = new MySqlCommand("SELECT category_id FROM category", con);
//int category_id = Convert.ToInt32(cmd2.ExecuteScalar());
//MySqlCommand cmd3 = new MySqlCommand(insert2, con);
//cmd.CommandType = CommandType.Text;
//cmd.Parameters.AddWithValue("@category_id", comBox);
//cmd2.ExecuteNonQuery();
//con.Close();

//try
//{
//    cmd.ExecuteNonQuery();
//    MessageBox.Show("Produkt hinzugefügt");
//}
//catch (MySqlException ex)
//{
//    MessageBox.Show("Produckt nicht hinzugefügt");
//}
//con.Close();