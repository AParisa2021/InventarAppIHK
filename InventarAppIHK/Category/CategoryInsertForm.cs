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
            Category category = new Category(txtCategory.Text.Trim());
            DBInventar.AddCategory(category);
        }
    }
}
