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
using System.Windows.Forms;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventarAppIHK
{
    public partial class ProductLocationForm : Form
    {
        public ProductLocationForm()
        {
            InitializeComponent();
            MyInitializeComponent();

        }

        public void MyInitializeComponent()
        {
            //DataImport.LoadFormProduct(dgvProduct);
            //CSVDataImport.LoadFormLocation(dgvLocation);
            DataImport.TxtLocation(dgvLocation, txtSelectLocation.Text.ToUpper());
            TxtSelectProd();
        }

        /// <summary>
        /// lädt die Daten aus den Tabellen product und category mit einem INNER JOIN in ProductLocationForm
        /// </summary>

        public void TxtSelectProd()
        {
             DataImport.TxtSelectProd(dgvProduct, txtSelectProduct.Text.ToUpper());
        }

        /// <summary>
        /// Datum-Formatierung. Datum ohne Uhrzeit ausgeben
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private static string Datetime(string datetime)
        {
            //string timeFormat = datetime.Substring(datetime.Length - 7);
            string timeFormat = Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
            return timeFormat;
        }
       
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSelectLocation_TextChanged(object sender, EventArgs e)
        {
            DataImport.TxtLocation(dgvLocation, txtSelectLocation.Text.ToUpper());
        }

        private void dgvLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLNumber.Text = dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLName.Text = dgvLocation.Rows[e.RowIndex].Cells[2].Value.ToString();   
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPNumber.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDate.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString().Substring(0,10);
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtCategoryName.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtLName.Text == "")
            {
                MessageBox.Show("Bitte wählen Sie ein Standort!");
            }
            if (txtPName.Text == "")
            {
                MessageBox.Show("Bitte wählen Sie ein Produkt!");

            }
            else
            {
                int category_id = DataImport.GetCategoryId(txtCategoryName.Text);
                int location_id = DataImport.GetLocationId(txtLName.Text);
                int product_id = DataImport.GetProductId(txtPName.Text);
                Inventar inventar = new Inventar(product_id, category_id, location_id);
                DataImport.ChooseInventar(inventar);
                MessageBox.Show("Ihre Auswahl wurde eingetragen!");
            }
           
        }

        private void txtSelectProduct_TextChanged(object sender, EventArgs e)
        {
            TxtSelectProd();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataImport.ClearAllText(this);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {       
            int category_id = DataImport.GetCategoryId(txtCategoryName.Text);
            int product_id = DataImport.GetProductId(txtPName.Text);
            int location_id = DataImport.GetLocationId(txtLName.Text);
            Inventar updateInventar = new Inventar(int.Parse(txtInventarId.Text), product_id, category_id, location_id);
            DataImport.UpdateProductLocation(updateInventar);
        }       

    }
}


