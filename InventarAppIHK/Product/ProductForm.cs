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
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";
            string columnName = dgvProduct.Columns[e.ColumnIndex].Name;
            int productID = DataImport.GetProductId(dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString());

            if (columnName == "edit")
            {
                ProductInsertForm productInsert = new ProductInsertForm();
                productInsert.txtID.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();

                productInsert.dtOrder.Text = (dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString());
                productInsert.txtName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString(); ;
                productInsert.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productInsert.comboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                //CategoryInsertForm catInsert = new CategoryInsertForm();
                //catInsert.Name = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productInsert.ShowDialog();
            }
            //else if(columnName == "delete")
            //{
            //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
            else if (columnName == "delete")
            {
                if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz inklusive Verknüpfungen löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MySqlConnection con = new MySqlConnection(sql);

                    con.Open();
                    int product_id = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[0].Value);
                    //string deleteProduct = "DELETE FROM inventar where product_id LIKE" + dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                    MySqlCommand commandProduct = new MySqlCommand("DELETE FROM product WHERE product_id=" + Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString()), con);
                    commandProduct.ExecuteNonQuery();
                    con.Close();
                }
            }
            MyInitializeComponent();            //um Update automatisch zu laden

            //string query = "DELETE FROM category where categoryName LIKE" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'";
            //MySqlCommand command = new MySqlCommand(query, con);
            //command.ExecuteNonQuery();
            //con.Close();
            //}
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
