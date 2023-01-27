using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace InventarAppIHK
{
    public partial class ProdCatForm : Form
    {
        static string connstring = "datasource=localhost;port=3306;username=root;password=;database=inventar";

        public ProdCatForm()
        {
            InitializeComponent();
            LoadProduct();
            FillComboBox();
        }

        private void dgVProdCat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void FillComboBox()
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

        public void LoadProduct()
        {
            dgVProdCat.Rows.Clear();
            string sql = "select product_id, productName, date, price from product where concat(product_id, productName, date, price) like '%' || @search_product || '%'"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter

            MySqlConnection con = new MySqlConnection(connstring);
            //string sql = "select * from product where concat(product_id, productName) like '" + txtSearchProduct + "'";         //Concat Funktion verbindet zwei Strings zu einem gemeinsamen String. Die Funktion Concat wird oft für Views oder einfache Abfragen benötigt, wo zwei Teile miteinander verbunden werden müssen
            //string sql = "select customer_id, customerName from public.tbcustomer where concat(customer_id, customerName) like '%" + txtSearchCustomer.Text + "%'"; //concat stellt Text als UTF16 Codeeinheit dar

            //string sql = "select * from public.tbcustomer ";
            //MessageBox.Show(sql);
            con.Open();
            MySqlCommand comm = new MySqlCommand(sql, con);
            comm.Parameters.AddWithValue("@search_product", txtSearchProduct.Text);

            MySqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
                    dgVProdCat.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                }
            }
            comm.Dispose();
            con.Close();
            dr.Close();
        }
    }
}
