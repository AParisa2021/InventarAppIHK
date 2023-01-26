using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace InventarAppIHK
{
    public class DBConnection
    {
        static string server = "datasource=localhost;port=3306;username=root;password=;database=inventar";

        public static MySqlConnection connMaster;

        public static MySqlConnection DataSource()
        {
            connMaster = new MySqlConnection(server);
            return connMaster;
        }

        public void connOpen()
        {
            DataSource();
            connMaster.Open();
        }

        public void connClose()
        {
            DataSource();
            connMaster.Close();
        }

    }
}
