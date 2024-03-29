﻿using InventarAppIHK.Import;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
          
            if(txtCategory.Text != "")
            {
                Category category = new Category(txtCategory.Text.Trim());
                CatMethods.InsertCategory(txtCategory.Text.ToUpper().Trim());
                Close();                                                                      //klappt nicht. fenster schließt nicht automatisch
            }
            else if (txtCategory.Text == "")
            {
                MessageBox.Show("Bitte geben Sie eine Kategorie an!");
            }
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Category category = new Category(int.Parse(txtId.Text), txtCategory.Text.Trim());
            CatMethods.UpdateCategory(category);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Utility.ClearAllFields(this);
        }

      
            private void txtCategory_KeyDown(object sender, KeyEventArgs e)
            {

            //DataImport.KeyEnter(txtCategory.Text, btnUpdate.Text, btnSave.Text, e, code, category)

                if (btnUpdate.Enabled == false)
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
                            CatMethods.InsertCategory(txtCategory.Text);
                        }
                    }              

                }     
                else if (btnSave.Enabled == false)            
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (txtCategory.Text == "")
                        {
                            MessageBox.Show("Bitte geben Sie eine Kategorie an!");
                        }
                        else
                        {
                        Category category = new Category(int.Parse(txtId.Text), txtCategory.Text.Trim());
                        CatMethods.UpdateCategory(category);
                        }
                    }
                }

        }
    }
}
