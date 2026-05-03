namespace Project
{
    partial class CategoriesForm
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
            dataGridView1 = new DataGridView();
            txtCategoryName = new TextBox();
            lblCategoryName = new Label();
            btnAddCategory = new Button();
            cmbParentCategory = new ComboBox();
            lblParentCategory = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(49, 45);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(723, 387);
            dataGridView1.TabIndex = 5;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(163, 7);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(150, 31);
            txtCategoryName.TabIndex = 1;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(49, 10);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(140, 25);
            lblCategoryName.TabIndex = 0;
            lblCategoryName.Text = "Category Name:";
            // 
            // btnAddCategory
            // 
            btnAddCategory.Location = new Point(580, 7);
            btnAddCategory.Name = "btnAddCategory";
            btnAddCategory.Size = new Size(100, 31);
            btnAddCategory.TabIndex = 4;
            btnAddCategory.Text = "Add Category";
            btnAddCategory.UseVisualStyleBackColor = true;
            btnAddCategory.Click += btnAddCategory_Click;
            // 
            // cmbParentCategory
            // 
            cmbParentCategory.FormattingEnabled = true;
            cmbParentCategory.Location = new Point(454, 7);
            cmbParentCategory.Name = "cmbParentCategory";
            cmbParentCategory.Size = new Size(120, 33);
            cmbParentCategory.TabIndex = 3;
            // 
            // lblParentCategory
            // 
            lblParentCategory.AutoSize = true;
            lblParentCategory.Location = new Point(330, 10);
            lblParentCategory.Name = "lblParentCategory";
            lblParentCategory.Size = new Size(142, 25);
            lblParentCategory.TabIndex = 2;
            lblParentCategory.Text = "Parent Category:";
            // 
            // CategoriesForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAddCategory);
            Controls.Add(cmbParentCategory);
            Controls.Add(lblParentCategory);
            Controls.Add(txtCategoryName);
            Controls.Add(lblCategoryName);
            Controls.Add(dataGridView1);
            Name = "CategoriesForm";
            Text = "CategoriesForm";
            Load += CategoriesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtCategoryName;
        private Label lblCategoryName;
        private Button btnAddCategory;
        private ComboBox cmbParentCategory;
        private Label lblParentCategory;
    }
}