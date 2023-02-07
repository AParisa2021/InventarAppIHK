using InventarAppIHK.Import;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            CSVDataImport.LoadFormCategory(dgvCategory);
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewImageColumn img = new DataGridViewImageColumn();
            //Image image = Image.FromFile("C:\\Users\\paris\\source\\repos\\IHK\\InventarAppIHK\\Resources\\MiniBleispift.png");
            //img.Image = image;
            //dgvCategory.Columns.Add(img);
            //img.HeaderText = "Bearbeiten";
            //img.Name = "MiniBleispift.png";

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
