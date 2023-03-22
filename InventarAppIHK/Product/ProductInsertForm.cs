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
using InventarAppIHK.Import;    //statische Methode implementiert die Datei in die Klasse ProductInsertForm
using System.Data.SqlClient;

namespace InventarAppIHK
{
    public partial class ProductInsertForm : Form
    {

        public ProductInsertForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }
        public void MyInitializeComponent()
        {
            ProdMethods.FillComboBox(comboCategory);
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Methode erhält aus der Klasse CSVDataImport mit der Methode GetCategoryId die category_id aus der Datenbank
        /// Sie gibt als Rückgabewert die entsprechende category_id aus und speichert sie in eine Variable
        /// Mit comboCategory wähle ich das entsprechende Produkt aus und übergebe der Methode die comboCategory als Parameter
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || comboCategory.Text == "")
                {
                    MessageBox.Show("Bitte wählen Sie einen Produktnamen und eine Kategorie.");

                }
                else if (txtName.Text != "" && comboCategory.Text != "")
                {
                    ProdMethods.InsertProduct(txtName.Text.Trim(), dtOrder.Text, Utility.formatDouble(txtPrice.Text), Utility.formatCategoryId(comboCategory.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dtOrder.CustomFormat = " ";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int category_id = CatMethods.GetCategoryId(comboCategory.Text);
            Product updateProduct = new Product(int.Parse(txtID.Text), txtName.Text, DateTime.Parse(dtOrder.Text), double.Parse(Utility.formatDouble(txtPrice.Text.Trim()).ToString()), category_id);
            ProdMethods.UpdateProduct(updateProduct);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            comboCategory.SelectedItem = null;
            Utility.ClearAllFields(this);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnUpdate.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter)
                {

                    if (txtName.Text == "" && comboCategory.Text == "")
                    {
                        MessageBox.Show("Bitte wählen Sie einen Produktnamen und eine Kategorie.");

                    }
                    else if (txtName.Text != "" && comboCategory.Text != "")
                    {
                        ProdMethods.InsertProduct(txtName.Text.Trim(), dtOrder.Text, Utility.formatDouble(txtPrice.Text.Trim()), Utility.formatCategoryId(comboCategory.Text));
                    }
                }
            }
            else if (btnSave.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtName.Text == "" && comboCategory.Text == "" && txtPrice.Text == "")
                    {
                        MessageBox.Show("Bitte geben Sie einen Ort an!");
                    }
                    else
                    {
                        int category_id = CatMethods.GetCategoryId(comboCategory.Text);
                        Product updateProduct = new Product(int.Parse(txtID.Text), txtName.Text, DateTime.Parse(dtOrder.Text), double.Parse(Utility.formatDouble(txtPrice.Text.Trim()).ToString()), category_id);
                        ProdMethods.UpdateProduct(updateProduct);
                    }
                }
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
      
             if (btnSave.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtName.Text == "" && comboCategory.Text == "" && txtPrice.Text == "")
                    {
                        MessageBox.Show("Bitte geben Sie einen Ort an!");
                    }
                    else
                    {
                        int category_id = CatMethods.GetCategoryId(comboCategory.Text);
                        Product updateProduct = new Product(int.Parse(txtID.Text), txtName.Text.Trim(), DateTime.Parse(dtOrder.Text), double.Parse(Utility.formatDouble(txtPrice.Text.Trim()).ToString()), category_id);
                        ProdMethods.UpdateProduct(updateProduct);
                    }
                }
            }
        }

        private void comboCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnUpdate.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                   
                        if (txtName.Text == "" && comboCategory.Text == "")
                        {
                            MessageBox.Show("Bitte wählen Sie einen Produktnamen und eine Kategorie.");

                        }
                        else if (txtName.Text != "" && comboCategory.Text != "")
                        {
                            ProdMethods.InsertProduct(txtName.Text.Trim(), dtOrder.Text, Utility.formatDouble(txtPrice.Text.Trim()), Utility.formatCategoryId(comboCategory.Text));
                        }           
                }
            }
            else if (btnSave.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtName.Text == "" && comboCategory.Text == "" && txtPrice.Text == "")
                    {
                        MessageBox.Show("Bitte geben Sie einen Ort an!");
                    }
                    else
                    {
                        int category_id = CatMethods.GetCategoryId(comboCategory.Text);
                        Product updateProduct = new Product(int.Parse(txtID.Text), txtName.Text.Trim(), DateTime.Parse(dtOrder.Text), double.Parse(Utility.formatDouble(txtPrice.Text.Trim()).ToString()), category_id);
                        ProdMethods.UpdateProduct(updateProduct);
                    }
                }
            }
        }
    }
}
