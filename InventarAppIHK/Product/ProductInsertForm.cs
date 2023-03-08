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
using InventarAppIHK.Import;    //statische Methode implementiert die Datei in die Klasse ProductInsertForm

namespace InventarAppIHK
{
    public partial class ProductInsertForm : Form
    {
        static string conn = "datasource=localhost;port=3306;username=root;password=;database=inventar";

        public ProductInsertForm()
        {
            InitializeComponent();
            MyInitializeComponent();
        }
        public void MyInitializeComponent()
        {
            FillComboBox();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }


        /// <summary>
        /// Methode erhält aus der Klasse CSVDataImport mit der Methode GetCategoryId die category_id aus der Datenbank
        /// Sie gibt als Rückgabewert die entsprechende category_id aus und speichert sie in eine Variable
        /// Mit comboCategory wähle ich das entsprechende Produkt aus und übergebe der Methode die comboCategory als Parameter
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int category_id = DataImport.GetCategoryId(comboCategory.Text);
            Product insertProduct = new Product(txtName.Text, (DateTime.Parse(dtOrder.Text.Substring(0, 10))), double.Parse(DataImport.NullStringDatabase( txtPrice.Text).ToString()), category_id);
            DataImport.InsertProduct(insertProduct);
            MessageBox.Show("Ihre Auswahl wurde eingetragen!");


          
        }

        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void FillComboBox()
        {
            {
                comboCategory.Items.Clear();
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");
                string sql = "select categoryName from category";
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader dr = comm.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;
                        comboCategory.Items.Add(dr[0].ToString());
                    }
                }
                comm.Dispose();
                conn.Close();
                dr.Close();


                //comboCategory.Items.Clear();
                //string select = "SELECT * FROM category";
                //    MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=inventar");

                //    conn.Open();

                //    MySqlCommand cmd = new MySqlCommand(select, conn);
                //    MessageBox.Show(select);

                //    try
                //    {

                //        MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                //        DataSet ds = new DataSet();
                //        adap.Fill(ds);
                //        cmd.ExecuteNonQuery();
                //        conn.Close();
                //        comboCategory.DataSource = ds.Tables[0];
                //        comboCategory.DisplayMember = "categoryName";
                //        comboCategory.ValueMember = "category_id";
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                dtOrder.CustomFormat = " ";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int category_id = DataImport.GetCategoryId(comboCategory.Text);
            Product updateProduct = new Product(int.Parse(txtID.Text), txtName.Text, DateTime.Parse(dtOrder.Text), double.Parse(txtPrice.Text), category_id);
            DataImport.UpdateProduct(updateProduct);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataImport.ClearAllText(this);
        }
    }
}
