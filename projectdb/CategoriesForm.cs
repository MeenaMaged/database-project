using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class CategoriesForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();

        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            LoadParentCategories();
            LoadCategories();
        }

        private void LoadParentCategories()
        {
            // Load parent categories into the combo box
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(object));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(DBNull.Value, "None");

            string query = "SELECT CategoryId, Name FROM Category";

            using (SqlConnection con = _dbService.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int categoryId = Convert.ToInt32(reader["CategoryId"]);
                        string categoryName = reader["Name"].ToString();
                        dt.Rows.Add(categoryId, categoryName);
                    }
                    cmbParentCategory.DataSource = dt;
                    cmbParentCategory.DisplayMember = "Name";
                    cmbParentCategory.ValueMember = "Id";
                    cmbParentCategory.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading parent categories: " + ex.Message);
                }
            }
        }

        private void LoadCategories()
        {
            // Fetching from the Category table
            string query = "SELECT * FROM Category";

            using (SqlConnection con = _dbService.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Categories: " + ex.Message);
                }
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Please enter a category name.");
                return;
            }

            string categoryName = txtCategoryName.Text.Trim();
            
            // Extract parent category ID from ComboBox
            int? parentCategoryId = null;
            object selectedValue = cmbParentCategory.SelectedValue;
            if (selectedValue != null && selectedValue != DBNull.Value && int.TryParse(selectedValue.ToString(), out int intVal))
            {
                parentCategoryId = intVal;
            }

            string query = "INSERT INTO Category (Name, ParentCategoryID) VALUES (@Name, @ParentCategoryID)";

            using (SqlConnection con = _dbService.GetConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", categoryName);
                        cmd.Parameters.AddWithValue("@ParentCategoryID", (object)parentCategoryId ?? DBNull.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category added successfully.");
                            txtCategoryName.Clear();
                            cmbParentCategory.SelectedIndex = 0;
                            LoadParentCategories(); // Refresh parent categories dropdown
                            LoadCategories(); // Refresh the grid
                        }
                        else
                        {
                            MessageBox.Show("Failed to add category.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding category: " + ex.Message);
                }
            }
        }

      
    }
}
