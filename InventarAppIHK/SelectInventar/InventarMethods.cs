using Google.Protobuf.WellKnownTypes;
using InventarAppIHK.SelectInventar;
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
        const string EditColumnName = "edit";
        const string DeleteColumnName = "delete";
        const string SerialColumnName = "SerienNr.";
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
        //public static void UpdateProductLocation(Inventar inventar)     //!!!!!Ändern
        //{
        //    string query = "UPDATE l_p SET location_id=@location_id, product_id=@product_id, seriennummer=@seriennummer WHERE l_p_Id=@l_p_Id";

        //    try
        //    {
        //        MySqlConnection conn = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, conn);
        //        command.Parameters.Add("@l_p_Id", MySqlDbType.Int32).Value = inventar.GetL_p_id();
        //        command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = inventar.GetProduct_id();
        //        command.Parameters.Add("@location_id", MySqlDbType.Int32).Value = inventar.GetLocation_id();
        //        command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = inventar.GetSeriennummer();

        //        command.ExecuteNonQuery();

        //        MessageBox.Show("Total update");

        //        MySqlDataReader dr = command.ExecuteReader();
        //        conn.Close();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}





        //******************Beginn UpdateProductLocation


        //Es gibt hier bereits eine Methode, die ein Produkt anhand seiner Seriennummer aktualisiert,
        //indem sie die zugehörige Produkt-Lagerort-Beziehung aktualisiert. Hier ist eine Möglichkeit,
        //diese Methode in zwei Methoden aufzuteilen.

        //Die Methode UpdateProductLocation ruft nun die neue private Methode UpdateLagerortProdukt
        //auf, um den eigentlichen Update-Vorgang durchzuführen. Dadurch wird der Code übersichtlicher,
        //da sich jeder Methode nur auf eine bestimmte Aufgabe konzentriert.
        public static void UpdateProductLocation(Inventar inventar)
        {
            try
            {
                MySqlConnection conn = Utility.GetConnection();

                UpdateLagerortProdukt(inventar, conn);

                MessageBox.Show("Total update");

                conn.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void UpdateLagerortProdukt(Inventar inventar, MySqlConnection conn)
        {
            string query = "UPDATE l_p SET location_id=@location_id, product_id=@product_id, seriennummer=@seriennummer WHERE l_p_Id=@l_p_Id";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.Add("@l_p_Id", MySqlDbType.Int32).Value = inventar.GetL_p_id();
            command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = inventar.GetProduct_id();
            command.Parameters.Add("@location_id", MySqlDbType.Int32).Value = inventar.GetLocation_id();
            command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = inventar.GetSeriennummer();

            command.ExecuteNonQuery();

            MySqlDataReader dr = command.ExecuteReader();
        }






        /// <summary>
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        //public static void LoadFormTotal(DataGridView dgv, DataGridViewCellEventArgs e)              //update datagridview mit einem Button updaten
        //{
        //    dgv.Rows.Clear();
        //    string query = "SELECT P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName " +
        //     "from l_p AS LP " +
        //     "INNER JOIN product AS P " +
        //     "ON LP.product_id=P.product_id " +
        //     "INNER JOIN location AS L " +
        //     "ON LP.location_id=L.location_id " +
        //     "INNER JOIN category AS C " +
        //     "ON P.category_id=C.category_id ";


        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        MySqlDataReader dr = command.ExecuteReader();
        //        int i = 0;
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                i++;        //wird noch nicht verwendet
        //                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), Utility.PriceFormat(dr[3].ToString()), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
        //            }
        //        }
        //        command.Dispose();
        //        con.Close();
        //        dr.Close();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message + "Error");
        //    }
        //}



        ////********************Beginn LoadFormTotal


        ////    GetTotalData() Methode, um die Datensätze aus der Datenbank abzurufen.
        ////UpdateDataGridView(DataGridView dgv, DataTable dt) Methode, um das DataGridView mit den abgerufenen Datensätzen zu aktualisieren.
        //public static DataTable GetTotalData()
        //{
        //    DataTable dt = new DataTable();

        //    string query = "SELECT P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName " +
        //     "from l_p AS LP " +
        //     "INNER JOIN product AS P " +
        //     "ON LP.product_id=P.product_id " +
        //     "INNER JOIN location AS L " +
        //     "ON LP.location_id=L.location_id " +
        //     "INNER JOIN category AS C " +
        //     "ON P.category_id=C.category_id ";

        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dt);
        //        con.Close();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message + "Error");
        //    }

        //    return dt;
        //}

        //public static void UpdateDataGridView(DataGridView dgv, DataTable dt)
        //{
        //    dgv.Rows.Clear();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        dgv.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString().Substring(0, 10), Utility.PriceFormat(row[3].ToString()), row[4].ToString(), row[5].ToString(), row[6].ToString());
        //    }
        //}




        /// <summary>
        /// MySQL Tabelle totalinventar aus Tabelle location und produkt Inventar wählen und in totalinventar einfügen
        /// </summary>
        /// <param name="product-id</param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="category_id"></param>
        /// <param name="location_id"></param>
        //public static void ChooseInventar(Inventar inventar)
        //{
        //    MySqlConnection con = Utility.GetConnection();


        //    string query = "INSERT INTO l_p(location_id, product_id, seriennummer) VALUES (@location_id, @product_id, @seriennummer)";

        //    //category_id = CSVDataImport.GetCategoryId(txtCategoryName.Text);
        //    //location_id = CSVDataImport.GetLocationId(txtLName.Text);
        //    MySqlCommand command = new MySqlCommand(query, con);    //In der Datenbank klasse erstellen und immer wieder darauf zugreifen
        //    command.CommandType = CommandType.Text;

        //    command.Parameters.AddWithValue("@location_id", MySqlDbType.Int32).Value = inventar.GetLocation_id();
        //    command.Parameters.AddWithValue("@product_id", MySqlDbType.Int32).Value = inventar.GetProduct_id();
        //    command.Parameters.Add("@seriennummer", MySqlDbType.VarChar).Value = inventar.GetSeriennummer();


        //    try
        //    {
        //        command.ExecuteNonQuery();
        //        MessageBox.Show("Ihre Auswahl wurde eingetragen!");
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message,"Produkt ist bereits reserviert!");
        //        con.Close();
        //    }
        //}


        //******************Beginn Inventar

        public static void ChooseInventar(Inventar inventar)
        {
            MySqlConnection con = Utility.GetConnection();       

            string query = "INSERT INTO l_p(location_id, product_id, seriennummer) VALUES (@location_id, @product_id, @seriennummer)";

            MySqlCommand command = new MySqlCommand(query, con);
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
                MessageBox.Show(ex.Message, "Produkt ist bereits reserviert!");
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
                    string deleteRow = "DELETE  FROM l_p WHERE seriennummer= '" + select_serial + "' ";
                    MySqlCommand commandProduct = new MySqlCommand(deleteRow, con);

                    //in der Inventartabelle nach product-id suchen da uique
                    //Tabelle mit unique product_id und location_id
                    commandProduct.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        //*****************Beginn Total Edi Delete

        //public static void TotalEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        //{
      

        //    string columnName = dgv.Columns[e.ColumnIndex].Name;
        //    int productID = ProdMethods.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());

        //    switch (columnName)
        //    {
        //        case EditColumnName:
        //            EditProductLocation(dgv, e);
        //            break;

        //        case DeleteColumnName:
        //            DeleteProduct(dgv, e);
        //            break;
        //    }
        //}

        private static void EditProductLocation(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            ProductLocationForm productLocationForm = new ProductLocationForm();
            productLocationForm.btnSave.Enabled = false;
            productLocationForm.dgvProduct.Enabled = false;


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

        //Methode TotalEditDelete ohne das Löschen von Produkten durch Seriennummern
        private static void DeleteProduct(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            const string deleteQuery = "DELETE FROM l_p WHERE seriennummer = @serialNumber";
            var serialNumber = dgv.Rows[e.RowIndex].Cells[4].Value.ToString(); ;
            if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (MySqlConnection con = Utility.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(deleteQuery, con);
                    command.Parameters.AddWithValue("@serialNumber", serialNumber);
                    command.ExecuteNonQuery();
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
        //public static void TxtLocation(DataGridView dgv, string txtSelectLocation)
        //{
        //    dgv.Rows.Clear();
        //    MySqlConnection con = Utility.GetConnection();
        //    string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
        //    MySqlCommand command = new MySqlCommand(query, con);
        //    command.Parameters.AddWithValue("@search_location", txtSelectLocation.ToUpper());
        //    MySqlDataReader dr = command.ExecuteReader();

        //    try
        //    {

        //        int i = 0;
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
        //                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
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



        //***********Beginn TxtLocation

        //Methode zur Verbindungsherstellung mit der Datenbank
        private static MySqlConnection ConnectToDatabase()
        {
            MySqlConnection con = Utility.GetConnection();
            return con;
        }

        //Methode zum Ausführen der Datenbankabfrage
        private static MySqlCommand ExecuteQuery(string query, MySqlConnection con)
        {
            MySqlCommand command = new MySqlCommand(query, con);
            return command;
        }


        //Methode zum Erstellen des SQL-Parameters für die Suchanfrage
        private static MySqlParameter CreateSearchParameter(string searchText)
        {
            MySqlParameter searchParameter = new MySqlParameter("@search_location", searchText.ToUpper());
            return searchParameter;
        }


        //Methode zum Ausführen des DataReader und Füllen des DataGridView
        private static void FillDataGridView(DataGridView dgv, MySqlDataReader dr)
        {
            try
            {
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    }
                }

                dr.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }


        //Neue Methode, welche die oben genannten Methoden aufruft und das DataGridView füllt
        public static void TxtLocation(DataGridView dgv, string txtSelectLocation)
        {
            dgv.Rows.Clear();
            MySqlConnection con = ConnectToDatabase();

            string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')";
            MySqlCommand command = ExecuteQuery(query, con);

            MySqlParameter searchParameter = CreateSearchParameter(txtSelectLocation);
            command.Parameters.Add(searchParameter);

            MySqlDataReader dr = command.ExecuteReader();
            FillDataGridView(dgv, dr);

            command.Dispose();
            con.Close();
        }




        /// <summary>
        /// lädt die Daten aus den Tabellen product und category mit einem INNER JOIN in ProductLocationForm
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="txtSelectProduct"></param>
        public static void TxtSelectProd(DataGridView dgv, string txtSelectProduct)     //!!!!Ändern
        {
            dgv.Rows.Clear();
            MySqlConnection con = Utility.GetConnection();

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
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), Utility.Datetime(dr[2].ToString()), Utility.PriceFormat(dr[3].ToString()), dr[4].ToString());
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

        // Methode zum Erstellen des SQL-Parameters für die Suchanfrage
        private static MySqlParameter CreateSearchParameter(string parameterName, string searchText)
        {
            MySqlParameter searchParameter = new MySqlParameter(parameterName, searchText.ToUpper());
            return searchParameter;
        }

        // Methode zum Ausführen der Datenbankabfrage und Rückgabe des DataReaders
        private static MySqlDataReader ExecuteQuery(string query, MySqlConnection con, params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(query, con);
            foreach (MySqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command.ExecuteReader();
        }

        // Methode zum Füllen des DataGridView aus dem DataReader
        //private static void FillDataGridView(DataGridView dgv, MySqlDataReader dr)
        //{
        //    try
        //    {
        //        int i = 0;
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                i++;
        //                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
        //            }
        //        }

        //        dr.Close();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message + "Error");
        //    }
        //}

        //// Methode, die von TxtSelectProd und TxtLocation aufgerufen wird
        //private static void SearchAndFill(DataGridView dgv, string query, string searchParameterName, string searchText)
        //{
        //    dgv.Rows.Clear();
        //    MySqlConnection con = Utility.GetConnection();
        //    MySqlParameter searchParameter = CreateSearchParameter(searchParameterName, searchText);
        //    MySqlDataReader dr = ExecuteQuery(query, con, searchParameter);
        //    FillDataGridView(dgv, dr);
        //    dr.Close();
        //    con.Close();
        //}

        // Methode, um Produkte nach Name oder Kategorie zu suchen
        //public static void TxtSelectProd(DataGridView dgv, string txtSelectProduct)
        //{
        //    string query = "select P.product_id, P.productName, P.date, P.price, C.categoryName " +
        //                  "from product AS P INNER JOIN category AS C ON P.category_id=C.category_id " +
        //                  "WHERE UPPER(CONCAT(P.product_id, P.productName, P.date, P.price, C.categoryName)) like concat('%' , @search_product , '%')";
        //    SearchAndFill(dgv, query, "@search_product", txtSelectProduct.ToUpper());
        //}

        //// Methode, um Standorte nach Name oder Stockwerk zu suchen
        //public static void TxtLocation(DataGridView dgv, string txtSelectLocation)
        //{
        //    string query = "select location_id, floor, locationName from location where concat(location_id, floor, locationName) like concat('%' , @search_location , '%')";
        //    SearchAndFill(dgv, query, "@search_location", txtSelectLocation.ToUpper());
        //}






        public static void Suml_p(string txtProductName, Label lblSum)
        {
            string query = "select Count(l_p_id) " +
                         "from l_p AS LP " +
                         "INNER JOIN product AS P ON LP.product_id=P.product_id " +
                         "where concat (LP.l_p_id, P.productName) like concat ('%' , @P.productName , '%')";

            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);

                using (var cmd = new MySqlCommand(query, con))
                {
                    if (txtProductName != "")
                    {
                        cmd.Parameters.AddWithValue("@P.productName", txtProductName.ToUpper().Trim());
                        object result = cmd.ExecuteScalar();
                        if (result != null)
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




        //****************Beginn Sum l_p


        //Erstellen Sie eine Methode, um die SQL-Abfrage zu erstellen

        //private static string BuildQuery(string txtProductName)
        //{
        //    return "select Count(l_p_id) " +
        //           "from l_p AS LP " +
        //           "INNER JOIN product AS P ON LP.product_id=P.product_id " +
        //           "where concat (LP.l_p_id, P.productName) like concat ('%' , @P.productName , '%')";
        //}


        ////Erstellen Sie eine Methode, um die Verbindung zur Datenbank herzustellen und die Abfrage auszuführen
        //private static object ExecuteQueryProduct(string txtProductName, string query)
        //{
        //    using (var con = Utility.GetConnection())
        //    using (var cmd = new MySqlCommand(query, con))
        //    {
        //        if (txtProductName != "")
        //        {
        //            cmd.Parameters.AddWithValue("@P.productName", txtProductName.ToUpper().Trim());
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //        var result = cmd.ExecuteScalar();
        //        return result ?? 0;
        //    }
        //}


        //private static object ExecuteQuery(string txtProductName, string query)
        //{
        //    using (var con = Utility.GetConnection())
        //    using (var cmd = new MySqlCommand(query, con))
        //    {
        //        if (txtProductName != "")
        //        {
        //            cmd.Parameters.AddWithValue("@P.productName", txtProductName.ToUpper().Trim());
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //        var result = cmd.ExecuteScalar();
        //        return result ?? 0;
        //    }
        //}


        ////Durch diese Aufteilung der ursprünglichen Methode in mehrere Teile erhöht
        ////sich die Lesbarkeit des Codes und es wird einfacher, Wartungsarbeiten
        ////durchzuführen oder Änderungen an den Abfragen vorzunehmen, wenn es notwendig wird.
        //public static void Suml_p(string txtProductName, Label lblSum)
        //{
        //    var query = BuildQuery(txtProductName);
        //    var result = ExecuteQuery(txtProductName, query);
        //    lblSum.Text = result.ToString();
        //}


    }
}
