﻿using InventarAppIHK.Import;
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
    public partial class LocationInsertForm : Form
    {
        public LocationInsertForm()
        {
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
            if(txtfloor.Text != "" && txtRoomName.Text != "")
            {
                Location location = new Location(txtfloor.Text.Trim(), txtRoomName.Text.Trim());
                LocMethods.AddLocation(location);
                this.Close();
            }
            else if (txtfloor.Text == "" && txtRoomName.Text == "")
            {
                MessageBox.Show("Bitte geben Sie einen Ort an!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {          
            Utility.ClearAllFields(this);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Location location = new Location(int.Parse(txtID.Text), txtfloor.Text, txtRoomName.Text);
            LocMethods.UpdateLocation(location);
        }

        private void txtRoomName_KeyDown(object sender, KeyEventArgs e)
        {

           if (btnSave.Enabled == false)
           {
                LocMethods.KeyDownUpdate(btnUpdate, txtID.Text, txtfloor.Text, txtRoomName.Text, e);          
           }
       
            else if (btnUpdate.Enabled == false)
            {
                LocMethods.KeyDownSave(btnUpdate, txtfloor.Text, txtRoomName.Text, e);                     
            }
        }
    }
}
