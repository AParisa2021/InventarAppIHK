using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static void AddLocation(Location location)
        {
            string insert = "INSERT INTO location(floor, locationName) VALUES (@floor, @locationName)";
            MySqlConnection con = Utility.GetConnection();
            MySqlCommand cmd = new MySqlCommand(insert, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.Floor;
            cmd.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.LocationName;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ort hinzugefügt");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ort nicht hinzugefügt!");
            }
            con.Close();
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
            try
            {
                MySqlConnection con = Utility.GetConnection();
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.Add("@location_id", MySqlDbType.VarChar).Value = location.LocationID;
                command.Parameters.Add("@floor", MySqlDbType.VarChar).Value = location.Floor;
                command.Parameters.Add("@locationName", MySqlDbType.VarChar).Value = location.LocationName;
                command.ExecuteNonQuery();
                MessageBox.Show("Ort update");

                MySqlDataReader da = command.ExecuteReader();

                con.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message + "Ort nicht hinzugefügt!");
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
        public static void LocationEditDelete(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            MySqlConnection con = Utility.GetConnection();
            string columnName = "";
            columnName = dgv.Columns[e.ColumnIndex].Name;
            if (columnName == "Edit")
            {
                LocationInsertForm locInsert = new LocationInsertForm();
                locInsert.btnSave.Enabled = false;
                locInsert.txtID.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                locInsert.txtfloor.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                locInsert.txtRoomName.Text = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();

                locInsert.ShowDialog();
            }
            else if (columnName == "Delete")
            {
                if (MessageBox.Show("Sind sie sich sicher, dass Sie diesen Datensatz löschen möchten?", "Löschen Datensatz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        MySqlCommand comm = new MySqlCommand("DELETE FROM location WHERE location_id= '" + Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value.ToString()) + "'", con); //prüfen ob [1] stimmt [4]phone ist in postgreSQL bei properties auf allow null gesetzt

                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show("Löschung nicht möglich!"); }
                }
            }
            con.Close();
        }
    }
}
