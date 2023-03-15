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
using Ubiety.Dns.Core.Records;

namespace InventarAppIHK
{
    public partial class CategoryInsertForm : Form
    {
        public CategoryInsertForm()
        {
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if(txtCategory.Text == "")
            {
                MessageBox.Show("Bitte geben Sie eine Kategorie an!");
            }
            else
            {
                Category category = new Category(txtCategory.Text.Trim());
                DataImport.InsertCategory(txtCategory.Text);
                MessageBox.Show("Kategorie gespeichert");
            }       
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Category category = new Category(int.Parse(txtId.Text), txtCategory.Text.Trim());
            DataImport.UpdateCategory(category);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataImport.ClearAllFields(this);
        }

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCategory.Text == "")
                {
                    MessageBox.Show("Bitte geben Sie eine Kategorie an!");
                }
                else
                {
                    Category category = new Category(txtCategory.Text.Trim());
                    DataImport.InsertCategory(txtCategory.Text);
                    MessageBox.Show("Kategorie gespeichert");
                }
            }
        }
    }
}
