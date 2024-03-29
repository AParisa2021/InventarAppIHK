﻿using Google.Protobuf.WellKnownTypes;
using InventarAppIHK.Import;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace InventarAppIHK
{
    public class ProdMethods
    {
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
            int categoryId = CatMethods.GetCategoryId(categoryName);


            try
            {
                MySqlConnection con = Utility.GetConnection();
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.Add("productName", MySqlDbType.VarChar).Value = productName;
                    command.Parameters.Add("date", MySqlDbType.VarChar).Value = date;
                    command.Parameters.Add("price", MySqlDbType.Double).Value = price;
                    command.Parameters.Add("category_id", MySqlDbType.Int32).Value = categoryId;
                    command.ExecuteNonQuery();
                }
                con.Close();
                MessageBox.Show("Ihre Auswahl wurde eingetragen!");

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Bitte geben Sie einen gültigen Preis an!");
            }
        }

        /// <summary>
        /// holt die jeweilige product_id des jeweiligen locationName aus der category Tabelle und gibt die LocationId als Rückgabewert zurück
        /// </summary>
        /// <param name="productName"></param>
        /// <returns="productid"></returns>
        public static int GetProductId(string productName)
        {

            string query = "SELECT product_id FROM product WHERE productName = @productName";

            int productid = 0;
            try
            {
                MySqlConnection con = Utility.GetConnection();
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
        /// Änderung eines Datensatzes der Tabelle product mit Update-Query
        /// </summary>
        /// <param name="product"></param>
        //public static void UpdateProduct(Product product)
        //{
        //    string query = "UPDATE product SET productName =@productName, date =@date, price=@price, " +
        //        "category_id=@category_id WHERE product_id=@product_id";

        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = product.GetProductID();
        //        command.Parameters.Add("@productName", MySqlDbType.VarChar).Value = product.GetProductName();
        //        command.Parameters.Add("@date", MySqlDbType.VarChar).Value = product.GetDate().ToString("yyyy-MM-dd");
        //        command.Parameters.Add("@price", MySqlDbType.Double).Value = product.GetPrice();
        //        command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = product.GetCategory_id();

        //        command.ExecuteNonQuery();
        //        MessageBox.Show("Produkt update");

        //        MySqlDataReader dr = command.ExecuteReader();
        //        dr.Close();
        //        con.Close();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        //*******************Beginn UpdateProduct

        //CreateUpdateProductCommand - Erstellt das MySqlCommand Objekt, das für das Aktualisieren eines Produkts verwendet wird.
        //Diese Methode nimmt ein Produktobjekt als Parameter und gibt ein MySqlCommand Objekt zurück.
        private static MySqlCommand CreateUpdateProductCommand(Product product)
        {
            string query = "UPDATE product SET productName =@productName, date =@date, price=@price, " +
                        "category_id=@category_id WHERE product_id=@product_id";

            MySqlConnection con = Utility.GetConnection();
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.Add("@product_id", MySqlDbType.Int32).Value = product.GetProductID();
            command.Parameters.Add("@productName", MySqlDbType.VarChar).Value = product.GetProductName();
            command.Parameters.Add("@date", MySqlDbType.VarChar).Value = product.GetDate().ToString("yyyy-MM-dd");
            command.Parameters.Add("@price", MySqlDbType.Double).Value = product.GetPrice();
            command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = product.GetCategory_id();

            return command;
        }


        //ExecuteUpdateProductCommand - Führt das MySqlCommand Objekt aus, das zum
        //Aktualisieren eines Produkts verwendet wird. Diese Methode nimmt ein
        //MySqlCommand Objekt als Parameter und gibt nichts zurück.
        private static void ExecuteUpdateProductCommand(MySqlCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Produkt update");

                MySqlDataReader dr = command.ExecuteReader();
                dr.Close();
                command.Connection.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }


        //UpdateProduct - Aktualisiert das Produkt, indem es die beiden oben genannten Methoden aufruft. Diese Methode nimmt ein
        //Produktobjekt als Parameter und gibt nichts zurück.
        public static void UpdateProduct(Product product)
        {
            MySqlCommand command = CreateUpdateProductCommand(product);
            ExecuteUpdateProductCommand(command);
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
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), Utility.PriceFormat(dr[3].ToString()), dr[4].ToString());
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
        /// mit count product_id zählt diese Methode die Anzahl der (Datensätze)Produkte, die mit where txtSelect gefiltert werden, also wie ist die
        /// Anzahl des jeweils gesuchten Produkts
        /// </summary>
        /// <param name="txtSelect"></param>
        /// <param name="lblSum"></param>
        //public static void SumProduct(DataGridView dgvProduct, string txtSelect, Label lblSum)
        //{
        //    dgvProduct.Rows.Clear();

        //    string query = "SELECT P.product_id, P.productName, P.date, P.price, C.categoryName " +
        //                    "FROM product AS P " +
        //                    "JOIN category AS C ON C.category_id= P.category_id " +
        //                    "WHERE P.productName like concat('%' , @search_product , '%')";

        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);

        //        command.Parameters.AddWithValue("@search_product", txtSelect.ToUpper());

        //        MySqlDataReader dr = command.ExecuteReader();
        //        int i = 0;
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), Utility.PriceFormat(dr[3].ToString()), dr[4].ToString());

        //                i++;
        //            }
        //        }
        //        command.Dispose();
        //        con.Close();
        //        dr.Close();
        //        lblSum.Text = i.ToString();

        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message + "Error");
        //    }
        //}

        //********************Beginn Sum

        //I extracted the data loading functionality to LoadProductData, which takes the dgvProduct and command as parameters and sets the lblSum label. This method is responsible for reading the data and adding the rows to the dgvProduct.
        //I also extracted the row adding logic to a new method called AddProductRow, which takes a dgvProduct and dr(data reader) as parameters and adds a new row to the dgvProduct with the values from the dr.This method is only responsible for adding a single row to the dgvProduct.

        public static void SumProduct(DataGridView dgvProduct, string txtSelect, Label lblSum)
        {
            dgvProduct.Rows.Clear();

            string query = "SELECT P.product_id, P.productName, P.date, P.price, C.categoryName " +
                            "FROM product AS P " +
                            "JOIN category AS C ON C.category_id= P.category_id " +
                            "WHERE P.productName like concat('%' , @search_product , '%')";

            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);

                command.Parameters.AddWithValue("@search_product", txtSelect.ToUpper());

                LoadProductData(dgvProduct, command, lblSum);

                command.Dispose();
                con.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + "Error");
            }
        }

        private static void LoadProductData(DataGridView dgvProduct, MySqlCommand command, Label lblSum)
        {
            MySqlDataReader dr = command.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    AddProductRow(dgvProduct, dr);
                    i++;
                }
            }
            dr.Close();
            lblSum.Text = i.ToString();
        }

        private static void AddProductRow(DataGridView dgvProduct, MySqlDataReader dr)
        {
            dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), Utility.PriceFormat(dr[3].ToString()), dr[4].ToString());
        }



        /// <summary>
        /// Im DataGridView im ProductForm kann der User auf edit oder delete drücken. Um einen Datensatz zu löschen oder um das ProductInsertForm zu öffnen um den Datensatz zu ändern
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        //public static void ProductEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        //{
        //    string columnName = dgv.Columns[e.ColumnIndex].Name;
        //    int productID = ProdMethods.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());

        //    if (columnName == "edit")
        //    {
        //        ProductInsertForm productInsert = new ProductInsertForm();
        //        productInsert.btnSave.Enabled = false;
        //        productInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
        //        productInsert.dtOrder.Text = (dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
        //        productInsert.txtName.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
        //        productInsert.txtPrice.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
        //        productInsert.comboCategory.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
        //        productInsert.Text = "Update";
        //        productInsert.ShowDialog();
        //    }

        //    else if (columnName == "delete")
        //    {
        //        if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz inklusive Verknüpfungen löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {

        //            try
        //            {
        //                MySqlConnection con = Utility.GetConnection();

        //                int product_id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);
        //                MySqlCommand commandProduct = new MySqlCommand("DELETE FROM product WHERE product_id=" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()), con);
        //                commandProduct.ExecuteNonQuery();
        //                con.Close();

        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Sie können diese Zeile nicht löschen, da sie in einer Elternspalte verwendet wird", ex.Message);

        //            }
        //        }
        //    }
        //}


        //*****************Beginn Edit Delete
        public static void ProductEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            int productID = ProdMethods.GetProductId(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());

            if (columnName == "edit")
            {
                OpenProductInsertForm(dgv, e);
            }
            else if (columnName == "delete")
            {
                DeleteProductFromDatabase(dgv, e);
            }
        }

        private static void OpenProductInsertForm(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            ProductInsertForm productInsert = new ProductInsertForm();
            productInsert.btnSave.Enabled = false;
            productInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            productInsert.dtOrder.Text = (dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
            productInsert.txtName.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
            productInsert.txtPrice.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            productInsert.comboCategory.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            productInsert.Text = "Update";
            productInsert.ShowDialog();
        }

        private static void DeleteProductFromDatabase(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz inklusive Verknüpfungen löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    MySqlConnection con = Utility.GetConnection();

                    int product_id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);
                    MySqlCommand commandProduct = new MySqlCommand("DELETE FROM product WHERE product_id=" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()), con);
                    commandProduct.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sie können diese Zeile nicht löschen, da sie in einer Elternspalte verwendet wird", ex.Message);
                }
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
                MySqlConnection conn = Utility.GetConnection();
                string sql = "select categoryName from category";

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
        /// Filterung Preis von, Preis bis und Filterung nach Produktnamen
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="txtFrom"></param>
        /// <param name="txtTo"></param>
        /// <param name="txtSelect"></param>
        /// <param name="lblTotal"></param>
        /// <param name="lblSum"></param>
        public static void SelectPriceProduct(DataGridView dgv, string txtFrom, string txtTo, string txtSelect, Label lblTotal, Label lblSum)
        {

            double total = 0;
            dgv.Rows.Clear();
            MySqlConnection con = Utility.GetConnection();
            string query = "SELECT P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName " +
                "from l_p AS LP " +
                "INNER JOIN product AS P " +
                 "ON LP.product_id=P.product_id " +
                "INNER JOIN category AS C " +
                "ON P.category_id=C.category_id " +
                "INNER JOIN location AS L " +
                "ON LP.location_id=L.location_id ";

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

                query += " concat(P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName ) like concat('%' , @search_totalinventar , '%')";
            }
            using (MySqlCommand comm = new MySqlCommand(query, con))
            {
                {
                    try
                    {
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
                                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString().Substring(0, 10), Utility.PriceFormat(dr[3].ToString()), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());

                                total += double.Parse(dr[3].ToString());
                                i++;
                            }
                        }
                        comm.Dispose();
                        con.Close();
                        dr.Close();

                        lblTotal.Text = total.ToString();
                        lblSum.Text = i.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        //private static List<Product> GetProductsFromDatabase(DataGridView dgv, string txtFrom, string txtTo, string txtSelect, Label lblTotal, Label lblSum)
        //{
        //    MySqlConnection con = Utility.GetConnection();
        //    string query = "SELECT P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName " +
        //                   "from l_p AS LP " +
        //                   "INNER JOIN product AS P " +
        //                   "ON LP.product_id=P.product_id " +
        //                   "INNER JOIN category AS C " +
        //                   "ON P.category_id=C.category_id " +
        //                   "INNER JOIN location AS L " +
        //                   "ON LP.location_id=L.location_id ";

        //    if (txtFrom.Trim() != "" || txtTo.Trim() != "" || txtSelect.ToUpper().Trim() != "")
        //    {
        //        query += " WHERE ";

        //        if (txtFrom.Trim().Length > 0 && txtTo.Trim().Length > 0)
        //        {
        //            query += " price BETWEEN @priceFrom AND @priceTo";
        //        }
        //        else if (txtFrom.Trim().Length > 0)
        //        {
        //            query += "price >= @priceFrom";
        //        }
        //        else if (txtTo.Trim().Length > 0)
        //        {
        //            query += " price <= @priceTo";
        //        }

        //        if (txtSelect.Trim() != "")
        //        {
        //            if (txtFrom.Trim().Length > 0 || txtTo.Trim().Length > 0)
        //            {
        //                query += " AND ";
        //            }

        //            query += " concat(P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName ) like concat('%' , @search_totalinventar , '%')";
        //        }
        //    }

        //    using (MySqlCommand comm = new MySqlCommand(query, con))
        //    {
        //        if (txtSelect.Trim() != "")
        //        {
        //            comm.Parameters.AddWithValue("@search_totalinventar", txtSelect.ToUpper());
        //        }

        //        if (txtFrom.Trim() != "")
        //        {
        //            comm.Parameters.AddWithValue("@priceFrom", double.Parse(txtFrom));
        //        }

        //        if (txtTo.Trim() != "")
        //        {
        //            comm.Parameters.AddWithValue("@priceTo", double.Parse(txtTo));
        //        }

        //        List<Product> products = new List<Product>();

        //        try
        //        {
        //            MySqlDataReader dr = comm.ExecuteReader();

        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    Product product = new Product();
        //                    product.SetProductID(int.Parse(dr[0].ToString()));
        //                    product.SetProductName(dr[1].ToString());
        //                    product.SetDate(DateTime.Parse(dr[2].ToString()));
        //                    product.SetPrice(double.Parse(dr[3].ToString()));
        //                    product.SetSerialNumber(dr[4].ToString());
        //                    product.SetCategoryName(dr[5].ToString());
        //                    product.SetFloor(dr[6].ToString());
        //                    product.SetLocationName(dr[7].ToString());

        //                    products.Add(product);
        //                }
        //            }


        //            dr.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }

        //        return products;
        //    }
        //}

        //private static string GetSelectQuery(string txtFrom, string txtTo, string txtSelect)
        //{
        //    string query = "SELECT P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName " +
        //                   "from l_p AS LP " +
        //                   "INNER JOIN product AS P " +
        //                   "ON LP.product_id=P.product_id " +
        //                   "INNER JOIN category AS C " +
        //                   "ON P.category_id=C.category_id " +
        //                   "INNER JOIN location AS L " +
        //                   "ON LP.location_id=L.location_id ";

        //    if (txtFrom.Trim() != "" || txtTo.Trim() != "" || txtSelect.ToUpper().Trim() != "")
        //    {
        //        query += " WHERE ";

        //        if (txtFrom.Trim().Length > 0 && txtTo.Trim().Length > 0)
        //        {
        //            query += " price BETWEEN @priceFrom AND @priceTo";
        //        }
        //        else if (txtFrom.Trim().Length > 0)
        //        {
        //            query += "price >= @priceFrom";
        //        }
        //        else if (txtTo.Trim().Length > 0)
        //        {
        //            query += " price <= @priceTo";
        //        }

        //        if (txtSelect.Trim() != "")
        //        {
        //            if (txtFrom.Trim().Length > 0 || txtTo.Trim().Length > 0)
        //            {
        //                query += " AND ";
        //            }

        //            query += " concat(P.product_id, P.productName, P.date, P.price, LP.seriennummer, C.categoryName, L.floor, L.locationName ) like concat('%' , @search_totalinventar , '%')";
        //        }
        //    }

        //    return query;
        //}

        //private static List<MySqlParameter> GetParameters(string txtFrom, string txtTo, string txtSelect)
        //{
        //    List<MySqlParameter> parameters = new List<MySqlParameter>();

        //    if (txtSelect.Trim() != "")
        //    {
        //        parameters.Add(new MySqlParameter("@search_totalinventar", "%" + txtSelect.ToUpper() + "%"));
        //    }

        //    if (txtFrom.Trim() != "")
        //    {
        //        parameters.Add(new MySqlParameter("@priceFrom", double.Parse(txtFrom)));
        //    }

        //    if (txtTo.Trim() != "")
        //    {
        //        parameters.Add(new MySqlParameter("@priceTo", double.Parse(txtTo)));
        //    }

        //    return parameters;
        //}

        //private static List<Product> GetProductsFromDataReader(MySqlDataReader dr)
        //{
        //    List<Product> products = new List<Product>();

        //    while (dr.Read())
        //    {
        //        Product product = new Product();
        //        product.SetProductID(int.Parse(dr[0].ToString()));
        //        product.SetProductName(dr[1].ToString());
        //        product.SetDate(DateTime.Parse(dr[2].ToString()));
        //        product.SetPrice(double.Parse(dr[3].ToString()));
        //        product.SetSerialNumber(dr[4].ToString());
        //        product.SetCategoryName(dr[5].ToString());
        //        product.SetFloor(dr[6].ToString());
        //        product.SetLocationName(dr[7].ToString());

        //        products.Add(product);
        //    }

        //    return products;
        //}

        //private static void SetDataToDataGridView(DataGridView dgv, List<Product> products)
        //{
        //    dgv.Rows.Clear();

        //    foreach (Product product in products)
        //    {
        //        int rowId = dgv.Rows.Add();
        //        DataGridViewRow row = dgv.Rows[rowId];
        //        row.Cells["ProductID"].Value = product.GetProductID();
        //        row.Cells["ProductName"].Value = product.GetProductName();
        //        row.Cells["Date"].Value = product.GetDate();
        //        row.Cells["Price"].Value = product.GetPrice();
        //        row.Cells["SerialNumber"].Value = product.GetSerialNumber();
        //        row.Cells["CategoryName"].Value = product.GetCategoryName();

        //    }
        //}
    }
}
    

