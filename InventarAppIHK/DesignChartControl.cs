using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace InventarAppIHK
{
    internal class DesignChartControl
    {
        public static void FillChart(Chart chart1)
        {
            MySqlConnection con = Utility.GetConnection();

            string query = "select price, productName from product"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            MySqlCommand command = new MySqlCommand(query, con);

      

            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet dataset = new DataSet();
            da.Fill(dataset);

            chart1.DataSource = dataset;
            chart1.Series[0].XValueMember = "productName";
            chart1.Series[0].YValueMembers = "price";
            // Insert code for additional chart formatting here.
            chart1.DataBind();
            con.Close();
        }
        public static void Location(Chart chart1)
        {
            MySqlConnection con = Utility.GetConnection();

            string query = "select floor, locationName from location"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            MySqlCommand command = new MySqlCommand(query, con);



            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet dataset = new DataSet();
            da.Fill(dataset);

            chart1.DataSource = dataset;
            chart1.Series[0].XValueMember = "floor";
            chart1.Series[0].YValueMembers = "locationName";
            // Insert code for additional chart formatting here.
            chart1.DataBind();
            con.Close();
        }
    }
}
