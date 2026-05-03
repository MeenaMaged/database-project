namespace Project
{
    partial class UserHomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            pnlTopBar = new Panel();
            btnSearch = new Button();
            btnProfile = new Button();
            btnCart = new Button();
            lblCategories = new Label();
            pnlCategories = new FlowLayoutPanel();
            lblProducts = new Label();
            pnlProducts = new FlowLayoutPanel();
            pnlTopBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.FromArgb(64, 64, 96);
            pnlTopBar.Controls.Add(btnSearch);
            pnlTopBar.Controls.Add(btnProfile);
            pnlTopBar.Controls.Add(btnCart);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(1205, 55);
            pnlTopBar.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(801, 10);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(125, 36);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnProfile
            // 
            btnProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProfile.FlatStyle = FlatStyle.Flat;
            btnProfile.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnProfile.ForeColor = Color.White;
            btnProfile.Location = new Point(941, 10);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(143, 36);
            btnProfile.TabIndex = 1;
            btnProfile.Text = "My Orders";
            btnProfile.UseVisualStyleBackColor = true;
            btnProfile.Click += btnProfile_Click;
            // 
            // btnCart
            // 
            btnCart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCart.FlatStyle = FlatStyle.Flat;
            btnCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCart.ForeColor = Color.White;
            btnCart.Location = new Point(1100, 10);
            btnCart.Name = "btnCart";
            btnCart.Size = new Size(95, 36);
            btnCart.TabIndex = 2;
            btnCart.Text = "Cart";
            btnCart.UseVisualStyleBackColor = true;
            btnCart.Click += btnCart_Click;
            // 
            // lblCategories
            // 
            lblCategories.AutoSize = true;
            lblCategories.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCategories.Location = new Point(15, 60);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new Size(130, 32);
            lblCategories.TabIndex = 1;
            lblCategories.Text = "Categories";
            // 
            // pnlCategories
            // 
            pnlCategories.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCategories.AutoScroll = true;
            pnlCategories.BorderStyle = BorderStyle.FixedSingle;
            pnlCategories.Location = new Point(15, 95);
            pnlCategories.Name = "pnlCategories";
            pnlCategories.Size = new Size(1175, 154);
            pnlCategories.TabIndex = 2;
            pnlCategories.WrapContents = false;
            // 
            // lblProducts
            // 
            lblProducts.AutoSize = true;
            lblProducts.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblProducts.Location = new Point(15, 252);
            lblProducts.Name = "lblProducts";
            lblProducts.Size = new Size(109, 32);
            lblProducts.TabIndex = 3;
            lblProducts.Text = "Products";
            // 
            // pnlProducts
            // 
            pnlProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlProducts.AutoScroll = true;
            pnlProducts.BorderStyle = BorderStyle.FixedSingle;
            pnlProducts.Location = new Point(15, 287);
            pnlProducts.Name = "pnlProducts";
            pnlProducts.Size = new Size(1175, 418);
            pnlProducts.TabIndex = 4;
            // 
            // UserHomeForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1205, 720);
            Controls.Add(pnlProducts);
            Controls.Add(lblProducts);
            Controls.Add(pnlCategories);
            Controls.Add(lblCategories);
            Controls.Add(pnlTopBar);
            MinimumSize = new Size(940, 700);
            Name = "UserHomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Home";
            Load += UserHomeForm_Load;
            pnlTopBar.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTopBar;
        private Button btnSearch;
        private Button btnProfile;
        private Button btnCart;
        private Label lblCategories;
        private FlowLayoutPanel pnlCategories;
        private Label lblProducts;
        private FlowLayoutPanel pnlProducts;
    }
}
