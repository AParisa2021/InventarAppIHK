using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        public void LoadCSV(string csvFile)
        {
            var query = from l in File.ReadAllLines(csvFile)
                        let data=l.Split(',')
        }
        public class Product
        {
            public string productName { get; set; }
            public DateTime date { get; set; }
            public double price { get; set; }
            public string categoryName { get; set; }
        }
    } 
}
