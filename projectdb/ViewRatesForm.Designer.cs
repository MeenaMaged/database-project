namespace Project
{
    partial class ViewRatesForm
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
            dataGridViewRates = new DataGridView();
            lblAvg = new Label();
            lblNoRates = new Label();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRates).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewRates
            // 
            dataGridViewRates.AllowUserToAddRows = false;
            dataGridViewRates.AllowUserToDeleteRows = false;
            dataGridViewRates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewRates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRates.Location = new Point(25, 80);
            dataGridViewRates.Name = "dataGridViewRates";
            dataGridViewRates.ReadOnly = true;
            dataGridViewRates.RowHeadersWidth = 62;
            dataGridViewRates.Size = new Size(600, 300);
            dataGridViewRates.TabIndex = 0;
            // 
            // lblAvg
            // 
            lblAvg.AutoSize = true;
            lblAvg.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAvg.Location = new Point(25, 25);
            lblAvg.Name = "lblAvg";
            lblAvg.Size = new Size(195, 32);
            lblAvg.TabIndex = 1;
            lblAvg.Text = "Average Rating:";
            // 
            // lblNoRates
            // 
            lblNoRates.AutoSize = true;
            lblNoRates.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
            lblNoRates.ForeColor = Color.Gray;
            lblNoRates.Location = new Point(200, 200);
            lblNoRates.Name = "lblNoRates";
            lblNoRates.Size = new Size(251, 30);
            lblNoRates.TabIndex = 2;
            lblNoRates.Text = "No ratings for this product yet.";
            lblNoRates.Visible = false;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(525, 400);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 40);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // ViewRatesForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 460);
            Controls.Add(btnClose);
            Controls.Add(lblNoRates);
            Controls.Add(lblAvg);
            Controls.Add(dataGridViewRates);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ViewRatesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Product Reviews";
            Load += ViewRatesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewRates).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewRates;
        private Label lblAvg;
        private Label lblNoRates;
        private Button btnClose;
    }
}
