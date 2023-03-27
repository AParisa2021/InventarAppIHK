using InventarAppIHK.Import;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventarAppIHK
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        public void MyInitializeComponent()
        {
            ProdMethods.LoadFormProduct(dgvProduct);

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdMethods.ProductEditDelete(dgvProduct, e);
            MyInitializeComponent();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductInsertForm insertProduct = new ProductInsertForm();
            insertProduct.btnSave.Enabled = true;
            insertProduct.btnUpdate.Enabled = false;
            insertProduct.ShowDialog();
            MyInitializeComponent();
        }

        private void lblSum_Click(object sender, EventArgs e)
        {
            //string query = "select Count(product_id) " +
            //               "from product where productName='" + txtSearch.Text.Trim().ToUpper() + "'";
            //try
            //{
            //    MySqlConnection con = Utility.GetConnection();
            //    MySqlCommand command = new MySqlCommand(query, con);
            //    MySqlDataReader dr = command.ExecuteReader();
            //    lblSum.Text = Convert.ToString(command);

            //    using (var cmd = new MySqlCommand(query, con))
            //    {
            //        lblSum.Text = cmd.ExecuteScalar().ToString();
            //    }

            //    con.Close();

            //}
            //catch (MySqlException ex)
            //{
            //    MessageBox.Show(ex.Message + "Error");
            //}
            //ProdMethods.SumProduct(txtSearch.Text, lblSum);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ProdMethods.SumProduct(txtSearch.Text, lblSum);

        }
    }
}
