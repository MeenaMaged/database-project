using Microsoft.Data.SqlClient;
using projectdb;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class UserHomeForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();

        public UserHomeForm()
        {
            InitializeComponent();
        }

        private void UserHomeForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            pnlCategories.Controls.Clear();

            string query = "SELECT CategoryID, Name FROM Category";
            DataTable table = new DataTable();

            using (SqlConnection con = _dbService.GetConnection())
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading categories: " + ex.Message);
                    return;
                }
            }

            foreach (DataRow row in table.Rows)
            {
                int catId = Convert.ToInt32(row["CategoryID"]);
                string catName = row["Name"].ToString();

                Panel card = new Panel();
                card.Size = new Size(140, 100);
                card.Margin = new Padding(8);
                card.BackColor = Color.FromArgb(230, 240, 255);
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Cursor = Cursors.Hand;
                card.Tag = catId;

                Label lbl = new Label();
                lbl.Text = catName;
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

                card.Controls.Add(lbl);

                // Both the panel and the label fire the same handler
                card.Click += Category_Click;
                lbl.Click += (s, ev) => Category_Click(card, ev);

                pnlCategories.Controls.Add(card);
            }
        }

        private void Category_Click(object sender, EventArgs e)
        {
            Panel card = sender as Panel;
            if (card == null || card.Tag == null) return;

            int catId = (int)card.Tag;
            string catName = "";
            if (card.Controls.Count > 0 && card.Controls[0] is Label lbl)
            {
                catName = lbl.Text;
            }

            CategoryProductsForm form = new CategoryProductsForm(catId, catName);
            form.Show();
        }

        private void LoadProducts()
        {
            pnlProducts.Controls.Clear();

            string query = "SELECT ProductId, Name, Price, Stock FROM Product";
            DataTable table = new DataTable();

            using (SqlConnection con = _dbService.GetConnection())
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading products: " + ex.Message);
                    return;
                }
            }

            if (table.Rows.Count == 0)
            {
                Label empty = new Label();
                empty.Text = "No products available right now.";
                empty.AutoSize = true;
                empty.Font = new Font("Segoe UI", 11F);
                empty.ForeColor = Color.Gray;
                empty.Margin = new Padding(15);
                pnlProducts.Controls.Add(empty);
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                int productId = Convert.ToInt32(row["ProductId"]);
                string name = row["Name"].ToString();
                decimal price = Convert.ToDecimal(row["Price"]);
                int stock = Convert.ToInt32(row["Stock"]);

                pnlProducts.Controls.Add(BuildProductCard(productId, name, price, stock));
            }
        }

        private Panel BuildProductCard(int productId, string name, decimal price, int stock)
        {
            Panel card = new Panel();
            card.Size = new Size(180, 175);
            card.Margin = new Padding(8);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Tag = productId;

            Label lblName = new Label();
            lblName.Text = name;
            lblName.Location = new Point(8, 10);
            lblName.Size = new Size(160, 50);
            lblName.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            Label lblPrice = new Label();
            lblPrice.Text = price.ToString("0.00") + " EGP";
            lblPrice.Location = new Point(8, 65);
            lblPrice.Size = new Size(160, 25);
            lblPrice.Font = new Font("Segoe UI", 10F);
            lblPrice.ForeColor = Color.FromArgb(0, 100, 0);

            Label lblStock = new Label();
            lblStock.Text = stock > 0 ? "In stock: " + stock : "Out of stock";
            lblStock.Location = new Point(8, 95);
            lblStock.Size = new Size(160, 22);
            lblStock.Font = new Font("Segoe UI", 9F);
            lblStock.ForeColor = stock > 0 ? Color.DimGray : Color.Firebrick;

            Button btnView = new Button();
            btnView.Text = "View";
            btnView.Size = new Size(160, 30);
            btnView.Location = new Point(8, 130);
            btnView.Click += (s, e) => OpenProductDetails(productId);

            card.Controls.Add(lblName);
            card.Controls.Add(lblPrice);
            card.Controls.Add(lblStock);
            card.Controls.Add(btnView);
            return card;
        }

        private void OpenProductDetails(int productId)
        {
            // Product details screen is built by another teammate; placeholder for now.
            MessageBox.Show("Product details for ID " + productId + " will open here.", "Info");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search s = new search();
            s.Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            MyOrdersForm orders =new  MyOrdersForm();
            orders.Show();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();

        }
    }
}
