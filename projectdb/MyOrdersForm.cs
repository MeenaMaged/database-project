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
    public partial class MyOrdersForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();

        public MyOrdersForm()
        {
            InitializeComponent();
        }

        private void MyOrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            // Clear existing columns in case we reload
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;

            if (!projectdb.Session.UserId.HasValue)
            {
                MessageBox.Show("No logged-in user found. Please login again.");
                return;
            }

            string query = "SELECT OrderId, UserId, Status, CreatedAt FROM Orders WHERE UserId = @userId";

            using (SqlConnection con = _dbService.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@userId", projectdb.Session.UserId.Value);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            // --- MAGIC STEP: Add the Cancel Button Column ---
            DataGridViewButtonColumn cancelBtnColumn = new DataGridViewButtonColumn();
            cancelBtnColumn.HeaderText = "Action";
            cancelBtnColumn.Name = "CancelButton";
            cancelBtnColumn.Text = "Cancel Order";
            cancelBtnColumn.UseColumnTextForButtonValue = true; // Makes the text show up on the button

            // Add the button column to the very end of the grid
            dataGridView1.Columns.Add(cancelBtnColumn);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "CancelButton")
            {
                // Get the status and ID from the row they clicked
                string currentStatus = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                int orderId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderId"].Value);

                // Check the requirement: ONLY pending orders can be cancelled
                if (currentStatus == "Pending")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to cancel Order #" + orderId + "?", "Confirm Cancel", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        CancelOrder(orderId);
                    }
                }
                else
                {
                    MessageBox.Show("You can only cancel Pending orders. This order is already " + currentStatus + ".");
                }
            }
        }

        // Helper method to actually update the database
        private void CancelOrder(int orderId)
        {
            string query = "UPDATE Orders SET Status = 'Cancelled' WHERE OrderId = @id";

            using (SqlConnection con = _dbService.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", orderId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Order cancelled.");
            LoadOrders(); // Refresh the grid to show the new 'Cancelled' status!
        }
    }
}

