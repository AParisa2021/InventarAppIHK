using InventarAppIHK.Properties;
using MySql.Data.MySqlClient;
using Mysqlx;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;


namespace InventarAppIHK
{
    public class CatMethods
    {
        /// <summary>
        /// Die Zeilen mit ungültigen Daten, die einen Error verursachen werden ignoriert und die Zeilen mit gültigen Daten
        /// werden in die Tabelle category gespeichert
        /// </summary>
        /// <param name="categoryName"></param>
        //public static void InsertCategory(string categoryName)
        //{

        //    string query = "INSERT INTO category (categoryname) VALUES (@categoryname)";
        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        command.Parameters.Add("categoryname", MySqlDbType.VarChar).Value = categoryName;
        //        command.ExecuteNonQuery();
        //        con.Close();

        //            MessageBox.Show("Kategorie gespeichert");                   

        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message,"Die Kategorie existiert bereits!");
        //    }
        //}

        //The code looks good in terms of functionality, but there are a few minor improvements that can be made to increase readability and maintainability:

        //    Add comments to describe the purpose of the method and each step.
        //    Use the using statement to automatically dispose of resources such as the connection and command objects.
        //Consistently use curly braces for conditional statements and loops, even if they only contain a single statement.
        //Here's the updated code with these improvements:

        // Insert a new category with the given name into the database
        public static void InsertCategory(string categoryName)
        {
            string query = "INSERT INTO category (categoryname) VALUES (@categoryname)";
            try
            {
                using (MySqlConnection con = Utility.GetConnection())
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        // Set the parameter values
                        command.Parameters.Add("categoryname", MySqlDbType.VarChar).Value = categoryName;

                        // Execute the query
                        command.ExecuteNonQuery();

                        // Display a success message
                        MessageBox.Show("Kategorie gespeichert");
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Display an error message if the category already exists
                MessageBox.Show(ex.Message, "Die Kategorie existiert bereits!");
            }
        }


        /// <summary>
        /// holt die jeweilige category_id des jeweiligen categoryName aus der category Tabelle und gibt die categoryId als Rückgabewert zurück
        /// </summary>
        /// <param name = "categoryName" ></ param >
        /// < returns = "categoryId" ></ returns >
        //public static int GetCategoryId(string categoryName)
        //{

        //    string query = "SELECT category_id FROM category WHERE categoryname = @categoryname ";

