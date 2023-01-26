using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InventarAppIHK
{
    public class insertData
    {
        DBConnection conn = new DBConnection();
        Encrypt encrypt = new Encrypt();

        public string InsertData(string userInsert,string passwordInsert, string insertDateTime) 
        {
            try
            {
                DBConnection.DataSource();
                conn.connOpen();
                MySqlCommand command= new MySqlCommand();
                command.CommandText = "INSERT INTO login (username, Password, Registered) values (@username, @Password, @datetime)";
                command.Parameters.AddWithValue("@username", userInsert);
                command.Parameters.AddWithValue("@Password", Encrypt.HashString(passwordInsert));
                command.Parameters.AddWithValue("@datetime", insertDateTime);
                command.Connection = DBConnection.connMaster;
                command.ExecuteNonQuery();
                MessageBox.Show("Account angelegt", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.connClose();
                return userInsert + passwordInsert + insertDateTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally 
            {
                conn.connClose();
            }
        }
    }
}
