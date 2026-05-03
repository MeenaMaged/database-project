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
    public partial class VendorsForm : Form
    {
        private projectdb.DatabaseService _dbService = new projectdb.DatabaseService();

        public VendorsForm()
        {
            InitializeComponent();
        }

        private void VendorsForm_Load(object sender, EventArgs e)
        {
            // Fetching from the Vendor table
            string query = "SELECT VendorId, UserId, StoreName FROM Vendor";

            using (SqlConnection con = _dbService.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    if (!dataGridView1.Columns.Contains("ViewProducts"))
                    {
                        var viewCol = new DataGridViewButtonColumn();
                        viewCol.Name = "ViewProducts";
                        viewCol.HeaderText = "Products";
                        viewCol.Text = "View Products";
                        viewCol.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(viewCol);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Vendors: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "ViewProducts")
            {
                int vendorId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["VendorId"].Value);
                VendorScreen productsScreen = new VendorScreen(vendorId);
                productsScreen.Show();
            }
        }
    }
}