        //    int categoryId = 0;
        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);

        //        command.Parameters.Add("@categoryname", MySqlDbType.VarChar).Value = categoryName;

        //        categoryId = (int)command.ExecuteScalar();

        //        con.Close();

        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }

        //    return categoryId;
        //}


        //        The code looks good overall.However, I would suggest a few minor improvements:

        //    Parameter naming: I would recommend using camelCase for parameter names, so "categoryName" instead of "categoryname".

        //    Error handling: Instead of just showing the error message in a MessageBox, it's better to log the error somewhere (such as a log file or database) so that it can be analyzed later. You can also re-throw the exception so that it can be caught by higher-level error handling code.

        //Here's the updated code:
        public static int GetCategoryId(string categoryName)
        {
            string query = "SELECT category_id FROM category WHERE categoryName = @categoryName";

            int categoryId = 0;

            try
            {
                using (MySqlConnection con = Utility.GetConnection())
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = categoryName;
                        categoryId = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (MySqlException ex)
            {
                // log the error and re-throw the exception
                LogError(ex);
                throw;
            }

            return categoryId;
        }

        private static void LogError(MySqlException ex)
        {
            // write the error message to a log file or database
            // ...
        }




        //*************************Beginn


        /// <summary>
        /// Den Namen der Kategorie verändert oder erweitern
        /// </summary>
        /// <param name="categoryName"></param>
        /// 


        //In this refactored version, the original method has been split into two: UpdateCategory and SetCommandParameters. The former is responsible for executing the update query, while the latter is responsible for setting the command parameters.

        //SetCommandParameters takes a MySqlCommand object and a Category object as input and sets the parameters of the command object using the properties of the category object. This allows the UpdateCategory method to focus on executing the query and handling exceptions.
        //The refactored version also uses the using statement to ensure that the connection and command objects are properly disposed of when they are no longer needed.
        public static void UpdateCategory(Category category)
    {
        string query = "UPDATE category SET categoryName=@categoryName WHERE category_id=@category_id";
        try
        {
                using (MySqlConnection con = Utility.GetConnection())
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        SetCommandParameters(command, category);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kategorie update");
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void SetCommandParameters(MySqlCommand command, Category category)
        {
            command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = category.GetCategoryId();
            command.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = category.GetCategoryName();
        }






        //public static void UpdateCategory(Category category)
        //{
        //    string query = "UPDATE category SET categoryName=@categoryName WHERE category_id=@category_id";
        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = category.GetCategoryId();
        //        command.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = category.GetCategoryName();

        //        command.ExecuteNonQuery();
        //        MessageBox.Show("Kategorie update");

        //        MySqlDataReader da = command.ExecuteReader();

        //        con.Close();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        /// <summary>
        ///  Im DataGridView im CategoryForm kann der User auf edit oder delete drücken. Um einen Datensatz zu löschen oder um 
        ///  das CategorynsertForm zu öffnen um den Datensatz zu ändern
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        //public static void CategoryEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        //{
        //    MySqlConnection con = Utility.GetConnection();

        //    string columnName = "";
        //    columnName = dgv.Columns[e.ColumnIndex].Name;
        //    if (columnName == "edit")
        //    {
        //        CategoryInsertForm catInsert = new CategoryInsertForm();
        //        catInsert.btnSave.Enabled = false;
        //        catInsert.txtId.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
        //        catInsert.txtCategory.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
        //        catInsert.Text = "Update";

        //        catInsert.ShowDialog();
        //    }
        //    else if (columnName == "delete")
        //    {
        //        if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", 
        //            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                MySqlCommand comm = new MySqlCommand("DELETE FROM category WHERE category_id= '" 
        //                    + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); 

        //                comm.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Löschung nicht möglich!");
        //            }
        //        }
        //    }

        //    con.Close();
        //}




        //**************************EditDelete**Begin

        public static void EditCategory(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            MySqlConnection con = Utility.GetConnection();

            CategoryInsertForm catInsert = new CategoryInsertForm();
            catInsert.btnSave.Enabled = false;
            catInsert.txtId.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            catInsert.txtCategory.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            catInsert.Text = "Update";

            catInsert.ShowDialog();

            con.Close();
        }

        public static void DeleteCategory(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            MySqlConnection con = Utility.GetConnection();

            if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand("DELETE FROM category WHERE category_id= '"
                        + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con);

                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Löschung nicht möglich!");
                }
            }

            con.Close();
        }

        public static void ProcessDataGridViewClick(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            if (columnName == "edit")
            {
                EditCategory(dgv, e);
            }
            else if (columnName == "delete")
            {
                DeleteCategory(dgv, e);
            }
        }


        //***********************Beginn LoadFormCategory**************

        /// <summary>
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        /// 


        public static void LoadFormCategory(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string query = "select * from category";

            try
            {
                using (MySqlConnection con = Utility.GetConnection())
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                PopulateDataGridView(dgv, dr);
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Error");
            }
        }

        private static void PopulateDataGridView(DataGridView dgv, MySqlDataReader dr)
        {
            while (dr.Read())
            {
                string categoryID = dr[0].ToString();
                string categoryName = dr[1].ToString();
                AddRowToDataGridView(dgv, categoryID, categoryName);
            }
        }

        private static void AddRowToDataGridView(DataGridView dgv, string categoryID, string categoryName)
        {
            dgv.Rows.Add(categoryID, categoryName);
        }







        //public static void LoadFormCategory(DataGridView dgv)              //update datagridview mit einem Button updaten
        //{
        //    dgv.Rows.Clear();
        //    string query = "select * from category";
        //    try
        //    {
        //        MySqlConnection con = Utility.GetConnection();
        //        MySqlCommand command = new MySqlCommand(query, con);
        //        MySqlDataReader dr = command.ExecuteReader();
        //        int i = 0;
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                i++;        //wird noch nicht verwendet
        //                dgv.Rows.Add(dr[0].ToString(), dr[1].ToString());
        //            }
        //        }
        //        command.Dispose();
        //        con.Close();
        //        dr.Close();
        //    }
        //    catch (MySqlException e)
        //    {
        //        MessageBox.Show(e.Message + "Error");
        //    }
    }
}

