using InventarAppIHK.SelectInventar;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;


namespace InventarAppIHK.Import
{
    class DataImport
    {
        /// <summary>
        /// Importiert Datensätze aus einer CSV Datei. Mit InsertCategory Methode speichert er den Kategorienamen (Index 3 in der CSV Datei)
        /// in die Datenbank-Tabelle category ein
        /// Mit der Methode InsertProduct speichert er den gesamten Datensatz jeweils in der richtigen Datenbank-Tabellenreihenfolge ein
        /// </summary>
        /// <param name="reader"></param>
        public static void ImportData(StreamReader reader)
        {
            using (reader)
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    CatMethods.InsertCategory(values[3]);
                    ProdMethods.InsertProduct(values[1].ToString(), values[2].ToString(), double.Parse(values[4].Replace(".", ",")), values[3].ToString());
                }
            }
        }
       
    }
}
