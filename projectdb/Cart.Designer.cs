namespace projectdb
{
    partial class Cart
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
            btnconfirmorder = new Button();
            dataGridView1 = new DataGridView();
            btnRemove = new Button();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnconfirmorder
            // 
            btnconfirmorder.BackColor = Color.LightGreen;
            btnconfirmorder.Location = new Point(1057, 529);
            btnconfirmorder.Margin = new Padding(4, 4, 4, 4);
            btnconfirmorder.Name = "btnconfirmorder";
            btnconfirmorder.Size = new Size(178, 36);
            btnconfirmorder.TabIndex = 0;
            btnconfirmorder.Text = "Confirm Order";
            btnconfirmorder.UseVisualStyleBackColor = false;
            btnconfirmorder.Click += btnconfirmorder_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 55);
            dataGridView1.Margin = new Padding(4, 4, 4, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1220, 459);
            dataGridView1.TabIndex = 1;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Red;
            btnRemove.Location = new Point(15, 529);
            btnRemove.Margin = new Padding(4, 4, 4, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(178, 38);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(929, 536);
            lblTotal.Margin = new Padding(4, 0, 4, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(102, 25);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "Total: $0.00";
            lblTotal.Click += label1_Click;
            // 
            // Cart
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1285, 594);
            Controls.Add(lblTotal);
            Controls.Add(btnRemove);
            Controls.Add(dataGridView1);
            Controls.Add(btnconfirmorder);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Cart";
            Text = "Form1";
            Load += CartForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnconfirmorder;
        private DataGridView dataGridView1;
        private Button btnRemove;
        private Label lblTotal;
    }
}