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

namespace InventarAppIHK
{
    public partial class TotalForm : Form
    {
        public TotalForm()
        {
            InitializeComponent();          
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            ProdMethods.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal, lblAmount);
            //ProdMethods.SetDataToDataGridView(dgvTotal, );
            dgvTotal.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            ProdMethods.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal, lblAmount);
        }


        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            ProdMethods.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal, lblAmount);
        }

        private void txtSelect_TextChanged(object sender, EventArgs e)
        {
            ProdMethods.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal, lblAmount);

        }

        /// <summary>
        /// Löschen und Bearbeiten des Datensatzes im DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTotal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            InventarMethods.TotalEditDelete(dgvTotal, e);
            ProdMethods.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal, lblAmount);

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductLocationForm openOrder = new ProductLocationForm();
            openOrder.btnUpdate.Enabled= false;
            openOrder.ShowDialog();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void TotalForm_Load(object sender, EventArgs e)
        {
            MyInitializeComponent();
        }

        private void txtSeriennummer_TextChanged(object sender, EventArgs e)
        {
            InventarMethods.Suml_p(txtProductName.Text, lblAmount);
        }
    }
}
