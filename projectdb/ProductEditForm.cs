using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Project
{
    public partial class ProductEditForm : Form
    {
        private readonly projectdb.DatabaseService _dbService;
        private int _productId = -1;
        private int _vendorId = -1;

        public ProductEditForm(projectdb.DatabaseService dbService)
        {
            _dbService = dbService;
            InitializeComponent();
            LoadCategories();
            SetVendorId();
        }

        public ProductEditForm(projectdb.DatabaseService dbService, int productId) : this(dbService)
        {
            _productId = productId;
            LoadProduct();
        }

        public ProductEditForm(projectdb.DatabaseService dbService, int productId, int vendorId) : this(dbService, productId)
        {
            _vendorId = vendorId;
        }

        public ProductEditForm(projectdb.DatabaseService dbService, bool isNew, int vendorId) : this(dbService)
        {
            _vendorId = vendorId;
        }

        private void SetVendorId()
        {
            if (projectdb.Session.UserId.HasValue)
            {
                string query = "SELECT VendorId FROM Vendor WHERE UserId = @userId";
                using (var con = _dbService.GetConnection())
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", projectdb.Session.UserId.Value);
                    try
                    {
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            _vendorId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error setting vendor ID: " + ex.Message);
                    }
                }
            }
        }

        private void LoadCategories()
        {
            string q = "SELECT CategoryId, Name FROM Category";
            using (var con = _dbService.GetConnection())
            using (var cmd = new SqlCommand(q, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var table = new DataTable();
                try
                {
                    adapter.Fill(table);
                    comboBoxCategory.DisplayMember = "Name";
                    comboBoxCategory.ValueMember = "CategoryId";
                    comboBoxCategory.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading categories: " + ex.Message);
                }
            }
        }

        private void LoadVendors()
        {
            // Keeping this for compatibility if needed, but we use _vendorId now
            string q = "SELECT VendorId, Name FROM Vendor";
            using (var con = _dbService.GetConnection())
            using (var cmd = new SqlCommand(q, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var table = new DataTable();
                try
                {
                    adapter.Fill(table);
                    comboBoxVendor.DisplayMember = "Name";
                    comboBoxVendor.ValueMember = "VendorId";
                    comboBoxVendor.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading vendors: " + ex.Message);
                }
            }
        }

        private void LoadProduct()
        {
            string q = "SELECT * FROM Product WHERE ProductId = @id";
            using (var con = _dbService.GetConnection())
            using (var cmd = new SqlCommand(q, con))
            {
                cmd.Parameters.AddWithValue("@id", _productId);
                try
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxName.Text = reader["Name"].ToString();
                            textBoxPrice.Text = reader["Price"].ToString();
                            textBoxDescription.Text = reader["Description"].ToString();
                            textBoxStock.Text = reader["Stock"].ToString();
                            comboBoxCategory.SelectedValue = Convert.ToInt32(reader["CategoryId"]);
                            _vendorId = Convert.ToInt32(reader["VendorId"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading product: " + ex.Message);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Name is required");
                return;
            }

            if (_vendorId == -1)
            {
                MessageBox.Show("Vendor profile not found. Cannot save product.");
                return;
            }

            decimal price = 0;
            decimal.TryParse(textBoxPrice.Text, out price);
            
            int stock = 0;
            int.TryParse(textBoxStock.Text, out stock);

            int categoryId = comboBoxCategory.SelectedValue == null ? -1 : Convert.ToInt32(comboBoxCategory.SelectedValue);

            string q;
            if (_productId == -1)
            {
                q = "INSERT INTO Product (VendorId, CategoryId, Name, Description, Price, Stock) VALUES (@vendor, @category, @name, @desc, @price, @stock)";
            }
            else
            {
                q = "UPDATE Product SET VendorId=@vendor, CategoryId=@category, Name=@name, Description=@desc, Price=@price, Stock=@stock WHERE ProductId=@id";
            }

            using (var con = _dbService.GetConnection())
            using (var cmd = new SqlCommand(q, con))
            {
                if (_productId != -1) cmd.Parameters.AddWithValue("@id", _productId);
                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@vendor", _vendorId);
                cmd.Parameters.AddWithValue("@category", categoryId);
                cmd.Parameters.AddWithValue("@desc", textBoxDescription.Text);
                cmd.Parameters.AddWithValue("@stock", stock);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving product: " + ex.Message);
                }
            }
        }
    }
}
