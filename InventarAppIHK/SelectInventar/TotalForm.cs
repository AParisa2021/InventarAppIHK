using InventarAppIHK.Import;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventarAppIHK
{
    public partial class TotalForm : Form
    {
        public TotalForm()
        {
            InitializeComponent();
            //LoadOrder();
            SelectPriceProduct();
        }

        public void SelectPriceProduct()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            double total = 0;
            dgvTotal.Rows.Clear();
            MySqlConnection conn = new MySqlConnection(sql);
            string query = "SELECT inventar_id, P.productName, P.date, P.price, C.categoryName, L.floor, L.locationName " +
                "from inventar AS I " +
                "INNER JOIN product AS P " +
                 "ON I.product_id=P.product_id " +
                "INNER JOIN category AS C " +
                "ON I.category_id=C.category_id " +
                "INNER JOIN location AS L " +
                "ON I.location_id=L.location_id ";

            //MySqlCommand comm = new MySqlCommand(query, conn);
            if (txtFrom.Text.Trim() != "" || txtTo.Text.Trim() != "" || txtSelect.Text.ToUpper().Trim() != "") query += " WHERE ";

            if (txtFrom.Text.Trim().Length > 0 && txtTo.Text.Trim().Length > 0)
            {
                query += " price BETWEEN @priceFrom AND @priceTo";

                //query += "price between (Cast('" + txtFrom.Text + "' AS double)) AND (Cast('" + txtTo.Text + "' AS double))";
            }
            else if (txtFrom.Text.Trim().Length > 0)
            {
                //query += " price >=  500";

                query += "price >= @priceFrom";           
                //productqty = (productqty - '" + numericUpDown1.Value + "')
            }
            else if (txtTo.Text.Trim().Length > 0)
            {
                //query += " price <= 300";

                query += " price <= @priceTo";

                //query += " price <= (Cast('" + txtTo.Text + "' AS double))";

            }
            if (txtSelect.Text.Trim() != "")
            {
                if (txtFrom.Text.Trim().Length > 0 || txtTo.Text.Trim().Length > 0)
                    query += " AND ";

                query += " concat(I.inventar_id, P.productName, P.date, P.price, C.categoryName, L.floor, L.locationName ) like concat('%' , @search_totalinventar , '%')";
            }
            //using (MySqlConnection conn = new MySqlConnection(sql))
            using (MySqlCommand comm = new MySqlCommand(query, conn)) {
                {
                    try
                    {
                        conn.Open();
                        MessageBox.Show(query);
                        //comm.Parameters.AddWithValue("@price", Convert.ToDouble(txtFrom.Text));

                        if(txtSelect.Text.Trim() != "")
                            comm.Parameters.AddWithValue("@search_totalinventar", txtSelect.Text.ToUpper());

                        if(txtFrom.Text.Trim() != "")
                            comm.Parameters.AddWithValue("@priceFrom", double.Parse(txtFrom.Text));

                        if(txtTo.Text.Trim() != "")
                            comm.Parameters.AddWithValue("@priceTo", double.Parse(txtTo.Text));

                        MySqlDataReader dr = comm.ExecuteReader();
                        int i = 0;
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                dgvTotal.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), double.Parse(dr[3].ToString()), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

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


        /// <summary>
        /// Lädt Die Tabelle Produkte aus der Datenbank und gibt sie in der DataGridView aus
        /// </summary>
        private void LoadOrder()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            double total = 0;
            dgvTotal.Rows.Clear();
            MySqlConnection conn = new MySqlConnection(sql);
            string query = "SELECT total_id, productName, date, price, C.categoryName, L.floor, L.locationName " +
                "from totalinventar AS T " +
                "INNER JOIN category AS C " +
                "ON T.category_id=C.category_id " +
                "INNER JOIN location AS L " +
                "ON T.location_id=L.location_id";


            //"SELECT total_id, productName, date, price, category_id, product_id" +
            //"from totalinventar /*JOIN category ON category.category_id=totalinventar.category_id JOIN location ON totalinventar.location_id=location.location_id*/";
            //            SELECT Orders.OrderID, Customers.CustomerName, Orders.OrderDate
            //FROM Orders
            //INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID;
            //"  O.productid, P.productname, O.customerid, C.customername, qty, price, total " +
            //         "from public.tborderjoin AS O " +
            //         "JOIN tbcustomer AS C " +
            //         "ON O.customerid=C.customer_id " +
            //         "JOIN tbproduct AS P " +
            //         "ON O.productid=P.product_id " +
            //         "WHERE UPPER(CONCAT(order_id, oderdate, O.productid, P.productname, O.customerid, C.customername, qty, price)) " +
            //         "LIKE '%" + txtSearch.Text.ToUpper() + "%'"; //'%" zwischen den Anführungszeichen, weil SQL LIKE QUery. Gehört nicht zum C# string

            conn.Open();
            MySqlCommand comm = new MySqlCommand(query, conn);
            MySqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dgvTotal.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

                    total += double.Parse(dr[3].ToString());
                }
            }
            comm.Dispose();
            conn.Close();
            dr.Close();

            lblTotal.Text = total.ToString();
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            SelectPriceProduct();

            //    string connection = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            //    MySqlConnection conn = new MySqlConnection(connection);


            //    double total = 0;

            //    dgvTotal.Rows.Clear();          //ohne Clear() macht er eine Ausgabe der eingetippten Suche, hängt das Ergebnis bzw. die Ausgabe hinten dran. Gibt Datensätze zurück
            //    conn.Open();


            //    MySqlCommand command = new MySqlCommand(Kleiner(), conn);
            //    if (txtTo.Text.Trim() != "") command.Parameters.AddWithValue("@price", double.Parse(txtTo.Text));

            //    MySqlDataReader dr = command.ExecuteReader();

            //    if (dr.HasRows)
            //    {

            //        while (dr.Read())
            //        {
            //            dgvTotal.Rows.Add(dr[0].ToString(), /*NullProblemDatabase*/(dr[1].ToString()), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            //            total += double.Parse(dr[3].ToString());
            //        }
            //    }
            //    command.Dispose();
            //    conn.Close();
            //    dr.Close();
            //}

            //private string Kleiner()
            //{

            //    string sql = "SELECT total_id, productName, date, price, C.categoryName, L.floor, L.locationName " +
            //                 "from totalinventar AS T " +
            //                 "INNER JOIN category AS C " +
            //                 "ON T.category_id=C.category_id " +
            //                 "INNER JOIN location AS L " +
            //                 "ON T.location_id=L.location_id";


            //    if (txtTo.Text.Trim() != "") sql += " WHERE ";

            //    if (txtTo.Text.Trim() != "")
            //    {
            //        if (txtTo.Text.Trim() != "")/* sql += " AND ";*/
            //            sql += "price <= @price";
            //    }

            //    return sql;
        }

        private string Groesser()
        {

            string sql = "SELECT total_id, productName, date, price, C.categoryName, L.floor, L.locationName " +
                         "from totalinventar AS T " +
                         "INNER JOIN category AS C " +
                         "ON T.category_id=C.category_id " +
                         "INNER JOIN location AS L " +
                         "ON T.location_id=L.location_id";



            if (txtFrom.Text.Trim() != "") sql += " WHERE ";

            if (txtFrom.Text.Trim() != "")
            {
                if (txtFrom.Text.Trim() != "")/* sql += " AND ";*/
                    sql += "price >= @price";
            }
            return sql;
        }
        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            SelectPriceProduct();
            //        string connection = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            //        MySqlConnection conn = new MySqlConnection(connection);


            //        double total = 0;

            //        dgvTotal.Rows.Clear();          //ohne Clear() macht er eine Ausgabe der eingetippten Suche, hängt das Ergebnis bzw. die Ausgabe hinten dran. Gibt Datensätze zurück
            //        conn.Open();


            //        MySqlCommand command = new MySqlCommand(Groesser(), conn);
            //        if (txtFrom.Text.Trim() != "") command.Parameters.AddWithValue("@price", double.Parse(txtFrom.Text));

            //        MySqlDataReader dr = command.ExecuteReader();

            //        if (dr.HasRows)
            //        {

            //            while (dr.Read())
            //            {
            //                dgvTotal.Rows.Add(dr[0].ToString(), /*NullProblemDatabase*/(dr[1].ToString()), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            //                total += double.Parse(dr[3].ToString());
            //            }
            //        }
            //        command.Dispose();
            //        conn.Close();
            //        dr.Close();
            //    }
            //}
        }

        private void txtSelect_TextChanged(object sender, EventArgs e)
        {
            SelectPriceProduct();
        }

        private void dgvTotal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //SelectPriceProduct();
        }
    }
}
//https://stackoverflow.com/questions/23106780/c-sharp-mysql-select-with-textbox-text