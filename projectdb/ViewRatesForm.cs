using Microsoft.Data.SqlClient;
using projectdb;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class ViewRatesForm : Form
    {
        private readonly DatabaseService _dbService = new DatabaseService();
        private int _productId;

        public ViewRatesForm(int productId)
        {
            InitializeComponent();
            _productId = productId;
        }

        private void ViewRatesForm_Load(object sender, EventArgs e)
        {
            LoadRates();
        }

        private void LoadRates()
        {
            string query = @"
                SELECT r.Rating, r.Review, u.FirstName + ' ' + u.LastName as UserName
                FROM Rate r
                JOIN Users u ON r.UserId = u.UserId
                WHERE r.ProductId = @productId";

            using (SqlConnection con = _dbService.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@productId", _productId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    dataGridViewRates.DataSource = table;

                    if (table.Rows.Count == 0)
                    {
                        lblNoRates.Visible = true;
                    }
                    else
                    {
                        lblNoRates.Visible = false;
                        
                        // Calculate Average
                        double avg = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            avg += Convert.ToDouble(row["Rating"]);
                        }
                        avg = avg / table.Rows.Count;
                        lblAvg.Text = $"Average Rating: {avg:F1} / 5.0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading ratings: " + ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
