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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public partial class DifferenceTables : Form
    {
        public DifferenceTables()
        {
            InitializeComponent();


            dgvProduct.Rows.Clear();
            //Select* from Inventar where productid Not in (select ID from product)
            string query = "SELECT P.product_id, P.productName, P.date, P.price, C.categoryName " +
                "FROM product AS P JOIN category AS C ON C.category_id=P.category_id " +
                "  WHERE product_id NOT IN (select product_id from inventarfk AS I WHERE P.product_id=I.product_id) ";
                       

            try
            {
                MySqlConnection con = DataImport.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), DataImport.Datetime(dr[2].ToString()), dr[3].ToString(), dr[4].ToString());
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
