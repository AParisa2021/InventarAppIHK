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
    
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ProdMethods.SumProduct(txtSearch.Text, lblSum);
        }
    }
}
