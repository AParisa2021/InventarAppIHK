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
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            DataImport.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal);
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            DataImport.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal);
        }


        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            //SelectPriceProduct();
            DataImport.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal);
        }

        private void txtSelect_TextChanged(object sender, EventArgs e)
        {
            DataImport.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal);
        }

        /// <summary>
        /// Löschen und Bearbeiten des Datensatzes im DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTotal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataImport.LoadFormTotal(dgvTotal);
            DataImport.TotalEditDelete(dgvTotal, e);
            DataImport.LoadFormTotal(dgvTotal, e);
            DataImport.SelectPriceProduct(dgvTotal, txtFrom.Text, txtTo.Text, txtSelect.Text, lblTotal);

            //MyInitializeComponent();
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
    }
}
