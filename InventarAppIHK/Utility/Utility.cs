using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public class Utility
    {
        /// <summary>
        /// Herstellung der Verbindung mit der Datenbank MYSQL
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=Parisa;password=Inventar2023;database=inventarapp";

            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Connection Error" + ex.Message);
            }
            return con;
        }

        /// <summary>
        /// wenn die comboBox leer oder null ist, gibt er einen leeren string zurück, sonst den Wert. So wird Programmfehler verindert
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public static string formatCategoryId(string valueString)
        {
            if (valueString == "" || valueString == null) return "";

            return (valueString);
        }

        /// <summary>
        /// Wenn im Price-TextBox nicht eingetragen wird, wird 0 gespeichert. Darüberhinaus wandelt diese Methode ein Punkt in ein string um,
        /// um keinen Error mit der Datenbank zu erhaltenn. sollte die TextBox leer sein oder null gibt er n0 zurück
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public static double formatDouble(string valueString)
        {
            if (valueString == "" || valueString == null) return 0;

            return price(double.Parse(valueString.Replace(".", ",")));
        }

        /// <summary>
        /// Runden auf zwei Stellen nach dem Komma
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public static double price(double valueString)
        {
            
                valueString = Math.Round(valueString, 2);
            
                return valueString;
        }

        /// <summary>
        /// Nachdem Button edit gedrück wurde, löscht es zum Bearbeiten bei Button.Click alle TextBoxen außer die Id TextBoxen, um bei der Bearbeitunf
        /// das richtige Product zu ändern. In der Schleife durchläuft jedes Controll Element. wenn der Name nicht den angegebenen c.Name(ID TextBoxen) 
        /// entspricht, leert er die TextBoxe, so durchläuft er die Schleife für jeden Element und prüft, ob sie gelöscht wird
        /// in der if Bedingung, wenn es eine TextBox mit einer ID ist, springt er raus aus der Bedingung und geht nicht in den else Teil
        /// The Control class implements very basic functionality required by classes that display information to the user. 
        /// It handles user input through the keyboard and pointing devices. It handles message routing and security. It defines the 
        /// bounds of a control (its position and size), although it does not implement painting. It provides a window handle
        /// </summary>
        /// <param name="con"></param>
        public static void ClearAllFields(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox && c != null)
                {
                    if (/*c.Name != "txtInventarId" &&*/ c.Name != "txtCatID" && c.Name != "txtLocationID" &&
                        c.Name != "txtProductID" && c.Name != "txtId" && c.Name != "txtID") // just put in the name of special textBoxes between double quotes
                        c.Text = "";    //wenn eine TextBox durchlaufen wird springt er in den If Teil, wenn die Bedingung erfüllt ist leer er TXT
                }
                else
                {
                    ClearAllFields(c);      //bei allen anderen außer TextBox Button Label geht er (ZUSÄTZLICH) in den else Teil
                }
            }

        }

        /// <summary>
        /// Damit bei einem leeren Datumseintrag das Programm nicht abstürzt, stattdessen in der DataGridView Tabelle ein - als Zeichen für leer bzw. Datum nicht vorhanden ausgibt
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private string NullProblemDatabase(string datetime)
        {
            if (datetime == "") return "-";
            else return Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Umwandlung des Formats in Datetime Jahr-Monat-Tag
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string Datetime(string datetime)
        {
            string timeFormat = Convert.ToDateTime(datetime.ToString()).ToString("yyyy-MM-dd");
            return timeFormat;
        }

        /// <summary>
        /// Diese Methode dienst zur Formatierung. setzt hinter Preisen ohne Kommastellen ,00 hinter. 
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string PriceFormat(string price)
        {
            if (!price.Contains(","))
            {
                return price + ",00";
            }
            else
            {
                return price ;
            }
        }
    }
}
