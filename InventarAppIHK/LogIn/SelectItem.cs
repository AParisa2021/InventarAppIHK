using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InventarAppIHK
{
    public class SelectItem
    {
        DBConnection conn =new DBConnection();
        public string SelectData(string userInsert, string passwordInsert)
        {
            try
            {
                DBConnection.DataSource();
                conn.connOpen();
                MySqlCommand command= new MySqlCommand();
                command.CommandText = ("Select * from login where (username, Password) = (@username, @Password)");
                command.Parameters.AddWithValue("@username", userInsert);
                command.Parameters.AddWithValue("@Password", Encrypt.HashString(passwordInsert));
                command.Connection = DBConnection.connMaster;
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    MessageBox.Show("Sie haben sich erfolgreich eingeloggt!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                        MainForm openMainForm = new MainForm();
                        openMainForm.ShowDialog();                      
                }
                else
                {
                    MessageBox.Show("Fehler!", "Information", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                return userInsert + passwordInsert;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }            
            
            finally
            {
                conn.connClose();
            }

        }
    }
}
