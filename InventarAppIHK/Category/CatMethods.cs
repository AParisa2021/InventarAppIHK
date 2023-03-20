using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlCommand = MySqlConnector.MySqlCommand;
using MySqlConnection = MySqlConnector.MySqlConnection;
using MySqlDataReader = MySqlConnector.MySqlDataReader;
using MySqlDbType = MySqlConnector.MySqlDbType;
using MySqlException = MySqlConnector.MySqlException;

namespace InventarAppIHK
{
    public class CatMethods
    {
        /// <summary>
        /// Die Zeilen mit ungültigen Daten, die einen Error verursachen werden ignoriert und die Zeilen mit gültigen Daten
        /// werden in die Tabelle category gespeichert
        /// </summary>
        /// <param name="categoryName"></param>
        public static void InsertCategory(string categoryName)
        {

            string query = "INSERT IGNORE INTO category (categoryname) VALUES (@categoryname)";
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("categoryname", MySqlDbType.VarChar).Value = categoryName;
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// holt die jeweilige category_id des jeweiligen categoryName aus der category Tabelle und gibt die categoryId als Rückgabewert zurück
        /// </summary>
        /// <param name = "categoryName" ></ param >
        /// < returns = "categoryId" ></ returns >
        public static int GetCategoryId(string categoryName)
        {

            string query = "SELECT category_id FROM category WHERE categoryname = @categoryname ";

            int categoryId = 0;
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);

                command.Parameters.Add("@categoryname", MySqlDbType.VarChar).Value = categoryName;

                categoryId = (int)command.ExecuteScalar();

                con.Close();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return categoryId;
        }

        /// <summary>
        /// Den Namen der Kategorie verändert oder erweitern
        /// </summary>
        /// <param name="categoryName"></param>
        public static void UpdateCategory(Category category)
        {
            string query = "UPDATE category SET categoryName=@categoryName WHERE category_id=@category_id";
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = category.CategoryID;
                command.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = category.CategoryName;

                command.ExecuteNonQuery();
                MessageBox.Show("Kategorie update");

                MySqlDataReader da = command.ExecuteReader();

                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        ///  Im DataGridView im CategoryForm kann der User auf edit oder delete drücken. Um einen Datensatz zu löschen oder um das CategorynsertForm zu öffnen um den Datensatz zu ändern

        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        public static void CategoryEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {

            MySqlConnection con = Utility.GetConnection();

            string columnName = "";
            columnName = dgv.Columns[e.ColumnIndex].Name;
            if (columnName == "edit")
            {
                CategoryInsertForm catInsert = new CategoryInsertForm();
                catInsert.btnSave.Enabled = false;
                catInsert.txtId.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                catInsert.txtCategory.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                catInsert.ShowDialog();
            }
            else if (columnName == "delete")
            {
                if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        MySqlCommand comm = new MySqlCommand("DELETE FROM category WHERE category_id= '" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Löschung nicht möglich!");
                    }
                }
            }

            con.Close();
        }

        /// <summary>
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        public static void LoadFormCategory(DataGridView dgv)              //update datagridview mit einem Button updaten
        {
            dgv.Rows.Clear();
            string query = "select * from category";
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //wird noch nicht verwendet
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString());
                    }
                }
                command.Dispose();
                con.Close();
                dr.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }
    }
}
