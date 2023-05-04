using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarAppIHK
{
    public partial class ChartControl : Form
    {
        public ChartControl()
        {
            InitializeComponent();
            //FillChart();
         
        }

        private void FillChart()
        {
            MySqlConnection con = Utility.GetConnection();

            string query = "select price, productName from product"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            MySqlCommand command = new MySqlCommand(query, con);

            //MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //chart1.DataSource = dt;
            //chart1.Series["productName"].XValueMember = "productName";
            ////chart1.Series["price"].YValueMembers = "price";
            //chart1.Titles.Add("Inventar");
            ////MessageBox.Show(chart1.ToString());

            //con.Close();

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

        private void ChartControl_Load(object sender, EventArgs e)
        {
            FillChart();
            MySqlConnection con = Utility.GetConnection();

            //dataGridView1.DataSource = null;
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from product", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MySqlConnection con = Utility.GetConnection();

            //string query = "select * from product where product_id=2"; // || in sql ist wie das + in C#. Hierdruch werden strings zusammengesetzt  https://stackoverflow.com/questions/13950279/like-statement-for-npgsql-using-parameter
            //MySqlCommand command = new MySqlCommand(query, con);
            //MySqlDataReader reader = command.ExecuteReader();
            //DataTable dt = new DataTable();
            //dataGridView1.DataSource = dt;
            ////MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
            ////DataTable dt = new DataTable();
            ////adapter.Fill(dt);
            ////chart1.DataSource = dt;
            //////chart1.Series["product"].XValueMember = "Produkt";
            ////chart1.Series["productName"].YValueMembers = "productName";
            ////chart1.Titles.Add("Inventar");
            ////MessageBox.Show(chart1.ToString());


            //con.Close();



            MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=Parisa;password=Inventar2023;database=inventarapp"); //open connection
            con.Open();
            dataGridView1.DataSource = null;
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from product", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
