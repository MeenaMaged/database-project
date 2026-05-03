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
    public partial class ProductsForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();
        private int _selectedProductId = -1;

        public ProductsForm()
        {
            InitializeComponent();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // We want to see all the products
            string query = "SELECT * FROM Product";

            using (SqlConnection con = _dbService.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Ensure Edit/Delete button columns exist once
                    // Remove existing if present
                    if (dataGridView1.Columns.Contains("Edit"))
                        dataGridView1.Columns.Remove("Edit");
                    if (dataGridView1.Columns.Contains("Delete"))
                        dataGridView1.Columns.Remove("Delete");

                    var editCol = new DataGridViewButtonColumn();
                    editCol.Name = "Edit";
                    editCol.HeaderText = "Edit";
                    editCol.Text = "Edit";
                    editCol.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(editCol);

                    var deleteCol = new DataGridViewButtonColumn();
                    deleteCol.Name = "Delete";
                    deleteCol.HeaderText = "Delete";
                    deleteCol.Text = "Delete";
                    deleteCol.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(deleteCol);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the user selected a row (by clicking the far left edge of the grid)
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Ask for confirmation before deleting (good practice!)
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    // Get the ID from the selected row (Assuming ProductId is the first column - Index 0)
                    int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                    // The SQL command to delete the specific product
                    string query = "DELETE FROM Product WHERE ProductId = @id"; // CHANGE 'ID' IF YOUR COLUMN NAME IS DIFFERENT

                    using (SqlConnection con = _dbService.GetConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Pass the ID as a parameter to prevent SQL Injection
                            cmd.Parameters.AddWithValue("@id", selectedId);

                            try
                            {
                                con.Open();
                                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the delete

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Product deleted successfully!");
                                    LoadData(); // Refresh the grid to show the updated list
                                }
                                else
                                {
                                    MessageBox.Show("Could not delete the product. It may have already been removed.");
                                }
                            }
                            catch (SqlException ex)
                            {
                                // Handle Foreign Key constraints (e.g., if the product is in an Order)
                                if (ex.Number == 547)
                                {
                                    MessageBox.Show("Cannot delete this product because it is currently linked to existing orders. You must remove it from the orders first.");
                                }
                                else
                                {
                                    MessageBox.Show("Database error: " + ex.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An error occurred: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an entire row to delete by clicking the arrow on the far left edge of the row.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = dataGridView1;

            // Edit button clicked
            if (grid.Columns[e.ColumnIndex].Name == "Edit")
            {
                int id = Convert.ToInt32(grid.Rows[e.RowIndex].Cells[0].Value);
                var editForm = new ProductEditForm(_dbService, id);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }

            // Delete button clicked
            if (grid.Columns[e.ColumnIndex].Name == "Delete")
            {
                int id = Convert.ToInt32(grid.Rows[e.RowIndex].Cells[0].Value);
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    string query = "DELETE FROM Product WHERE ProductId = @id";
                    using (SqlConnection con = _dbService.GetConnection())
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        try
                        {
                            con.Open();
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0) LoadData();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 547)
                                MessageBox.Show("Cannot delete this product because it is linked to existing orders.");
                            else
                                MessageBox.Show("Database error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
