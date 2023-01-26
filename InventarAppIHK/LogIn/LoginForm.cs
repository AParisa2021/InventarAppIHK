﻿using System;
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
            Import.CSVDataImport.ImportData();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (txtUser.Text.Length < 3 || txtPassword.Text.Length < 3)
                MessageBox.Show("Benutzername oder Passwort sind zu kurz!", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            insertdata.InsertData(txtUser.Text, txtPassword.Text, time);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            selectItem.SelectData(txtUser.Text, txtPassword.Text);        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

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
    }
}
//Omar Test2? Username und Password  Parisa Test3!