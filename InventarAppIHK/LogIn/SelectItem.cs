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
        private static bool startButtonWasClicked = false;

        public string SelectData(string userInsert, string passwordInsert)
        {
            try
            {
                DBConnection.DataSource();
                conn.connOpen();
                MySqlCommand command= new MySqlCommand();
                command.CommandText = ("Select * from login where (username, Password) = (@username, @Password)");
                command.Parameters.AddWithValue("@username", userInsert);
                command.Parameters.AddWithValue("@Password", Encrypt.HashValue(passwordInsert));
                command.Connection = DBConnection.connMaster;
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    MessageBox.Show("Sie haben sich erfolgreich eingeloggt!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                        MainForm openMainForm = new MainForm();
                        openMainForm.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Ihre Eingabedaten sind nicht korrekt oder Sie sind noch nicht registriert!");
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

        //***************************************LogIn Form schließt nicht
        public static void Close()
        {
            startButtonWasClicked = true;
            if (startButtonWasClicked)
            {
                LoginForm frm = new LoginForm();
                frm.Hide();
            }
        }
        public static void CloseForm()
        {
        }
    }
}
