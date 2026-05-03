namespace Project
{
    partial class VendorScreen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnDeleteSelected = new Button();
            btnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(31, 47);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1387, 401);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.Location = new Point(1053, 474);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.Size = new Size(194, 61);
            btnDeleteSelected.TabIndex = 1;
            btnDeleteSelected.Text = "Delete Selected product";
            btnDeleteSelected.UseVisualStyleBackColor = true;
            btnDeleteSelected.Click += btnDeleteSelected_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(1253, 474);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(194, 61);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add New Product";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // VendorScreen
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1483, 564);
            Controls.Add(btnDeleteSelected);
            Controls.Add(btnAdd);
            Controls.Add(dataGridView1);
            Name = "VendorScreen";
            Text = "Vendor Dashboard - My Products";
            Load += VendorScreen_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnDeleteSelected;
        private Button btnAdd;
    }
}
