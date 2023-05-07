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
        Encrypt encrypt = new Encrypt();

        public string InsertData(string userInsert,string passwordInsert, string insertDateTime) 
        {
            try
            {            
                string query = "INSERT INTO login (username, Password, Registered) values (@username, @Password, @datetime)";
                MySqlConnection conn = Utility.GetConnection();
                MySqlCommand command= new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", userInsert);
                command.Parameters.AddWithValue("@Password", Encrypt.HashValue(passwordInsert));
                command.Parameters.AddWithValue("@datetime", insertDateTime);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Account angelegt", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return userInsert + passwordInsert + insertDateTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }         
        }
    }
}
