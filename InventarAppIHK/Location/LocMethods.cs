﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public class LocMethods
    {
        /// <summary>
        /// Ort, an dem sich das Inventar befindet in die Datenbank hinzufügen und SQL injection verhindern
        /// </summary>
        /// <param name="location"></param>
        //public static void AddLocation(Location location)
        //{
        //    string insert = "INSERT INTO location(floor, locationName) VALUES (@floor, @locationName)";
        //    MySqlConnection con = Utility.GetConnection();
        //    MySqlCommand cmd = new MySqlCommand(insert, con);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.GetFloor();
        //    cmd.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.GetLocationName();

        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Ort hinzugefügt");
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ort existiert bereits!");
        //        con.Close();
        //    }
        //}


        //************************Beginn AddLocation

        //Methode in zwei Teile aufteilen, eine zum Einfügen der Daten und eine andere, um die Ausnahme
        //abzufangen und eine Meldung anzuzeigen. Hier ist eine Möglichkeit, dies zu tun:
        public static void InsertLocation(Location location)
        {
            string insert = "INSERT INTO location(floor, locationName) VALUES (@floor, @locationName)";
            MySqlConnection con = Utility.GetConnection();
            MySqlCommand cmd = new MySqlCommand(insert, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.GetFloor();
            cmd.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.GetLocationName();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static bool AddLocation(Location location)
        {
            try
            {
                InsertLocation(location);
                MessageBox.Show("Ort hinzugefügt");
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ort existiert bereits!");
                return false;
            }
        }






        /// <summary>
        /// holt die jeweilige location_id des jeweiligen locationName aus der category Tabelle und gibt die LocationId als Rückgabewert zurück
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="locationName"></param>
        /// <returns="locationId"></returns>
        public static int GetLocationId(string locationName)
        {
            MySqlConnection con = Utility.GetConnection();
            string query = "SELECT location_id FROM location WHERE locationName = @locationName";

            int locationId = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    try
                    {
                        command.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = locationName;

                        locationId = (int)command.ExecuteScalar();
                    }
                    catch (MySqlException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            con.Close();

            return locationId;
        }
       
        
        
        /// <summary>
        /// Mit der TextBox einen Datensatz im DataGridView filtern
        /// </summary>
        /// <param name="search_location"></param>
        public static void FilterTxtLocation(Location locationName)
        {
            DataGridView dgv = new DataGridView();
            dgv.Rows.Clear();
            TextBox txt = new TextBox();
            string query = "select location_id, floor, locationName from location where concat(@location_id, @floor, @locationName) like '%' || @search_location || '%'"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter

            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader dr = command.ExecuteReader();
                command.Parameters.AddWithValue("@search_location", MySqlDbType.VarChar).Value = locationName;

                int i = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        i++;        //Zählt Nummer der Customer. wird noch nicht verwendet aufgrund von Verschiebungen im DataGridView. SPäter prüfen, ob es notwendig ist
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
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

        /// <summary>
        /// Verändert oder Erweitert mit Update die Etage den Namen des Ortes
        /// </summary>
        /// <param name="location"></param>
        public static void UpdateLocation(Location location)
        {
            string query = "UPDATE location SET floor=@floor, locationName=@locationName WHERE location_id=@location_id";
            MySqlConnection con = Utility.GetConnection();
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.Add("@location_id", MySqlDbType.VarChar).Value = location.GetLocationID();
            command.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.GetFloor();
            command.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.GetLocationName();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Ort update");
                MySqlDataReader da = command.ExecuteReader();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Ort nicht hinzugefügt!");
                con.Close();
            }
        }

        /// <summary>
        /// Lädt User aus der Datenbank Tabelle category & gibt die jeweils in den einzelnen Zeilen des dgvCategory aus
        /// </summary>
        public static void LoadFormLocation(DataGridView dgv)              //update datagridview mit einem Button updaten
        {
            dgv.Rows.Clear();
            string query = "select * from location";
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
                        dgv.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
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

       

        /// <summary>
        /// das DataGridView hat die Funktionen edit und delete. Wenn delete gewählt wird, wird der Datensatz aus der DB Tabelle und dgv gelöscht
        /// Bei edit öffnet sich die LocationInsertForm und man kann ein Update machen
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        //public static void LocationEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        //{
        //    MySqlConnection con = Utility.GetConnection();
        //    string columnName = "";
        //    columnName = dgv.Columns[e.ColumnIndex].Name;
        //    if (columnName == "Edit")
        //    {
        //        LocationInsertForm locInsert = new LocationInsertForm();
        //        locInsert.btnSave.Enabled = false;
        //        locInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
        //        locInsert.txtfloor.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
        //        locInsert.txtRoomName.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
        //        locInsert.Text = "Update";

        //        locInsert.ShowDialog();
        //    }
        //    else if (columnName == "Delete")
        //    {
        //        if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                MySqlCommand comm = new MySqlCommand("DELETE FROM location WHERE location_id= '" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

        //                comm.ExecuteNonQuery();
        //            }
        //            catch (Exception ex) { MessageBox.Show("Löschung nicht möglich!"); }
        //        }
        //    }
        //    con.Close();
        //}


        //**********************Beginn LocationEditDelete


        // ID des ausgewählten Datensatzes aus dem DataGridView zu erhalten:
        private static int GetSelectedLocationId(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            return Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString());
        }


        //Dann könnte man eine Methode erstellen, die die Bearbeitung des ausgewählten Datensatzes in einem separaten Formular durchführt:
        private static void EditLocation(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            LocationInsertForm locInsert = new LocationInsertForm();
            locInsert.btnSave.Enabled = false;
            locInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            locInsert.txtfloor.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            locInsert.txtRoomName.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            locInsert.Text = "Update";

            locInsert.ShowDialog();
        }


        //Und schließlich könnte man eine Methode erstellen, die den ausgewählten Datensatz löscht:
        private static void DeleteLocation(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int locationId = GetSelectedLocationId(dgv, e);
                    MySqlConnection con = Utility.GetConnection();
                    MySqlCommand comm = new MySqlCommand("DELETE FROM location WHERE location_id= '" + locationId + "'", con);
                    comm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { MessageBox.Show("Löschung nicht möglich!"); }
            }
        }


        //Schließlich könnte man die LocationEditDelete-Methode wie folgt ändern, um diese separaten Methoden zu verwenden:
        public static void LocationEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            if (columnName == "Edit")
            {
                EditLocation(dgv, e);
            }
            else if (columnName == "Delete")
            {
                DeleteLocation(dgv, e);
            }
        }




        /// <summary>
        /// KeyPress Methode: Beim drücken der Enter Taste wird location gespeichert.
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="textBox1"></param>
        /// <param name="textBox2"></param>
        /// <param name="e"></param>
        public static void KeyDownSave(Button btn, string textBox1, string textBox2, KeyEventArgs e)
        {

            if (btn.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (textBox1 == "" && textBox2 == "")
                    {
                        MessageBox.Show("Bitte geben Sie einen Ort an!");
                    }
                    else
                    {
                        Location location = new Location(textBox1.Trim(), textBox2.Trim());
                        LocMethods.AddLocation(location);
                    }
                }

            }
        }
        /// <summary>
        /// KeyPress Methode: Beim drücken der Enter Taste wird location geändert mit der update Methode.
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="textBox1"></param>
        /// <param name="textBox2"></param>
        /// <param name="textBox3"></param>
        /// <param name="e"></param>
        public static void KeyDownUpdate(Button btn, string textBox1, string textBox2, string textBox3, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (textBox1 == "" && textBox2 == "")
                    {
                        MessageBox.Show("Bitte geben Sie einen Ort an!");
                    }
                    else
                    {
                        Location location = new Location(int.Parse(textBox1), textBox2, textBox3);
                        LocMethods.UpdateLocation(location);
                    }
                }
            }
           catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ort existiert bereits!");
            }

        }
    }
}
