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
    public partial class LocationForm : Form
    {
        public LocationForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        public void MyInitializeComponent()
        {
            DataImport.LoadFormLocation(dgvLocation);
        }

        private void LocationForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LocationInsertForm insertLocation = new LocationInsertForm();
            insertLocation.btnSave.Enabled = true;
            insertLocation.btnUpdate.Enabled = false;
            insertLocation.ShowDialog();
        }
    }
}
