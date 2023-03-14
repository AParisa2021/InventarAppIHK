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
            DataImport.LoadFormProduct(dgvProduct);

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataImport.ProductEditDelete(dgvProduct, e);
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
    }
}
