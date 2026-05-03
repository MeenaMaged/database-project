namespace Project
{
    partial class ProductDetailsForm
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
            lblName = new Label();
            lblPrice = new Label();
            lblStock = new Label();
            lblVendor = new Label();
            lblCategory = new Label();
            txtDescription = new TextBox();
            btnAddToCart = new Button();
            numQuantity = new NumericUpDown();
            label1 = new Label();
            btnRate = new Button();
            btnBack = new Button();
            btnViewRates = new Button();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblName.Location = new Point(25, 25);
            lblName.Name = "lblName";
            lblName.Size = new Size(233, 45);
            lblName.TabIndex = 0;
            lblName.Text = "Product Name";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPrice.ForeColor = Color.Green;
            lblPrice.Location = new Point(25, 80);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(135, 38);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "0.00 EGP";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(25, 130);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(74, 25);
            lblStock.TabIndex = 2;
            lblStock.Text = "Stock: 0";
            // 
            // lblVendor
            // 
            lblVendor.AutoSize = true;
            lblVendor.Location = new Point(25, 165);
            lblVendor.Name = "lblVendor";
            lblVendor.Size = new Size(73, 25);
            lblVendor.TabIndex = 3;
            lblVendor.Text = "Vendor:";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(25, 200);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(88, 25);
            lblCategory.TabIndex = 4;
            lblCategory.Text = "Category:";
            // 
            // txtDescription
            // 
            txtDescription.BackColor = SystemColors.Window;
            txtDescription.Location = new Point(25, 245);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(500, 120);
            txtDescription.TabIndex = 5;
            // 
            // btnAddToCart
            // 
            btnAddToCart.BackColor = Color.DodgerBlue;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.Location = new Point(325, 410);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(200, 50);
            btnAddToCart.TabIndex = 6;
            btnAddToCart.Text = "Add to Cart";
            btnAddToCart.UseVisualStyleBackColor = false;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // numQuantity
            // 
            numQuantity.Location = new Point(130, 420);
            numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(80, 31);
            numQuantity.TabIndex = 7;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 422);
            label1.Name = "label1";
            label1.Size = new Size(84, 25);
            label1.TabIndex = 8;
            label1.Text = "Quantity:";
            // 
            // btnRate
            // 
            btnRate.Location = new Point(25, 480);
            btnRate.Name = "btnRate";
            btnRate.Size = new Size(150, 40);
            btnRate.TabIndex = 9;
            btnRate.Text = "Rate Product";
            btnRate.UseVisualStyleBackColor = true;
            btnRate.Click += btnRate_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(425, 25);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 35);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnViewRates
            // 
            btnViewRates.Location = new Point(200, 480);
            btnViewRates.Name = "btnViewRates";
            btnViewRates.Size = new Size(150, 40);
            btnViewRates.TabIndex = 11;
            btnViewRates.Text = "View Reviews";
            btnViewRates.UseVisualStyleBackColor = true;
            btnViewRates.Click += btnViewRates_Click;
            // 
            // ProductDetailsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 550);
            Controls.Add(btnViewRates);
            Controls.Add(btnBack);
            Controls.Add(btnRate);
            Controls.Add(label1);
            Controls.Add(numQuantity);
            Controls.Add(btnAddToCart);
            Controls.Add(txtDescription);
            Controls.Add(lblCategory);
            Controls.Add(lblVendor);
            Controls.Add(lblStock);
            Controls.Add(lblPrice);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ProductDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Product Details";
            Load += ProductDetailsForm_Load;
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblPrice;
        private Label lblStock;
        private Label lblVendor;
        private Label lblCategory;
        private TextBox txtDescription;
        private Button btnAddToCart;
        private NumericUpDown numQuantity;
        private Label label1;
        private Button btnRate;
        private Button btnBack;
        private Button btnViewRates;
    }
}
