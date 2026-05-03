using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class CategoryProductsForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();
        private int _categoryId;
        private string _categoryName;

        public CategoryProductsForm(int categoryId, string categoryName)
        {
            InitializeComponent();
            _categoryId = categoryId;
            _categoryName = categoryName;
            this.Text = categoryName;
            lblHeader.Text = categoryName;
        }

        private void CategoryProductsForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            pnlProducts.Controls.Clear();

            string query = "SELECT ProductId, Name, Price, Stock FROM Product WHERE CategoryId = @cid";
            DataTable table = new DataTable();

            using (SqlConnection con = _dbService.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cid", _categoryId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
                empty.Text = "No products found in this category.";
                empty.AutoSize = true;
                empty.Font = new Font("Segoe UI", 11F);
                empty.ForeColor = Color.Gray;
                empty.Margin = new Padding(20);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
