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
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventarAppIHK
{
    public partial class CategoryForm : Form
    {

        public CategoryForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        public void MyInitializeComponent()
        {
            CatMethods.LoadFormCategory(dgvCategory);
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatMethods.CategoryEditDelete(dgvCategory, e);
            MyInitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryInsertForm insertCategory = new CategoryInsertForm();
            insertCategory.btnSave.Enabled = true;
            insertCategory.btnUpdate.Enabled = false;
            insertCategory.ShowDialog();
            MyInitializeComponent();
        }
    }
}


//erst in der Verknüfungstabelle löschen z.B. delete From inventar where product_id=5
//Delete From product where product_id=5; dann in der Haupttabelle
//; 