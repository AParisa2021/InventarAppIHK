using InventarAppIHK.Import;
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

namespace InventarAppIHK
{
    public partial class TotalForm : Form
    {
        public TotalForm()
        {
            InitializeComponent();
            LoadOrder();
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
            string connection = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            MySqlConnection conn = new MySqlConnection(connection);


            double total = 0;

            dgvTotal.Rows.Clear();          //ohne Clear() macht er eine Ausgabe der eingetippten Suche, hängt das Ergebnis bzw. die Ausgabe hinten dran. Gibt Datensätze zurück
            conn.Open();
     

            MySqlCommand command = new MySqlCommand(Kleiner(), conn);
            if (txtTo.Text.Trim() != "") command.Parameters.AddWithValue("@price", double.Parse(txtTo.Text));

            MySqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    dgvTotal.Rows.Add(dr[0].ToString(), /*NullProblemDatabase*/(dr[1].ToString()), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                    total += double.Parse(dr[3].ToString());
                }
            }
            command.Dispose();
            conn.Close();
            dr.Close();
        }
       
        private string Kleiner()
        {

            string sql = "SELECT total_id, productName, date, price, C.categoryName, L.floor, L.locationName " +
                         "from totalinventar AS T " +
                         "INNER JOIN category AS C " +
                         "ON T.category_id=C.category_id " +
                         "INNER JOIN location AS L " +
                         "ON T.location_id=L.location_id";


            if (txtTo.Text.Trim() != "") sql += " WHERE ";
          
            if (txtTo.Text.Trim() != "")
            {
                if (txtTo.Text.Trim() != "")/* sql += " AND ";*/
                    sql += "price <= @price";
            }
       
            return sql;
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
            string connection = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            MySqlConnection conn = new MySqlConnection(connection);


            double total = 0;

            dgvTotal.Rows.Clear();          //ohne Clear() macht er eine Ausgabe der eingetippten Suche, hängt das Ergebnis bzw. die Ausgabe hinten dran. Gibt Datensätze zurück
            conn.Open();


            MySqlCommand command = new MySqlCommand(Groesser(), conn);
            if (txtFrom.Text.Trim() != "") command.Parameters.AddWithValue("@price", double.Parse(txtFrom.Text));

            MySqlDataReader dr = command.ExecuteReader();

            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    dgvTotal.Rows.Add(dr[0].ToString(), /*NullProblemDatabase*/(dr[1].ToString()), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                    total += double.Parse(dr[3].ToString());
                }
            }
            command.Dispose();
            conn.Close();
            dr.Close();
        }
    }
}
