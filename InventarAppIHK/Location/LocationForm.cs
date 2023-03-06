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

namespace InventarAppIHK
{
    public partial class LocationForm : Form
    {
        private MySqlCommand comm;

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
            MyInitializeComponent();    //Laden von dgv nach einem neuen Eintrag

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dgvLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=inventar";

            //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "";
            MySqlConnection con = new MySqlConnection(sql);
            //int category_id = DataImport.GetCategoryId(CategoryInsertForm);

            string columnName = "";
            columnName = dgvLocation.Columns[e.ColumnIndex].Name;
            con.Open();
            if (columnName == "Edit")
            {
                LocationInsertForm locInsert = new LocationInsertForm();
                locInsert.txtID.Text = dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString();
                locInsert.txtfloor.Text = dgvLocation.Rows[e.RowIndex].Cells[1].Value.ToString();
                locInsert.txtRoomName.Text = dgvLocation.Rows[e.RowIndex].Cells[2].Value.ToString();

                locInsert.ShowDialog();
            }
            else if (columnName == "Delete")
            {
                //string query = "DELETE FROM category where categoryName LIKE" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();

                //MySqlCommand command = new MySqlCommand(query, con);
                comm = new MySqlCommand("DELETE FROM location WHERE location_id= '" + Convert.ToInt32(dgvLocation.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

                comm.ExecuteNonQuery();
            }
            MyInitializeComponent();        //laden dgv nach update
            con.Close();
        }
    }
}
