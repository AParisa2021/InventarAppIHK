using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InventarAppIHK
{
    public partial class LoginForm : Form
    {

        insertData insertdata = new insertData();
        DBConnection conn = new DBConnection();
        SelectItem selectItem = new SelectItem();
        public LoginForm()
        {
            InitializeComponent();            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
            if (txtUser.Text.Length < 3 || txtPassword.Text.Length < 3)
            {
                MessageBox.Show("Benutzername oder Passwort sind zu kurz!");
            }
            else
            {
                insertdata.InsertData(txtUser.Text, txtPassword.Text, time);
                MessageBox.Show("Sind haben sich erfolgreich registriert");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // //Login Attempts counter
            //int loginAttempts = 0;

            ////Simple iteration upto three times
            //for (int i = 0; i < 3; i++)
            //{
            //    if (loginAttempts > 2)
            //        MessageBox.Show("Bitte Admin");
            //    //MessageBox.Show("Bitte Benutzername und Passwort eingeben!");

            //    if (txtUser.Text == "" && txtPassword.Text == "")       //Fehler denn wenn er eine Falscheingabe macht geht er in den nächsten if teil
            //    {                                                       //loginAttempts zählt er nur 3 wenn keine Eingabe erfolt und nicht bei einer falschen Eingabe
            //        loginAttempts+=i;
            //    }
              

            //}

            ////Display the result
            //if (loginAttempts > 2)
            //    MessageBox.Show("Bitte Admin");
            //else
            //{
            //    selectItem.SelectData(txtUser.Text, txtPassword.Text);
            //    this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //    //this.Close();
            //}

            if (txtUser.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("Bitte registrieren");
            }
            else if (txtUser.Text != "" && txtPassword.Text != "")
            {
            
                selectItem.SelectData(txtUser.Text, txtPassword.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte Benutzername und Passwort eingeben!");
            }
         
        }
      
     

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Anzeige Passwort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnLogin_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
//Omar Test2? Username und Password  Parisa Test3!