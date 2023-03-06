using InventarAppIHK.Import;
using InventarAppIHK.SelectInventar;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventarAppIHK
{
    public partial class ProductLocationForm : Form
    {
        public ProductLocationForm()
        {
            InitializeComponent();
            MyInitializeComponent();

        }

        public void MyInitializeComponent()
        {
            //DataImport.LoadFormProduct(dgvProduct);
            //CSVDataImport.LoadFormLocation(dgvLocation);
            TxtLocation();
            TxtSelectProd();
        }

       
            public void TxtSelectProd()
            {
            dgvProduct.Rows.Clear();
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
            //string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            //string query = "select product_id, productName, date, price, C.categoryName " +
            //   "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id where concat(P.product_id, P.productName, P.date, P.price, C.categoryName) like concat('%' , @search_product , '%')";
            string query = "select P.product_id, P.productName, P.date, P.price, C.categoryName " +
            "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id " +
            "WHERE UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) like concat('%' , @search_product , '%')";
            //string query = "SELECT P.product_id, P.productName, P.date, P.price, C.categoryName FROM from product AS P INNER JOIN category AS C ON P.category_id = C.category_id WHERE productName = ('" + txtSelectProduct.Text+"') GROUP BY productName";


            //string query = "SELECT product_id, productName, date, price, C.categoryName FROM from product AS P JOIN category AS C ON P.category_id=C.category_id WHERE product_id = ('" + txtSelectProduct.Text + "') GROUP BY product_id";

            conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@search_product", txtSelectProduct.Text);  //**************klappt nicht******
                MySqlDataReader dr = command.ExecuteReader();

                try
                {

                    int i = 0;
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
                        dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), Datetime(dr[2].ToString()), dr[3].ToString(), dr[4].ToString());
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
        /// Datum-Formatierung. Datum ohne Uhrzeit ausgeben
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private static string Datetime(string datetime)
        {
            //string timeFormat = datetime.Substring(datetime.Length - 7);
            string timeFormat = Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
            return timeFormat;
        }
        public void TxtLocation()
        {
            dgvLocation.Rows.Clear();
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
            //string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like '" + txtSelectLocation.Text + "'"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@search_location", txtSelectLocation.Text.ToUpper());
            MySqlDataReader dr = command.ExecuteReader();

            try
            {                        

                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
                        dgvLocation.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
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
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSelectLocation_TextChanged(object sender, EventArgs e)
        {
            TxtLocation();
        }

        private void dgvLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLNumber.Text = dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLName.Text = dgvLocation.Rows[e.RowIndex].Cells[2].Value.ToString();
            //CSVDataImport pickLocation = new CSVDataImport();
            //pickLocation.PickDgv(txtNumber.Text, dgvLocation, e);         **************Methode geht nicht*****
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPNumber.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDate.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString().Substring(0,10);
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtCategoryName.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtLName.Text == "")
            {
                MessageBox.Show("Bitte wählen Sie ein Standort!");
            }
            if (txtPName.Text == "")
            {
                MessageBox.Show("Bitte wählen Sie ein Produkt!");

            }
            else
            {
                int category_id = DataImport.GetCategoryId(txtCategoryName.Text);
                int location_id = DataImport.GetLocationId(txtLName.Text);
                int product_id = DataImport.GetProductId(txtPName.Text);
                Inventar inventar = new Inventar(product_id, category_id, location_id);
                DataImport.ChooseInventar(inventar);
                MessageBox.Show("Ihre Auswahl wurde eingetragen!");
            }
           
        }

        private void txtSelectProduct_TextChanged(object sender, EventArgs e)
        {
            TxtSelectProd();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataImport.ClearAllText(this);
        }
    }
}






//try
//{
//    string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

//    string query = "INSERT INTO totalinventar (productName, date, price, category_id, location_id) VALUES (@productName, @date, @price, @category_id, @location_id)";

//    int category_id = CSVDataImport.GetCategoryId(txtCategoryName.Text);
//    int location_id = CSVDataImport.GetLocationId(txtLName.Text);
//    MySqlConnection con = new MySqlConnection(sql);
//    MySqlCommand command = new MySqlCommand(query, con);    //In der Datenbank klasse erstellen und immer wieder darauf zugreifen
//    command.CommandType = CommandType.Text;
//    con.Open();

//    command.Parameters.AddWithValue("@productName", MySqlDbType.VarChar).Value = productName;
//    command.Parameters.AddWithValue("@date", MySqlDbType.VarChar).Value = date;
//    command.Parameters.AddWithValue("@price", MySqlDbType.Double).Value = price;
//    command.Parameters.AddWithValue("@category_id", MySqlDbType.Int32).Value = category_id;
//    command.Parameters.AddWithValue("@location_id", MySqlDbType.Int32).Value = location_id;

//    //command.Parameters.AddWithValue("@productName", txtPName.Text/*.Replace(',', '.')*/);
//    //command.Parameters.AddWithValue("@date", DateTime.Parse(txtDate.Text.Substring(0, 10)));
//    //command.Parameters.AddWithValue("@price", Convert.ToDouble(txtPrice.Text));
//    //command.Parameters.AddWithValue("@category_id", category_id);
//    //command.Parameters.AddWithValue("@location_id", location_id);

//    command.ExecuteNonQuery();
//    con.Close();

//}
//catch (MySqlException ex)
//{
//        MessageBox.Show(ex.Message);
//}          