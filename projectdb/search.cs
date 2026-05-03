using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace projectdb
{
    public partial class search : Form
    {
        public search()
        {
            InitializeComponent();
        }

        private void search_Load(object sender, EventArgs e)
        {
            // Sets the default filter to 'Product'
            if (comboFilterType.Items.Count > 0)
                comboFilterType.SelectedIndex = 0;
        }

        // This method name must match the Designer exactly to fix error CS0103
        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            string filterType = comboFilterType.SelectedItem?.ToString();
            string searchText = txtSearchInput.Text;

            // Clear results if the search box is emptied
            if (string.IsNullOrEmpty(searchText))
            {
                dataGridViewResults.DataSource = null;
                return;
            }

            DatabaseService db = new DatabaseService();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // Map selection to database columns for DESKTOP-9VOLK8E
                    string filterColumn = filterType switch
                    {
                        "Product" => "p.Name",
                        "Vendor" => "v.StoreName",
                        "Category" => "c.Name",
                        _ => "p.Name"
                    };

                    string sql = $@"SELECT p.Name, p.Price, v.StoreName, c.Name as CategoryName 
                                 FROM Product p 
                                 JOIN Vendor v ON p.VendorId = v.VendorId 
                                 JOIN Category c ON p.CategoryId = c.CategoryId 
                                 WHERE {filterColumn} LIKE @searchText";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchText", searchText + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridViewResults.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                // Writes to the Output window instead of popping up boxes while typing
                System.Diagnostics.Debug.WriteLine("Search Error: " + ex.Message);
            }
        }

        // Empty handlers to prevent errors if the designer still points to them
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void comboFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}