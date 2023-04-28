﻿using InventarAppIHK.SelectInventar;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public class InventarMethods
    {
        public static int Getl_p_Id(string seriennummer)
        {
            string query = "SELECT l_p_id FROM l_p WHERE seriennummer=@seriennummer ";

            int l_p_id = 0;
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);

                command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = seriennummer;

                l_p_id = (int)command.ExecuteScalar();

                con.Close();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return l_p_id;


            //MySqlConnection con = Utility.GetConnection();
            //string query = "SELECT l_p_id FROM l_p WHERE seriennummer=@seriennummer";
            ////string query = "SELECT l_p_id FROM l_p WHERE seriennummer=@seriennummer";

            //int l_p_id = 0;
            //try
            //{
            //    using (MySqlCommand command = new MySqlCommand(query, con))
            //    {
            //        command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = seriennummer;

            //        l_p_id = (int)command.ExecuteScalar();
            //    }

            //}
            //catch (MySqlException e)
            //{
            //    MessageBox.Show(e.Message);
            //}
            //con.Close();
            //return l_p_id;
        }

        /// <summary>
        /// Änderungen in der Zwischentabelle inventar können mit dieser Methode vorgenommen werden
        /// </summary>
        /// <param name="inventar"></param>
        public static void UpdateProductLocation(Inventar inventar)     //!!!!!Ändern
        {
            string query = "UPDATE l_p SET location_id=@location_id, product_id=@product_id, seriennummer=@seriennummer WHERE l_p_Id=@l_p_Id";

            try
            {
                MySqlConnection conn = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add("@l_p_Id", MySqlDbType.Int32).Value = inventar.GetL_p_id();
                command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = inventar.GetProduct_id();
                command.Parameters.Add("@location_id", MySqlDbType.Int32).Value = inventar.GetLocation_id();
                command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = inventar.GetSeriennummer();

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
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        public static void LoadFormTotal(DataGridView dgv, DataGridViewCellEventArgs e)              //update datagridview mit einem Button updaten
        {
            dgv.Rows.Clear();
            string query = "SELECT P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName " +
             "from l_p AS LP " +
             "INNER JOIN product AS P " +
             "ON LP.product_id=P.product_id " +            
             "INNER JOIN location AS L " +
             "ON LP.location_id=L.location_id " +
             "INNER JOIN category AS C " +
             "ON P.category_id=C.category_id ";

            //string query = "SELECT P.product_id, P.productName, P.date, P.price, C.categoryName, L.floor, L.locationName " +
            //    "from inventarfk AS I " +
            //    "INNER JOIN product AS P " +
            //    "ON I.product_id=P.product_id " +
            //    "INNER JOIN location AS L " +
            //    "ON I.location_id=L.location_id " +
            //    "INNER JOIN category AS C " +
            //    "ON P.category_id=C.category_id ";

            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), double.Parse(dr[3].ToString()), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                    }
                }
                command.Dispose();
                con.Close();
                dr.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + "Error");
            }
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
            MySqlConnection con = Utility.GetConnection();


            string query = "INSERT INTO l_p(location_id, product_id, seriennummer) VALUES (@location_id, @product_id, @seriennummer)";

            //category_id = CSVDataImport.GetCategoryId(txtCategoryName.Text);
            //location_id = CSVDataImport.GetLocationId(txtLName.Text);
            MySqlCommand command = new MySqlCommand(query, con);    //In der Datenbank klasse erstellen und immer wieder darauf zugreifen
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@location_id", MySqlDbType.Int32).Value = inventar.GetLocation_id();
            command.Parameters.AddWithValue("@product_id", MySqlDbType.Int32).Value = inventar.GetProduct_id();
            command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = inventar.GetSeriennummer();


            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Ihre Auswahl wurde eingetragen!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Produkt ist bereits reserviert!");
                con.Close();
            }
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
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            int productID = ProdMethods.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
            if (columnName == "edit")
            {
                ProductLocationForm productLocationForm = new ProductLocationForm();
                productLocationForm.btnSave.Enabled = false;
                productLocationForm.dgvProduct.Enabled = false; //Produkt kann höchsten die Location ändern. nicht seinen Namen. deswegen dgvProduct.Enabled = false
                productLocationForm.txtCatID.Text = (CatMethods.GetCategoryId(dgv.Rows[e.RowIndex].Cells[5].Value.ToString())).ToString();
                productLocationForm.txtLocationID.Text = (LocMethods.GetLocationId(dgv.Rows[e.RowIndex].Cells[7].Value.ToString())).ToString();
                productLocationForm.txtProductID.Text = (ProdMethods.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString())).ToString();
                productLocationForm.txtPNumber.Text = (ProdMethods.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString())).ToString();
                productLocationForm.txtLPId.Text = (InventarMethods.Getl_p_Id(dgv.Rows[e.RowIndex].Cells[4].Value.ToString())).ToString();

                productLocationForm.txtLNumber.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                productLocationForm.txtLName.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
                productLocationForm.txtDate.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                productLocationForm.txtPName.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                productLocationForm.txtPrice.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                productLocationForm.txtSerial.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();

                productLocationForm.txtCategoryName.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();

                productLocationForm.ShowDialog();
            }
            else if (columnName == "delete")
            {
                if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MySqlConnection con = Utility.GetConnection();
                    //WHERE seriennummer da diese Eindeutig ist. Ansonsten löscht er alle Produkte mit dem gleichen Namen
                    string select_serial = dgv.CurrentRow.Cells["serial"].Value.ToString();
                    string deleteRow = "DELETE  FROM l_p WHERE seriennummer= '"+ select_serial + "' ";
                    MySqlCommand commandProduct = new MySqlCommand(deleteRow, con);

                    //in der Inventartabelle nach product-id suchen da uique
                    //Tabelle mit unique product_id und location_id
                    commandProduct.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        ///// <summary>
        ///// lädt die Daten aus den Tabellen product und category mit einem INNER JOIN in ProductLocationForm
        ///// </summary>
        ///// <param name="dgv"></param>
        ///// <param name="txtSelectProduct"></param>
        //public static void TxtSelectProd(DataGridView dgv, string txtSelectProduct)
        //{
        //    dgv.Rows.Clear();

        //    dgv.Rows.Clear();
        //    MySqlConnection con = Utility.GetConnection();

        //    string query = "select P.product_id, P.productName, P.date, P.price, C.categoryName " +
        //    "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id " +
        //    "WHERE UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) like concat('%' , @search_product , '%')" +
        //    " AND product_id NOT IN (select product_id from inventarfk AS I WHERE P.product_id=I.product_id) ";

        //    MySqlCommand command = new MySqlCommand(query, con);
        //    command.Parameters.AddWithValue("@search_product", txtSelectProduct);
        //    MySqlDataReader dr = command.ExecuteReader();

        //    try
        //    {

        //        int i = 0;
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), Datetime(dr[2].ToString()), dr[3].ToString(), dr[4].ToString());
        //            }
        //        }

        //        command.Dispose();
        //        con.Close();
        //        dr.Close();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message + "Error");
        //    }
        //}


        /// <summary>
        /// Mit select holt er die Orte aus der Tabelle location und gibt sie im DataGridViewLocation aus
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="txtSelectLocation"></param>
        public static void TxtLocation(DataGridView dgv, string txtSelectLocation)
        {
            dgv.Rows.Clear();
            MySqlConnection con = Utility.GetConnection();
            string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            MySqlCommand command = new MySqlCommand(query, con);
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
                con.Close();
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
        public static void TxtSelectProd(DataGridView dgv, string txtSelectProduct)     //!!!!Ändern
        {
            dgv.Rows.Clear();

            dgv.Rows.Clear();
            MySqlConnection con = Utility.GetConnection();

            //string query = "select P.product_id, P.productName, P.date, P.price, C.categoryName " +
            //"from product AS P INNER JOIN category AS C ON P.category_id=C.category_id " +
            //"WHERE UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) like concat('%' , @search_product , '%')" +
            //" AND product_id NOT IN (select product_id from inventarfk AS I WHERE P.product_id=I.product_id) ";


            string query = "select P.product_id, P.productName, P.date, P.price, C.categoryName " +
          "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id " +
          "WHERE UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) like concat('%' , @search_product , '%')";

            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@search_product", txtSelectProduct);
            MySqlDataReader dr = command.ExecuteReader();

            try
            {

                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), Utility.Datetime(dr[2].ToString()), dr[3].ToString(), dr[4].ToString());
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

        public static void Suml_p(string txtProductName, Label lblSum)
        {           
            string query = "select Count(l_p_id) " +
                         "from l_p AS LP " +
                         "INNER JOIN product AS P ON LP.product_id=P.product_id " +
                         "where concat (LP.l_p_id, P.productName) like concat ('%' , @P.productName , '%')";

            //string query = "select Count(l_p_id), P.productName " +
            //             "from l_p AS LP " +
            //             "INNER JOIN product AS P ON LP.product_id=P.product_id " +
            //             "where  @P.productName=P.productName ";     //kein Absturz
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                //MySqlDataReader dr = command.ExecuteReader();
                //lblSum.Text = Convert.ToString(command);

                using (var cmd = new MySqlCommand(query, con))
                {
                    if (txtProductName != "")
                    {
                        cmd.Parameters.AddWithValue("@P.productName", txtProductName.ToUpper().Trim());
                        object result = cmd.ExecuteScalar();
                        if(result != null)
                        {
                            lblSum.Text = result.ToString();
                        }
                 
                    }
                    else
                    {
                        lblSum.Text = "0";
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error");
            }
        }
    }
}
