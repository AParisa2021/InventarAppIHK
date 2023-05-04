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
{//datagridView mit join alles anzeigen
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SelectItem.Close();

        }

        //to show subform form in mainform
        /// <summary>
        /// Methode öffnet die User Form innerhalb der Main Form, wenn btn_Users etc. geklickt werden. Als Parameter wird die UnterForm als childform(UserForm) in die Methode reingegeben. Die gesamte Form
        /// wird im Panel angezeigt
        /// </summary>
        private Form activeForm = null;
        private void openChildForm(Form childform)
        {
            if (activeForm != null)     //wenn UserForm existiert
                activeForm.Close();     //schließt das Formular
            activeForm = childform;     //aktuelle Form wird zur UnterForm
            childform.TopLevel = false; //Ruft 1 Wert ab, der angibt, ob das Formular als Fenster der obersten Ebene angezeigt wird oder legt diesen fest. True, um das Fenster als obersten Ebene anzuzeigen, sonst false
            childform.FormBorderStyle = FormBorderStyle.None;   //Ramenform für ein Formular
            childform.Dock = DockStyle.Fill;        //Andocken des Steuerelements
            panelMain.Controls.Add(childform);      //gibt die childForm im Panel aus
            panelMain.Tag = childform;              //Tag: 1 Objekt, das Daten über das Steuerelement zeigt
            childform.BringToFront();
            childform.Show();           //zeigt childform an
        }

        /// <summary>
        /// After pressing the Button change Button.Text to Load to Update the table in the DataGridView
        /// </summary>
        private bool _Load;

        public bool Load
        {
            get
            {
                return _Load;
            }
            set
            {
                _Load = value;
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnCategory.Height;
            panelLeft.Top= btnCategory.Top;

            openChildForm(new CategoryForm());            
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnProduct.Height;
            panelLeft.Top = btnProduct.Top;
            openChildForm(new ProductForm());           
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnLocation.Height;
            panelLeft.Top = btnLocation.Top;

            openChildForm(new LocationForm());           
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnOrder.Height;
            panelLeft.Top = btnOrder.Top;

            openChildForm(new TotalForm());           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Inventarchart.Series["Inventar"].Points.AddXY("2023", 2000);
            Inventarchart.Series["Inventar"].Points.AddXY("2023", 2000);


        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
         
        }

     
    }
}
