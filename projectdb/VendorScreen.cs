using Microsoft.Data.SqlClient;
using projectdb;
using System;
using System.Data;
using System.Windows.Forms;

namespace Project
{
    public partial class VendorScreen : Form
    {
        private DatabaseService _dbService = new DatabaseService();
        private int _vendorId = -1;

        public VendorScreen()
        {
            InitializeComponent();
        }

        public VendorScreen(int vendorId) : this()
        {
            _vendorId = vendorId;
        }

        private void VendorScreen_Load(object sender, EventArgs e)
        {
            if (_vendorId != -1)
            {
                LoadData();
                return;
            }

            if (Session.UserId.HasValue)
            {
                _vendorId = GetVendorId(Session.UserId.Value);
                if (_vendorId != -1)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vendor profile not found for this user.");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No active session. Please login.");
                this.Close();
            }
        }

        private int GetVendorId(int userId)
        {
            string query = "SELECT VendorId FROM Vendor WHERE UserId = @userId";
            using (SqlConnection con = _dbService.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result) : -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching VendorId: " + ex.Message);
                    return -1;
                }
            }
        }

        private void LoadData()
        {
            string query = "SELECT * FROM Product WHERE VendorId = @vendorId";

            using (SqlConnection con = _dbService.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@vendorId", _vendorId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();

                    try
                    {
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;

                        // Ensure Edit/Delete button columns exist once
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = dataGridView1;

            // Edit button clicked
            if (grid.Columns[e.ColumnIndex].Name == "Edit")
            {
                int id = Convert.ToInt32(grid.Rows[e.RowIndex].Cells[0].Value);
                var editForm = new ProductEditForm(_dbService, id, _vendorId);
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
                    DeleteProduct(id);
                }
            }
        }

        private void DeleteProduct(int id)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new ProductEditForm(_dbService, true, _vendorId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    DeleteProduct(selectedId);
                }
            }
            else
            {
                MessageBox.Show("Please select an entire row to delete.");
            }
        }
    }
}
