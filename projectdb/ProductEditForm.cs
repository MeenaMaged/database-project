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

        public ProductEditForm(projectdb.DatabaseService dbService)
        {
            _dbService = dbService;
            InitializeComponent();
            LoadVendors();
        }

        public ProductEditForm(projectdb.DatabaseService dbService, int productId) : this(dbService)
        {
            _productId = productId;
            LoadProduct();
        }

        private void LoadVendors()
        {
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
                            comboBoxVendor.SelectedValue = Convert.ToInt32(reader["VendorId"]);
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

            decimal price = 0;
            decimal.TryParse(textBoxPrice.Text, out price);
            int vendorId = comboBoxVendor.SelectedValue == null ? -1 : Convert.ToInt32(comboBoxVendor.SelectedValue);

            string q;
            if (_productId == -1)
            {
                q = "INSERT INTO Product (Name, Price, VendorId) VALUES (@name, @price, @vendor)";
            }
            else
            {
                q = "UPDATE Product SET Name=@name, Price=@price, VendorId=@vendor WHERE ProductId=@id";
            }

            using (var con = _dbService.GetConnection())
            using (var cmd = new SqlCommand(q, con))
            {
                if (_productId != -1) cmd.Parameters.AddWithValue("@id", _productId);
                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@vendor", vendorId);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving product: " + ex.Message);
                }
            }
        }
    }
}
