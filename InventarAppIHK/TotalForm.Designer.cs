namespace InventarAppIHK
{
    partial class TotalForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvTotal = new System.Windows.Forms.DataGridView();
            this.total_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 303);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 161);
            this.panel1.TabIndex = 0;
            // 
            // dgvTotal
            // 
            this.dgvTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.total_id,
            this.productName,
            this.date,
            this.price,
            this.categoryName,
            this.edit,
            this.delete});
            this.dgvTotal.Location = new System.Drawing.Point(0, 12);
            this.dgvTotal.Name = "dgvTotal";
            this.dgvTotal.RowHeadersWidth = 51;
            this.dgvTotal.RowTemplate.Height = 24;
            this.dgvTotal.Size = new System.Drawing.Size(823, 231);
            this.dgvTotal.TabIndex = 1;
            // 
            // total_id
            // 
            this.total_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.total_id.HeaderText = "Nummer";
            this.total_id.MinimumWidth = 6;
            this.total_id.Name = "total_id";
            this.total_id.Width = 87;
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
            // categoryName
            // 
            this.categoryName.HeaderText = "Kategorie";
            this.categoryName.MinimumWidth = 6;
            this.categoryName.Name = "categoryName";
            this.categoryName.Width = 125;
            // 
            // edit
            // 
            this.edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.edit.HeaderText = "bearbeiten";
            this.edit.MinimumWidth = 6;
            this.edit.Name = "edit";
            this.edit.Width = 78;
            // 
            // delete
            // 
            this.delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.delete.HeaderText = "Löschen";
            this.delete.MinimumWidth = 6;
            this.delete.Name = "delete";
            this.delete.Width = 64;
            // 
            // TotalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 464);
            this.Controls.Add(this.dgvTotal);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TotalForm";
            this.Text = "TotalForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryName;
        private System.Windows.Forms.DataGridViewImageColumn edit;
        private System.Windows.Forms.DataGridViewImageColumn delete;
    }
}