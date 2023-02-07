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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        public void MyInitializeComponent()
        {
            CSVDataImport.LoadFormProduct(dgvProdukt);

        }

        private void dgvProdukt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
