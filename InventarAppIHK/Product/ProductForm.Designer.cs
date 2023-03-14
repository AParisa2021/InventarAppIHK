namespace InventarAppIHK
{
    partial class ProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.product_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 413);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 87);
            this.panel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(1080, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(44, 43);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProduct.ColumnHeadersHeight = 29;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.product_id,
            this.productName,
            this.date,
            this.price,
            this.category_id,
            this.edit,
            this.delete});
            this.dgvProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProduct.EnableHeadersVisualStyles = false;
            this.dgvProduct.Location = new System.Drawing.Point(0, 0);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowHeadersWidth = 51;
            this.dgvProduct.RowTemplate.Height = 24;
            this.dgvProduct.Size = new System.Drawing.Size(1200, 413);
            this.dgvProduct.TabIndex = 1;
            this.dgvProduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellContentClick);
            // 
            // product_id
            // 
            this.product_id.HeaderText = "Produktnummer";
            this.product_id.MinimumWidth = 6;
            this.product_id.Name = "product_id";
            this.product_id.Width = 125;
            // 
            // productName
            // 
            this.productName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.productName.HeaderText = "Produktname";
            this.productName.MinimumWidth = 6;
            this.productName.Name = "productName";
            // 
            // date
            // 
            this.date.HeaderText = "Datum";
            this.date.MinimumWidth = 6;
            this.date.Name = "date";
            this.date.Width = 125;
            // 
            // price
            // 
            this.price.HeaderText = "Preis";
            this.price.MinimumWidth = 6;
            this.price.Name = "price";
            this.price.Width = 125;
            // 
            // category_id
            // 
            this.category_id.HeaderText = "Kategorie";
            this.category_id.MinimumWidth = 6;
            this.category_id.Name = "category_id";
            this.category_id.Width = 125;
            // 
            // edit
            // 
            this.edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.edit.HeaderText = "Bearbeiten";
            this.edit.Image = ((System.Drawing.Image)(resources.GetObject("edit.Image")));
            this.edit.MinimumWidth = 6;
            this.edit.Name = "edit";
            this.edit.Width = 79;
            // 
            // delete
            // 
            this.delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.delete.HeaderText = "Löschen";
            this.delete.MinimumWidth = 6;
            this.delete.Name = "delete";
            this.delete.Width = 64;
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 500);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProductForm";
            this.Text = "ProductForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn category_id;
        private System.Windows.Forms.DataGridViewImageColumn edit;
        private System.Windows.Forms.DataGridViewImageColumn delete;
        private System.Windows.Forms.Button btnAdd;
    }
}