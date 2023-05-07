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
using System.Windows.Forms.DataVisualization.Charting;
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
            dgvProduct.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MyInitializeComponent();
            ProdMethods.ProductEditDelete(dgvProduct, e);
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
    
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ProdMethods.SumProduct(dgvProduct,txtSearch.Text, lblSum);
        }
    }
}
