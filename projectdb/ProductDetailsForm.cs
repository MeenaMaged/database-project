using Microsoft.Data.SqlClient;
using projectdb;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class ProductDetailsForm : Form
    {
        private readonly DatabaseService _dbService = new DatabaseService();
        private readonly Cartcontroller _cartController = new Cartcontroller();
        private int _productId;
        private decimal _price;
        private string _name;

        public ProductDetailsForm(int productId)
        {
            InitializeComponent();
            _productId = productId;
        }

        private void ProductDetailsForm_Load(object sender, EventArgs e)
        {
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            string query = @"
                SELECT p.Name, p.Description, p.Price, p.Stock, v.StoreName, c.Name as CategoryName
                FROM Product p
                JOIN Vendor v ON p.VendorId = v.VendorId
                JOIN Category c ON p.CategoryId = c.CategoryId
                WHERE p.ProductId = @id";

            using (SqlConnection con = _dbService.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", _productId);
                try
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _name = reader["Name"].ToString();
                            _price = Convert.ToDecimal(reader["Price"]);
                            
                            lblName.Text = _name;
                            lblPrice.Text = _price.ToString("0.00") + " EGP";
                            lblStock.Text = "Stock: " + reader["Stock"].ToString();
                            lblVendor.Text = "Vendor: " + reader["StoreName"].ToString();
                            lblCategory.Text = "Category: " + reader["CategoryName"].ToString();
                            txtDescription.Text = reader["Description"].ToString();

                            int stock = Convert.ToInt32(reader["Stock"]);
                            btnAddToCart.Enabled = stock > 0;
                            if (stock <= 0) lblStock.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading details: " + ex.Message);
                }
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (!Session.UserId.HasValue)
            {
                MessageBox.Show("Please login to add items to cart.");
                return;
            }

            int qty = (int)numQuantity.Value;
            try
            {
                if (_cartController.AddToCart(Session.UserId.Value, _productId, qty, _price))
                {
                    MessageBox.Show("Product added to cart!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            if (!Session.UserId.HasValue)
            {
                MessageBox.Show("Please login to rate products.");
                return;
            }

            Product_Rate_Screen rateScreen = new Product_Rate_Screen(Session.UserId.Value, _productId);
            rateScreen.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewRates_Click(object sender, EventArgs e)
        {
            ViewRatesForm ratesForm = new ViewRatesForm(_productId);
            ratesForm.ShowDialog();
        }
    }
}
