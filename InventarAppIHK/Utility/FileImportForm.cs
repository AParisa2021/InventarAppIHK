using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventarAppIHK.Import;

namespace InventarAppIHK
{
    public partial class FileImportForm : Form
    {
        public StreamReader file;
        public FileImportForm()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            DataImport.ImportData(file);
        }

        public void SetText(string text)
        {
            txtSelect.Text = text;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    file = new StreamReader(openFileDialog1.FileName);
                    SetText(openFileDialog1.FileName);
                }
                catch(SecurityException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
