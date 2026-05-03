namespace Project
{
    partial class AdminDashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Vendors = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // Vendors
            // 
            Vendors.Location = new Point(154, 171);
            Vendors.Name = "Vendors";
            Vendors.Size = new Size(230, 87);
            Vendors.TabIndex = 0;
            Vendors.Text = "Vendors";
            Vendors.UseVisualStyleBackColor = true;
            Vendors.Click += Vendors_Click;
            // 
            // button1
            // 
            button1.Location = new Point(433, 171);
            button1.Name = "button1";
            button1.Size = new Size(216, 87);
            button1.TabIndex = 1;
            button1.Text = "Users";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(154, 282);
            button2.Name = "button2";
            button2.Size = new Size(230, 74);
            button2.TabIndex = 2;
            button2.Text = "Products ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(433, 282);
            button3.Name = "button3";
            button3.Size = new Size(216, 74);
            button3.TabIndex = 3;
            button3.Text = "Categories ";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Menu;
            textBox1.Font = new Font("Segoe UI", 24F);
            textBox1.ForeColor = SystemColors.InfoText;
            textBox1.Location = new Point(202, 36);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(377, 83);
            textBox1.TabIndex = 4;
            textBox1.Text = "Welcome Back ";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(Vendors);
            Name = "AdminDashboard";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Vendors;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox1;
    }
}
