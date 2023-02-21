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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventarAppIHK
{
    public partial class CategoryForm : Form
    {
        private MySqlCommand comm;

        public CategoryForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        public void MyInitializeComponent()
        {
            DataImport.LoadFormCategory(dgvCategory);
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
     
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "";
            MySqlConnection con = new MySqlConnection(sql);
            //int category_id = DataImport.GetCategoryId(CategoryInsertForm);

            string columnName = "";
            columnName = dgvCategory.Columns[e.ColumnIndex].Name;
            con.Open();
            if (columnName == "edit")
            {
                CategoryInsertForm catInsert = new CategoryInsertForm();
                catInsert.txtId.Text = dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString();

                catInsert.txtCategory.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                catInsert.ShowDialog();
            }
            else if (columnName == "delete")
            {
                //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();

                //MySqlCommand command = new MySqlCommand(query, con);
                comm = new MySqlCommand("DELETE FROM category WHERE category_id= '" + Convert.ToInt32(dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

                comm.ExecuteNonQuery();
            }
            con.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryInsertForm insertCategory = new CategoryInsertForm();
            insertCategory.btnSave.Enabled = true;
            insertCategory.btnUpdate.Enabled = false;
            insertCategory.ShowDialog();
        }
    }
}


//erst in der Verknüfungstabelle löschen z.B. delete From inventar where product_id=5
//Delete From product where product_id=5; dann in der Haupttabelle
//; 