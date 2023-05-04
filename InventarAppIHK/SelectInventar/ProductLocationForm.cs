using InventarAppIHK.Import;
using InventarAppIHK.SelectInventar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventarAppIHK
{
    public partial class ProductLocationForm : Form
    {
        public ProductLocationForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            //CSVDataImport.LoadFormLocation(dgvLocation);
            dgvProduct.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InventarMethods.TxtLocation(dgvLocation, txtSelectLocation.Text.ToUpper());
            TxtSelectProd();
        }

        /// <summary>
        /// lädt die Daten aus den Tabellen product und category mit einem INNER JOIN in ProductLocationForm
        /// </summary>

        public void TxtSelectProd()
        {
             InventarMethods.TxtSelectProd(dgvProduct, txtSelectProduct.Text.ToUpper());
        }
        
        /// <summary>
        /// Datum-Formatierung. Datum ohne Uhrzeit ausgeben
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private static string Datetime(string datetime)
        {
            string timeFormat = Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
            return timeFormat;
        }
       
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSelectLocation_TextChanged(object sender, EventArgs e)
        {
            InventarMethods.TxtLocation(dgvLocation, txtSelectLocation.Text.ToUpper());
        }

        private void dgvLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLocationID.Text = dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLNumber.Text = dgvLocation.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtLName.Text = dgvLocation.Rows[e.RowIndex].Cells[2].Value.ToString();   
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {           
                txtPNumber.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDate.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString().Substring(0, 10);
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
            if (txtSerial.Text == "")
            {
                MessageBox.Show("Bitte geben Sie eine Seriennummer ein!");

            }
            else if (txtProductID.Text != "" || txtLocationID.Text != "" || txtSerial.Text != "")
            {
                try
                {
                    //int category_id = CatMethods.GetCategoryId(txtCategoryName.Text);
                    int location_id = LocMethods.GetLocationId(txtLName.Text);
                    int product_id = ProdMethods.GetProductId(txtPName.Text);
                    Inventar inventar = new Inventar (location_id, product_id, txtSerial.Text);
                    InventarMethods.ChooseInventar(inventar);
                }
                catch(Exception ex)
                { 
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void txtSelectProduct_TextChanged(object sender, EventArgs e)
        {
            TxtSelectProd();
            ProdMethods.SumProduct(txtSelectProduct.Text, lblSum);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Utility.ClearAllFields(this);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //int category_id = CatMethods.GetCategoryId(txtCategoryName.Text);
            //int l_p_id = InventarMethods.Getl_p_Id(txtLPId.Text);
            //int product_id = ProdMethods.GetProductId(txtPName.Text);
            //int location_id = LocMethods.GetLocationId(txtLName.Text);
            //Inventar updateInventar = new Inventar(int.Parse(txtProductID.Text), /*product_id, */category_id, location_id);
            //Inventar updateInventar = new Inventar(int.Parse(txtLPId.Text), /*int.Parse(txtProductID.Text),*/ location_id, product_id, txtSerial.Text );
            Inventar updateInventar = new Inventar(int.Parse(txtLPId.Text), int.Parse(txtLocationID.Text),int.Parse(txtProductID.Text),txtSerial.Text);

            InventarMethods.UpdateProductLocation(updateInventar);
        }       

    }
}


